using Zeus.Core.DependencyInjection.Abstractions;

namespace Zeus.Core.DependencyInjection.Collections
{
    public class ServiceRegistrationActionList : List<Action<IOnServiceRegistredContext>>
    {
        public bool IsClassInterceptorsDisabled { get; set; }
    }
}
