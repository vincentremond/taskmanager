namespace TaskManager.Contract.Utilities
{
    public interface ICloneProvider
    {
        T DeepClone<T>(T obj);
    }
}
