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
        private const string _FILENAME_PREFIX = "testmode_";

        /// <summary>
        /// The assosiated data
        /// </summary>
        public DataFile Data;

        protected string fileName = "";
        protected FileType fileType = FileType.Binary;

        /// <summary>
        /// Called after trying to save file. Returns true if successful.
        /// </summary>
        public event EventHandler<bool> OnDataSaved;
        /// <summary>
        /// Called after trying to load file. Returns true if successful.
        /// </summary>
        public event EventHandler<bool> OnDataLoaded;
        /// <summary>
        /// Called after trying to delete file. Returns true if successful.
        /// </summary>
        public event EventHandler<bool> OnDataDeleted;

        public DataFileManager(string fileName, FileType fileType)
        {
            this.fileName = fileName;
            this.fileType = fileType;
        }

        protected virtual void SetFileInformation() { }

        /// <summary>
        /// Save data to file.
        /// Calls onDataSaved.
        /// Invokes OnDataSaved.
        /// </summary>
        public void Save()
        {
            onBeforeSave();

            bool success = false;

            // Create file if it does not exist, or open existing file.
            FileStream fileStream = new FileStream(GetFileNameWithPath(), FileMode.Create);

            object dataToWrite = Data;

            if (useEnryption())
                dataToWrite = encrypt(JsonUtility.ToJson(Data));

            // Save data depending on FileType.
            switch (fileType)
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
                    throw new Exception($"FileType '{fileType}' is not defined. Save process is canceled");
            }

            fileStream.Close();

            success = true;
            onDataSaved(success);
            OnDataSaved?.Invoke(this, success);
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
            $"DataFileManager Load {fileName}".Log(Color.yellow);
            onBeforeLoad();

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
                    switch (fileType)
                    {
                        case FileType.Binary:
                            BinaryFormatter binaryFormatter = new BinaryFormatter();
                            if (serializationBinder != null)
                                binaryFormatter.Binder = serializationBinder;
                            if (useEnryption())
                                loadedEncryptedData = (string)binaryFormatter.Deserialize(fileStream);
                            else
                                loadedData = (DataFile)binaryFormatter.Deserialize(fileStream);
                            break;
                        case FileType.XML:
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataFile));
                            if (useEnryption())
                                loadedEncryptedData = (string)xmlSerializer.Deserialize(fileStream);
                            else
                                loadedData = (DataFile)xmlSerializer.Deserialize(fileStream);
                            break;
                        default:
                            throw new Exception($"FileType '{fileType}' is not defined. Save process is canceled");
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

                if (useEnryption() && success)
                    loadedData = JsonUtility.FromJson<DataFile>(decrypt(loadedEncryptedData));

                fileStream.Close();

                if (success)
                    Data = loadedData;
            }

            $"DataFileManager onDataLoaded {fileName}, success: {success}".Log(Color.yellow);
            onDataLoaded(success);
            OnDataLoaded?.Invoke(this, success);
        }

        /// <summary>
        /// Delete file and clears data.
        /// Calls onDataDeleted.
        /// Invokes OnDataDeleted.
        /// </summary>
        public void Delete()
        {
            onBeforeDelete();

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

            Data = default;
            onDataDeleted(success);
            OnDataDeleted?.Invoke(this, success);
        }

        /// <summary>
        /// Called before trying to save data.
        /// </summary>
        protected virtual void onBeforeSave()
        {
            SetFileInformation();
        }

        /// <summary>
        /// Called when tried to save data.
        /// </summary>
        /// <param name="success">Was save successful?</param>
        protected virtual void onDataSaved(bool success) { }

        /// <summary>
        /// Called before trying to load data.
        /// </summary>
        protected virtual void onBeforeLoad() { }

        /// <summary>
        /// Called when tried to load data.
        /// </summary>
        /// <param name="success">Was load successful?</param>
        protected virtual void onDataLoaded(bool success) { }

        /// <summary>
        /// Called before trying to delete file.
        /// </summary>
        protected virtual void onBeforeDelete() { }

        /// <summary>
        /// Called when tried to delete file.
        /// </summary>
        /// <param name="success">Was delete successful?</param>
        protected virtual void onDataDeleted(bool success) { }

        public string GetFileName()
            => fileName;

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

        protected virtual bool useEnryption() => false;

        /* DO NOT CHANGE THIS VALUE!! */
        private const string salt = "ZUMdrrc9LhRSk0OdZt1R";

        private static string encrypt(string stringToEncrypt)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(stringToEncrypt);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(salt));
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

        private static string decrypt(string stringToDecrypt)
        {
            byte[] data = Convert.FromBase64String(stringToDecrypt);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(salt));
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