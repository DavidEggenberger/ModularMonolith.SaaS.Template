using System.Reflection;

namespace Shared.Features.Modules
{
    public interface IModule
    {
        Assembly? FeaturesAssembly { get; }
    }
}
