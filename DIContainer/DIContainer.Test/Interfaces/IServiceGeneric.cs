namespace DIContainer.Test.Interfaces
{
    internal interface IServiceGeneric<T> where T : IService
    {
        T Service { get; }
    }
}
