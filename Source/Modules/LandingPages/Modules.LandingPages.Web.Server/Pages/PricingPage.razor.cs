using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Modules.LandingPages.Web.Server.Components;

namespace Modules.LandingPages.Web.Server.Pages
{
    public partial class PricingPage : ComponentBase
    {
        [Inject]
        public IModalService ModalService { get; set; }

        private IModalReference modalReference;

        public bool ShowMonthlyPrices = true;

        public void OpenSignUpModal()
        {
            //var modelParameters = new ModalParameters
            //{
            //    { nameof(SignInModal.CancelRequestedCallback), new EventCallback(this, () => modalReference.Close()) }
            //};

            //modalReference = ModalService.Show<SignUpModal>(string.Empty, modelParameters, DefaultModalOptions.Options);
        }
    }
}
