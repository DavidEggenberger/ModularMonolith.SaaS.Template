using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
