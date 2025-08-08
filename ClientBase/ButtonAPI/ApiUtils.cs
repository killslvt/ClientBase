using System.Linq;
using UnityEngine;
using VRC.UI.Elements;

namespace ClientBase.ButtonAPI
{
    public static class ApiUtils
    {
        public const string Identifier = "ClientBase";

        private static QuickMenu _quickMenu;
        private static GameObject _selectedUserPageGrid;
        private static GameObject _qmMenuTemplate;
        private static GameObject _qmTabTemplate;
        private static GameObject _qmButtonTemplate;
        private static GameObject _mmMenuTemplate;
        private static GameObject _mmTabTemplate;
        private static GameObject _mmButtonTemplate;

        public static readonly System.Random random = new System.Random();

        public static QuickMenu QuickMenu => _quickMenu = Resources.FindObjectsOfTypeAll<QuickMenu>().FirstOrDefault();

        public static GameObject GetSelectedUserPageGrid()
        {
            if (_selectedUserPageGrid == null)
            {
                _selectedUserPageGrid = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");
            }
            return _selectedUserPageGrid;
        }

        public static GameObject GetQMMenuTemplate()
        {
            if (_qmMenuTemplate == null && QuickMenu != null)
            {
                _qmMenuTemplate = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard")?.gameObject;
            }
            return _qmMenuTemplate;
        }

        public static GameObject GetQMTabButtonTemplate()
        {
            if (_qmTabTemplate == null && QuickMenu != null)
            {
                _qmTabTemplate = QuickMenu.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings")?.gameObject;
            }
            return _qmTabTemplate;
        }

        public static GameObject GetQMButtonTemplate()
        {
            if (_qmButtonTemplate == null && QuickMenu != null)
            {
                _qmButtonTemplate = QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Here/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_WorldActions/Button_RejoinWorld")?.gameObject;
            }
            return _qmButtonTemplate;
        }

        public static Sprite OnIconSprite()
        {
            return Loader.QMLoader.LoadSprite("Icon_On.png");
        }

        public static Sprite OffIconSprite()
        {
            return Loader.QMLoader.LoadSprite("Icon_Off.png");
        }

        public static int RandomNumbers()
        {
            return random.Next(100000, 999999);
        }

        public static string GetSelectedPageName()
        {
            var controller = QuickMenu?.prop_MenuStateController_0;  
            return controller?.field_Private_UIPage_0?.field_Public_String_0 ?? "Unknown";
        }
    }
}
