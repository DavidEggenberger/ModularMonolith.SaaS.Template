using Microsoft.AspNetCore.Components;

namespace Shared.Client.Components.Modals
{
    public partial class ErrorModal
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Detail { get; set; }

        [Parameter]
        public Action ModalExitedCallback { get; set; }
    }
}
