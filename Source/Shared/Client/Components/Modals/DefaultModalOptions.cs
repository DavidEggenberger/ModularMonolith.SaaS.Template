using Blazored.Modal;

namespace Shared.Client.Components.Modals
{
    public class DefaultModalOptions
    {
        public static ModalOptions Modal
        {
            get
            {
                return new ModalOptions
                {
                    OverlayCustomClass = "modaloverlay",
                    Position = ModalPosition.Middle,
                    HideCloseButton = true,
                    HideHeader = true
                };
            }
        }
    }
}
