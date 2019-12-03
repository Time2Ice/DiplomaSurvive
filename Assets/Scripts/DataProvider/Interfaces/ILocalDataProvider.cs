namespace DataProvider
{
    public interface ILocalDataProvider
    {
        void Save<T>(T obj, PathType pathType = PathType.Persistent, string postfix = null);
        T Load<T>(PathType pathType = PathType.Persistent, string postfix = null);
        bool Exist<T>(PathType pathType = PathType.Persistent, string postfix = null);
        void Delete<T>(PathType pathType = PathType.Persistent, string postfix = null);

    }
}