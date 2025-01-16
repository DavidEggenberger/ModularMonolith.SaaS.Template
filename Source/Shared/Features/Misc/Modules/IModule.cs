using System.Reflection;

namespace Shared.Features.Misc.Modules
{
    public interface IModule
    {
        Assembly FeaturesAssembly { get; }
    }
}
