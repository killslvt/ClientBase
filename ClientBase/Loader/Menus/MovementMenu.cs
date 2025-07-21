using ClientBase.ButtonAPI;
using ClientBase.Features.Movement;
using ClientBase.SDK;
using VRC.SDKBase;

namespace ClientBase.Loader.Menus
{
    internal class MovementMenu
    {
        public static void Init(QMNestedMenu menu)
        {
            new QMSingleButton(menu, 1, 0, "Force Jump", () =>
            {
                Networking.LocalPlayer.SetJumpImpulse(1f);
                PopupUtils.HudMessage("Movement", "Forced Jump Enabled", 3);
            }, "", true, null, null);

            new QMToggleButton(menu, 1, 2, "Flight", () =>
            {
                Flight.FlyEnabled = true;  
                PopupUtils.HudMessage("Movement", "Flight Toggled On", 3);
            }, () =>
            {
                Flight.FlyEnabled = false;
                PopupUtils.HudMessage("Movement", "Flight Toggled Off", 3);
            }, "Enables Flight", false);

            new QMToggleButton(menu, 2, 2, "Speed Hack", () =>
            {
                SpeedHack.Enable();
                PopupUtils.HudMessage("Movement", "Speed Hack Toggled On", 3);
            }, () =>
            {
                SpeedHack.Disable();
                PopupUtils.HudMessage("Movement", "Speed Hack Toggled Off", 3);
            }, "Enables Flight", false);
        }
    }
}
