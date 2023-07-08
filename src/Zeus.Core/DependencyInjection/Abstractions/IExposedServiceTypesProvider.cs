namespace Zeus.Core.DependencyInjection.Abstractions
{
    public interface IExposedServiceTypesProvider
    {
        Type[] GetExposedServiceTypes(Type targetType);
    }
}
