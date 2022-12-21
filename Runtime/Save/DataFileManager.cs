using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using Mixin.Utils;

namespace Mixin.Save
{
    [Serializable]
    /// <summary>
    /// Manages data from a specific file.
    /// </summary>
    public class DataFileManager<DataFile>
    {
        /// <summary>
        /// The assosiated data
        /// </summary>
        protected DataFile _data;

        protected string _fileName = "";
        protected FileType _fileType = FileType.Binary;

        public event Action OnBeforeSave;
        public event Action OnBeforeLoad;
        public event Action OnBeforeDelete;

        /// <summary>
        /// Called after trying to save file. Returns true if successful.
        /// </summary>
        public event Action<bool> OnAfterSave;
        /// <summary>
        /// Called after trying to load file. Returns true if successful.
        /// </summary>
        public event Action<bool> OnAfterLoad;
        /// <summary>
        /// Called after trying to delete file. Returns true if successful.
        /// </summary>
        public event Action<bool> OnAfterDelete;

        public DataFileManager(string fileName, FileType fileType)
        {
            _fileName = fileName;
            _fileType = fileType;
        }

        /// <summary>
        /// Save data to file.
        /// Calls onDataSaved.
        /// Invokes OnDataSaved.
        /// </summary>
        public void Save()
        {
            OnBeforeSave?.Invoke();

            bool success = false;

            // Create file if it does not exist, or open existing file.
            FileStream fileStream = new FileStream(GetFileNameWithPath(), FileMode.Create);

            object dataToWrite = _data;

            if (UseEnryption())
                dataToWrite = Encrypt(JsonUtility.ToJson(_data));

            // Save data depending on FileType.
            switch (_fileType)
            {
                case FileType.Binary:
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, dataToWrite);
                    break;
                case FileType.XML:
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataFile));
                    xmlSerializer.Serialize(fileStream, dataToWrite);
                    break;
                default:
                    throw new Exception($"FileType '{_fileType}' is not defined. Save process is canceled");
            }

            fileStream.Close();

            success = true;
            OnAfterSave?.Invoke(success);
        }

        /// <summary>
        /// Load data from file.
        /// Calls onDataLoaded.
        /// Invokes OnDataLoaded.
        /// </summary>
        public void Load()
        {
            Load(null);
        }

        /// <summary>
        /// Load data from file.
        /// Calls onDataLoaded.
        /// Invokes OnDataLoaded.
        /// </summary>
        public void Load(SerializationBinder serializationBinder)
        {
            $"DataFileManager Load {_fileName}".Log(Color.yellow);
            OnBeforeLoad?.Invoke();

            bool success = false;

            // File does not exist.
            if (!ThisFileExists())
            {
                $"The file {GetFileNameWithPath()} does not exist.".LogWarning();
            }
            // File does exist.
            else
            {
                success = true;
                // Open existing file.
                FileStream fileStream = new FileStream(GetFileNameWithPath(), FileMode.Open);
                DataFile loadedData = default;
                string loadedEncryptedData = "";

                try
                {
                    // Load data depending on FileType.
                    switch (_fileType)
                    {
                        case FileType.Binary:
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            if (serializationBinder != null)
                                binaryFormatter.Binder = serializationBinder;
                            if (UseEnryption())
                                loadedEncryptedData = (string)binaryFormatter.Deserialize(fileStream);
                            else
                                loadedData = (DataFile)binaryFormatter.Deserialize(fileStream);
                            break;
                        case FileType.XML:
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataFile));
                            if (UseEnryption())
                                loadedEncryptedData = (string)xmlSerializer.Deserialize(fileStream);
                            else
                                loadedData = (DataFile)xmlSerializer.Deserialize(fileStream);
                            break;
                        default:
                            throw new Exception($"FileType '{_fileType}' is not defined. Save process is canceled");
                    }
                }
                catch (SerializationException)
                {
                    Debug.LogError("Could not Serialize FileData.");
                    success = false;
                }
                catch (Exception)
                {
                    success = false;
                }

                if (UseEnryption() && success)
                    loadedData = JsonUtility.FromJson<DataFile>(Decrypt(loadedEncryptedData));

                fileStream.Close();

                if (success)
                    _data = loadedData;
            }

            $"DataFileManager onDataLoaded {_fileName}, success: {success}".Log(Color.yellow);
            OnAfterLoad?.Invoke(success);
        }

        /// <summary>
        /// Delete file and clears data.
        /// Calls onDataDeleted.
        /// Invokes OnDataDeleted.
        /// </summary>
        public void Delete()
        {
            OnBeforeDelete?.Invoke();

            bool success = false;

            // File does exist.
            if (ThisFileExists())
            {
                File.Delete(GetFileNameWithPath());
                success = true;
            }
            // File does not exist.
            else
                $"The file {GetFileNameWithPath()} does not exist.".LogWarning();

            _data = default;
            OnAfterDelete?.Invoke(success);
        }

        public string GetFileName()
            => _fileName;

        public string GetFileNameWithoutExtension()
            => Path.GetFileNameWithoutExtension(GetFileNameWithPath().ToString());

        public string GetFileNameWithPath()
            => $"{GetDataSavePath()}/{GetFileName()}";

        // returns the base path where files will be saved
        public string GetDataSavePath()
            => Application.persistentDataPath;

        public long GetFileSize()
            => new FileInfo(GetFileNameWithPath()).Length;

        public string GetFileSizeInBytes()
            => $"{GetFileSize().FormatThousand()} Bytes";

        public bool ThisFileExists()
            => FileExists(GetFileNameWithPath());

        protected static bool FileExists(string path)
            => File.Exists(path);

        #region Encryption + Decryption

        protected virtual bool UseEnryption() => false;

        /* DO NOT CHANGE THIS VALUE!! */
        private const string _salt = "ZUMdrrc9LhRSk0OdZt1R";

        private static string Encrypt(string stringToEncrypt)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(stringToEncrypt);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_salt));
                using (TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider()
                {
                    Key = key,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                }
                    )
                {
                    ICryptoTransform cryptoTransform = cryptoServiceProvider.CreateEncryptor();
                    byte[] result = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(result, 0, result.Length);
                }
            }
        }

        private static string Decrypt(string stringToDecrypt)
        {
            byte[] data = Convert.FromBase64String(stringToDecrypt);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_salt));
                using (TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider()
                {
                    Key = key,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                }
                    )
                {
                    ICryptoTransform cryptoTransform = cryptoServiceProvider.CreateDecryptor();
                    byte[] result = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(result);
                }
            }
        }
        #endregion
    }
}