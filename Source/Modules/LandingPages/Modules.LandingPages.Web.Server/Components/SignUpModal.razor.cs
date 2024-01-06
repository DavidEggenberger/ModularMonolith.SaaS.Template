using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Modules.LandingPages.Web.Server.Components
{
    public partial class SignUpModal
    {
        [Parameter]
        public EventCallback CancelRequestedCallback { get; set; }

        private bool switchToSignIn;

        public async Task CancelRequestedAsync()
        {
            if (CancelRequestedCallback.HasDelegate)
            {
                await CancelRequestedCallback.InvokeAsync(this);
            }
        }
    }
}
