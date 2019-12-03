using Core.DataProvider;
using UnityEngine;

namespace DataProvider
{
    public class LocalDataProvider : ILocalDataProvider
    {
        private const string PathFolder = "AppData";

        void ILocalDataProvider.Save<T>(T obj, PathType pathType, string postfix)
        {
            Save(obj, pathType, postfix);
        }
        
        T ILocalDataProvider.Load<T>(PathType pathType, string postfix)
        {
            return Load<T>(pathType, postfix);
        }

        bool ILocalDataProvider.Exist<T>(PathType pathType, string postfix)
        {
            return Exist<T>(pathType, postfix);
        }

        void ILocalDataProvider.Delete<T>(PathType pathType, string postfix)
        {
            Delete<T>(pathType, postfix);
        }

        public static void Save<T>(T obj, PathType pathType = PathType.Persistent, string postfix = null)
        {
            var pathFolder = $"/{PathFolder}";
            if (!LocalDataWriter.DirectoryExists(pathFolder, pathType))
                LocalDataWriter.CreateDirectory(pathFolder, pathType);

            var path = $"{pathFolder}/{typeof(T)}{postfix??string.Empty}";
            var str = JsonUtility.ToJson(obj);
            LocalDataWriter.WriteAllText(path, str, pathType);
        }


        public static T Load<T>(PathType pathType = PathType.Persistent, string postfix = null)
        {
            var path = $"/{PathFolder}/{typeof(T)}{postfix??string.Empty}";
            if (!LocalDataWriter.FileExists(path, pathType))
            {
                Debug.LogError($"The specified path does not exist: {path}");
                return default;
            }

            var str = LocalDataWriter.ReadAllText(path, pathType);
            var result = JsonUtility.FromJson<T>(str); //todo use parser
            return result;
        }

        public static bool Exist<T>(PathType pathType = PathType.Persistent, string postfix = null)
        {
            return LocalDataWriter.FileExists($"/{PathFolder}/{typeof(T)}{postfix??string.Empty}", pathType);
        }

        public static void Delete<T>(PathType pathType = PathType.Persistent, string postfix = null)
        {
            if (Exist<T>())
                LocalDataWriter.Delete($"/{PathFolder}/{typeof(T)}{postfix??string.Empty}", pathType);
        }
    }
}