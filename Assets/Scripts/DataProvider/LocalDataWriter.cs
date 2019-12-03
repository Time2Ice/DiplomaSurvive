using System;
using System.IO;
using System.Linq;
using DataProvider;
using UnityEngine;

namespace Core.DataProvider
{
    public class LocalDataWriter : ILocalDataWriter
    {
        public static void CreateDirectory(string path, PathType pathType = PathType.Persistent)
        {
            try
            {
                path = FullPath(path, pathType);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        void ILocalDataWriter.DeleteDirectory(string path, PathType pathType)
        {
            DeleteDirectory(path, pathType);
        }

        void ILocalDataWriter.Delete(string path, PathType pathType)
        {
            Delete(path, pathType);
        }

        bool ILocalDataWriter.DirectoryExists(string path, PathType pathType)
        {
            return DirectoryExists(path, pathType);
        }

        bool ILocalDataWriter.FileExists(string path, PathType pathType)
        {
            return FileExists(path, pathType);
        }

        void ILocalDataWriter.WriteAllBytes(string path, byte[] binaryData, PathType pathType)
        {
            WriteAllBytes(path, binaryData, pathType);
        }

        void ILocalDataWriter.WriteAllText(string path, string text, PathType pathType)
        {
            WriteAllText(path, text, pathType);
        }

        byte[] ILocalDataWriter.ReadAllBytes(string path, PathType pathType)
        {
            return ReadAllBytes(path, pathType);
        }

        string ILocalDataWriter.ReadAllText(string path, PathType pathType)
        {
            return ReadAllText(path, pathType);
        }

        string[] ILocalDataWriter.GetFiles(string path, PathType pathType)
        {
            return GetFiles(path, pathType);
        }

        FileInfo[] ILocalDataWriter.GetFileInfos(string path, PathType pathType)
        {
            return GetFileInfos(path, pathType);
        }

        string[] ILocalDataWriter.GetFileNames(string path, string filter, PathType pathType)
        {
            return GetFileNames(path, filter, pathType);
        }

        void ILocalDataWriter.CreateDirectory(string path, PathType pathType)
        {
            CreateDirectory(path, pathType);
        }

        public static void DeleteDirectory(string path, PathType pathType = PathType.Persistent)
        {
            try
            {
                path = FullPath(path, pathType);
                var files = Directory.GetFiles(path);
                var dirs = Directory.GetDirectories(path);

                foreach (var file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (var dir in dirs)
                    DeleteDirectory(dir);

                Directory.Delete(path, false);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public static void Delete(string path, PathType pathType = PathType.Persistent)
        {
            try
            {
                path = FullPath(path, pathType);
                if (File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public static bool FileExists(string path, PathType pathType = PathType.Persistent)
        {
            path = FullPath(path, pathType);
            return File.Exists(path);
        }

        public static bool DirectoryExists(string path, PathType pathType = PathType.Persistent)
        {
            path = FullPath(path, pathType);
            return Directory.Exists(path);
        }

        public static void WriteAllBytes(string path, byte[] binaryData, PathType pathType = PathType.Persistent)
        {
            try
            {
                path = FullPath(path, pathType);
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    File.SetAttributes(path, FileAttributes.Normal);
                }

                File.WriteAllBytes(path, binaryData);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public static void WriteAllText(string path, string text, PathType pathType = PathType.Persistent)
        {
            try
            {
                path = FullPath(path, pathType);
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    File.SetAttributes(path, FileAttributes.Normal);
                }

                File.WriteAllText(path, text);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }
        
        public static FileInfo[] GetFileInfos(string path, PathType pathType = PathType.Persistent)
        {
            path = FullPath(path, pathType);
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files;
            if (Directory.Exists(path))
                files = d.GetFiles();
            else
            {
                Debug.LogError($"The specified path does not exist: {path}");
                files = new FileInfo[0];
            }

            return files;
        }

        public static byte[] ReadAllBytes(string path, PathType pathType = PathType.Persistent)
        {
            try
            {
                path = FullPath(path, pathType);
                var binaryData = File.ReadAllBytes(path);
                return binaryData;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            return new byte[0];
        }

        public static string ReadAllText(string path, PathType pathType = PathType.Persistent)
        {
            path = FullPath(path, pathType);
            string text;
            if (File.Exists(path))
                text = File.ReadAllText(path);
            else
            {
                Debug.LogError($"The specified path does not exist: {path}");
                text = string.Empty;
            }

            return text;
        }

        public static string[] GetFiles(string path, PathType pathType = PathType.Persistent)
        {
            path = FullPath(path, pathType);
            string[] files;
            if (Directory.Exists(path))
                files = Directory.GetFiles(path);
            else
            {
                Debug.LogError($"The specified path does not exist: {path}");
                files = new string[0];
            }

            return files;
        }

        public static string[] GetFileNames(string path, string filter = "*", PathType pathType = PathType.Persistent)
        {
            path = FullPath(path, pathType);
            string[] files;
            if (Directory.Exists(path))
                files = Directory.GetFiles(path, filter).Select(Path.GetFileNameWithoutExtension).ToArray();
            else
            {
                Debug.LogError($"The specified path does not exist: {path}");
                files = new string[0];
            }

            return files;
        }

        private static string FullPath(string path, PathType pathType)
        {
            switch (pathType)
            {
                case PathType.Persistent:
                    return $"{Application.persistentDataPath}{path}";
                case PathType.Cash:
                    return $"{Application.temporaryCachePath}{path}";
                case PathType.Streaming:
                    return $"{Application.streamingAssetsPath}{path}";
                case PathType.Application:
                    return $"{Application.dataPath}{path}";
                case PathType.Console:
                    return $"{Application.consoleLogPath}{path}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(pathType), pathType, null);
            }
        }
    }
}