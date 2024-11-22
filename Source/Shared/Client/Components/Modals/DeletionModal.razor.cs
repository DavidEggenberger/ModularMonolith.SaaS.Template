using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Client.Components.Modals
{
    public partial class DeletionModal
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
