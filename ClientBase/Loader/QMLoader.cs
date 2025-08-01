using System;
using UnityEngine;
using ClientBase.ButtonAPI;
using ClientBase.SDK;

namespace ClientBase.Loader
{
    internal class QMLoader
    {
        public static string folderpath = $"{Environment.CurrentDirectory}/ClientBase";
        public static readonly string resourcePath = System.IO.Path.Combine(Environment.CurrentDirectory + "/ClientBase/Icons");

        public static Sprite LoadSprite(string sprite)
        {
            return $"{resourcePath}/{sprite}".LoadSpriteFromDisk();
        }

        public static void Init()
        {
            CleanQM();
            LoadButtons();
        }

        private static void CleanQM()
        {
            Logging.Log("Cleaning Quick Menu...", LType.Info);

            GameObject gameObject1 = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners");
            GameObject gameObject2 = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/VRC+_Banners");
            GameObject gameObject3 = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_VRCPlus");
            GameObject gameObject4 = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_VRCPlusExperiment");

            UnityEngine.Object.DestroyImmediate(gameObject1);
            UnityEngine.Object.DestroyImmediate(gameObject2);
            UnityEngine.Object.DestroyImmediate(gameObject3);
            UnityEngine.Object.DestroyImmediate(gameObject4);

            Logging.Log("Quick Menu cleaned!", LType.Success);
        }

        private static void LoadButtons()
        {
            Logging.Log("Loading Quick Menu buttons...", LType.Info);

            var qmTab = new QMTabMenu("ClientBase", "ClientBase Tab Menu", null);

            var movementMenu = new QMNestedMenu(qmTab, 1, 0, "Movement", "Movement Features", "Movement Features Menu", true);
            Menus.MovementMenu.Init(movementMenu);

            var exploitsMenu = new QMNestedMenu(qmTab, 2, 0, "Exploits", "Exploit Features", "Exploits Features menu", true);
            Menus.ExploitsMenu.Init(exploitsMenu);

            Logging.Log("Quick Menu buttons loaded!", LType.Success);
        }
    }
}
