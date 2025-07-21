using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Pages.QM;

namespace ClientBase.ButtonAPI
{
    public class QMNestedMenu : QMMenuBase
    {
        protected bool IsMenuRoot;
        protected GameObject BackButton;
        protected QMSingleButton MainButton;
        protected Transform parent;

        public QMNestedMenu(QMMenuBase location, float posX, float posY, string btnText, string menuTitle, string tooltip, bool halfButton = false, Sprite sprite = null, Sprite bgImage = null)
        {
            btnQMLoc = location.GetMenuName();
            Initialize(false, btnText, posX, posY, menuTitle, tooltip, halfButton, sprite, bgImage);
        }

        public QMNestedMenu(DefaultVRCMenu location, float posX, float posY, string btnText, string menuTitle, string tooltip, bool halfButton = false, Sprite sprite = null, Sprite bgImage = null)
        {
            btnQMLoc = "Menu_" + location;
            Initialize(false, btnText, posX, posY, menuTitle, tooltip, halfButton, sprite, bgImage);
        }

        public QMNestedMenu(Transform target, float posX, float posY, string btnText, string menuTitle, string tooltip, bool halfButton = false, Sprite sprite = null, Sprite bgImage = null)
        {
            parent = target;
            Initialize(false, btnText, posX, posY, menuTitle, tooltip, halfButton, sprite, bgImage);
        }

        private void Initialize(bool isRoot, string btnText, float btnPosX, float btnPosY, string menuTitle, string tooltip, bool halfButton, Sprite sprite, Sprite bgImage)
        {
            MenuName = $"ClientBase-QMMenu-{ApiUtils.RandomNumbers()}";

            MenuObject = UnityEngine.Object.Instantiate(ApiUtils.GetQMMenuTemplate(), ApiUtils.GetQMMenuTemplate().transform.parent);
            MenuObject.name = MenuName;
            MenuObject.SetActive(false);
            MenuObject.transform.SetSiblingIndex(19);

            var fieldInterface = MenuObject.GetComponent<Dashboard>().field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_0;
            UnityEngine.Object.DestroyImmediate(MenuObject.GetComponent<Dashboard>());

            MenuPage = MenuObject.AddComponent<UIPage>();
            MenuPage.field_Public_String_0 = MenuName;
            MenuPage.field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_0 = fieldInterface;
            var pagesList = new Il2CppSystem.Collections.Generic.List<UIPage>();
            pagesList.Add(MenuPage);
            MenuPage.field_Private_List_1_UIPage_0 = pagesList;


            ApiUtils.QuickMenu.prop_MenuStateController_0.field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, MenuPage);

            IsMenuRoot = isRoot;

            if (IsMenuRoot)
            {
                var pages = ApiUtils.QuickMenu.prop_MenuStateController_0.field_Public_ArrayOf_UIPage_0.ToList();
                pages.Add(MenuPage);
                ApiUtils.QuickMenu.prop_MenuStateController_0.field_Public_ArrayOf_UIPage_0 = pages.ToArray();
            }

            Transform layout = MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
            for (int i = 0; i < layout.childCount; i++)
            {
                Transform child = layout.GetChild(i);
                if (child != null)
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }

            MenuTitleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
            SetMenuTitle(menuTitle);

            BackButton = MenuObject.transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
            BackButton.SetActive(true);

            var backBtnComp = BackButton.GetComponentInChildren<Button>();
            backBtnComp.onClick = new Button.ButtonClickedEvent();
            backBtnComp.onClick.AddListener(new Action(OnBackBtnClick));

            var rightContainer = MenuObject.transform.GetChild(0).Find("RightItemContainer");
            rightContainer.Find("Button_QM_Expand").gameObject.SetActive(false);
            rightContainer.Find("Button_QM_Report").gameObject.SetActive(false);

            MainButton = parent != null
                ? new QMSingleButton(parent, btnPosX, btnPosY, btnText, OpenMe, tooltip, halfButton, sprite, bgImage)
                : new QMSingleButton(btnQMLoc, btnPosX, btnPosY, btnText, OpenMe, tooltip, halfButton, sprite, bgImage);

            ClearChildren();

            var scrollRect = MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>();
            scrollRect.enabled = false;
        }

        private void OnBackBtnClick()
        {
            if (IsMenuRoot)
            {
                if (btnQMLoc.StartsWith("Menu_"))
                {
                    ApiUtils.QuickMenu.prop_MenuStateController_0
                        .Method_Public_Void_String_Boolean_Boolean_PDM_0("QuickMenu" + btnQMLoc.Substring(5), false, false);
                }
                else
                {
                    ApiUtils.QuickMenu.prop_MenuStateController_0
                        .Method_Public_Void_String_Boolean_Boolean_PDM_0(btnQMLoc, false, false);
                }
            }
            else
            {
                MenuPage.Method_Protected_Virtual_New_Void_0();
            }
        }

        public void OpenMe()
        {
            ApiUtils.QuickMenu.prop_MenuStateController_0
                .Method_Public_Void_String_UIContext_Boolean_EnumNPublicSealedvaNoLeRiBoIn6vUnique_0(MenuPage.field_Public_String_0, null, false, (UIPage.EnumNPublicSealedvaNoLeRiBoIn6vUnique)1635);

            MenuObject.SetActive(true);
            MenuObject.GetComponent<Canvas>().enabled = true;
            MenuObject.GetComponent<CanvasGroup>().enabled = true;
            MenuObject.GetComponent<GraphicRaycaster>().enabled = true;
        }

        public void CloseMe()
        {
            BackButton.GetComponent<Button>().onClick.Invoke();
        }

        public QMSingleButton GetMainButton() => MainButton;

        public GameObject GetBackButton() => BackButton;
    }
}
