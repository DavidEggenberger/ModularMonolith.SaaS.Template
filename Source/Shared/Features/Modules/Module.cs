using System.Reflection;

namespace Shared.Features.Modules
{
    public class Module
    {
        /// <summary>
        /// Gets the startup class of the module.
        /// </summary>
        public IModuleStartup Startup { get; }

        /// <summary>
        /// Gets the assembly of the module.
        /// </summary>
        public Assembly Assembly => Startup.GetType().Assembly;

        public Module(IModuleStartup startup)
        {
            Startup = startup;
        }
    }
}
