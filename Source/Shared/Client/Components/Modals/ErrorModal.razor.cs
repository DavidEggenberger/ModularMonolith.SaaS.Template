using Microsoft.AspNetCore.Components;

namespace Shared.Client.Components.Modals
{
    public partial class ErrorModal : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Detail { get; set; }

        [Parameter]
        public EventCallback ModalExitedCallback { get; set; }
    }
}
