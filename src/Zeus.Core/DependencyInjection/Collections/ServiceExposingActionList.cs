using Zeus.Core.DependencyInjection.Abstractions;

namespace Zeus.Core.DependencyInjection.Collections
{
    public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
    {

    }
}
