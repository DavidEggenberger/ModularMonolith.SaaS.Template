using Microsoft.AspNetCore.Components;

namespace Shared.Client.Components.Modals
{
    public partial class DeletionModal : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string RemoveDeleteButtonNaming { get; set; }

        [Parameter]
        public EventCallback DeletionRequestedCallback { get; set; }

        [Parameter]
        public EventCallback DeletionNotRequestedCallback { get; set; }
    }
}
