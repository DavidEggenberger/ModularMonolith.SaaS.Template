using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Modules.LandingPages.Web.Server.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modules.LandingPages.Web.Server.Pages
{
    public partial class LandingPage : ComponentBase
    {
        [Inject]
        public IModalService ModalService { get; set; }

        [Inject]
        public NavigationManager NaviagtionManager { get; set; }

        [SupplyParameterFromQuery(Name = "ShowSignUp")]
        public bool ShowSignUp { get; set; }

        [SupplyParameterFromQuery(Name = "ShowSignIn")]
        public bool ShowSignIn { get; set; }


        protected FeatureSection SelectedFeature;
        private bool clicked;
        private List<FAQ> faqs;

        protected override async Task OnInitializedAsync()
        {
            SelectedFeature = FeatureSection.ExcelTable;
            faqs = new List<FAQ>()
            {
                new FAQ() { Answer = "I already use LinkedIn, what does ContactCone bring me?" }
            };

            CarouselInitAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            //if (ShowSignIn)
            //{
            //    ModalService.Show<SignUpModal>(string.Empty, DefaultModalOptions.Options);
            //}

            //if (ShowSignUp)
            //{
            //    ModalService.Show<SignInModal>(string.Empty, DefaultModalOptions.Options);
            //}
        }

        private async Task CarouselInitAsync()
        {
            while (clicked is false)
            {
                await Task.Delay(6000);
                if (clicked is true)
                {
                    return;
                }
                var currentIndex = (int)SelectedFeature;
                SelectedFeature = (FeatureSection)((currentIndex % 4) + 1);
                StateHasChanged();
            }
        }

        private IModalReference modalReference;

        public void OpenSignUpModal()
        {
            //var modelParameters = new ModalParameters
            //{
            //    { nameof(SignInModal.CancelRequestedCallback), new EventCallback(this, () => modalReference.Close()) }
            //};

            //modalReference = ModalService.Show<SignUpModal>(string.Empty, modelParameters, DefaultModalOptions.Options);
        }
    }
    public enum FeatureSection
    {
        ExcelTable = 1,
        Touchpoints = 2,
        Groups = 3,
        Notifications = 4
    }

    public class FAQ
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsExpanded { get; set; }
    }
}
