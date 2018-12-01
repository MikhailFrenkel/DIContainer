namespace DIContainer.Interfaces
{
    public interface IDIContainer
    {
        TDependency Resolve<TDependency>() where TDependency : class;
    }
}
