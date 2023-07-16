/*using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Mixin.Utils
{
    [CreateAssetMenu(fileName = "FolderRefrence", menuName = "Mixin/FolderRefrence")]
    public class FolderRefrence : ScriptableObject
    {
        [SerializeField]
        private string _directoryPath;

        public string DirectoryPath { get => _directoryPath; }

        private void OnEnable()
        {
            _directoryPath = UnityEditor.AssetDatabase.GetAssetPath(this);
        }

        *//*public string GetPath()
        {
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
            return Path.GetDirectoryName(assetPath);
        }*//*

        /// <summary>
        /// Returns all files in this directory
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesInDirectory()
        {
            string[] files = Directory.GetFiles(_directoryPath);

            // Filter out the asset file itself
            files = files.Where(file => file != UnityEditor.AssetDatabase.GetAssetPath(this)).ToArray();

            return files;
        }

        /// <summary>
        /// Returns all files in this directory and its subdirectories recursively.
        /// </summary>
        public string[] GetAllFilesRecursively()
        {
            List<string> files = new List<string>();
            AddFilesRecursively(files, _directoryPath);
            return files.ToArray();
        }

        /// <summary>
        /// Recursively adds all files in the specified directory and its subdirectories to the specified list.
        /// </summary>
        private void AddFilesRecursively(List<string> files, string directory)
        {
            files.AddRange(Directory.GetFiles(directory).Where(file => file != UnityEditor.AssetDatabase.GetAssetPath(this)));
            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                AddFilesRecursively(files, subdirectory);
            }
        }
    }
}
*/