using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Pages.QM;

namespace ClientBase.ButtonAPI
{
    public class QMMenuPage : QMMenuBase
    {
        public QMMenuPage(string MenuTitle, string tooltip, Sprite ButtonImage = null)
        {
            Initialize(MenuTitle, tooltip, ButtonImage);
        }

        private void Initialize(string MenuTitle, string tooltip, Sprite ButtonImage)
        {
            MenuName = $"ClientBase-Tab-Menu-{ApiUtils.RandomNumbers()}";
            MenuObject = UnityEngine.Object.Instantiate(ApiUtils.GetQMMenuTemplate(), ApiUtils.GetQMMenuTemplate().transform.parent);
            MenuObject.name = MenuName;
            MenuObject.SetActive(false);
            MenuObject.transform.SetSiblingIndex(19);

            var oldDashboard = MenuObject.GetComponent<Dashboard>();
            var dashboardField = oldDashboard.field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_0;
            UnityEngine.Object.DestroyImmediate(oldDashboard);

            MenuPage = MenuObject.AddComponent<UIPage>();
            MenuPage.field_Public_String_0 = MenuName;
            MenuPage.field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_0 = dashboardField;
            var pagesList = new Il2CppSystem.Collections.Generic.List<UIPage>();
            pagesList.Add(MenuPage);
            MenuPage.field_Private_List_1_UIPage_0 = pagesList;

            var menuStateController = ApiUtils.QuickMenu.prop_MenuStateController_0;
            menuStateController.field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, MenuPage);

            var pages = menuStateController.field_Public_ArrayOf_UIPage_0.ToList();
            pages.Add(MenuPage);
            menuStateController.field_Public_ArrayOf_UIPage_0 = pages.ToArray();

            var layoutGroup = MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup");
            for (int i = 0; i < layoutGroup.childCount; i++)
                UnityEngine.Object.Destroy(layoutGroup.GetChild(i).gameObject);

            MenuTitleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
            SetMenuTitle(MenuTitle);

            var rightContainer = MenuObject.transform.GetChild(0).Find("RightItemContainer");
            rightContainer.Find("Button_QM_Expand").gameObject.SetActive(false);
            rightContainer.Find("Button_QM_Report").gameObject.SetActive(false);

            ClearChildren();
            MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>().enabled = false;

            MainButton = UnityEngine.Object.Instantiate(ApiUtils.GetQMTabButtonTemplate(), ApiUtils.GetQMTabButtonTemplate().transform.parent);
            MainButton.name = MenuName;

            MenuTabComp = MainButton.GetComponent<MenuTab>();
            MenuTabComp.field_Private_MenuStateController_0 = menuStateController;
            MenuTabComp._controlName = MenuName;
            MenuTabComp.GetComponent<StyleElement>().field_Private_Selectable_0 = MainButton.GetComponent<Button>();

            BadgeObject = MainButton.transform.GetChild(0).gameObject;
            BadgeText = BadgeObject.GetComponentInChildren<TextMeshProUGUI>();

            MainButton.GetComponent<Button>().onClick.AddListener(new Action(OnMainButtonClick));

            UnityEngine.Object.Destroy(MainButton.GetComponent<MonoBehaviour1PublicVoVo5>());
            SetTooltip(tooltip);

            if (ButtonImage != null)
                SetImage(ButtonImage);
        }

        private void OnMainButtonClick()
        {
            MenuObject.SetActive(true);
            MenuObject.GetComponent<Canvas>().enabled = true;
            MenuObject.GetComponent<CanvasGroup>().enabled = true;
            MenuObject.GetComponent<GraphicRaycaster>().enabled = true;
            MenuTabComp.GetComponent<StyleElement>().field_Private_Selectable_0 = MainButton.GetComponent<Button>();
        }

        public void SetImage(Sprite newImg)
        {
            var img = MainButton.transform.Find("Icon").GetComponent<Image>();
            img.sprite = newImg;
            img.overrideSprite = newImg;
            img.color = Color.white;
            img.m_Color = Color.white;
        }

        public void SetIndex(int newPosition) => MainButton.transform.SetSiblingIndex(newPosition);

        public void SetActive(bool newState) => MainButton.SetActive(newState);

        public void SetBadge(bool showing = true, string text = "")
        {
            if (BadgeObject != null && BadgeText != null)
            {
                BadgeObject.SetActive(showing);
                BadgeText.text = text;
            }
        }

        public void OpenMe() => MainButton.GetComponent<Button>().onClick.Invoke();

        public void SetTooltip(string tooltip)
        {
            foreach (var toolTip in MainButton.GetComponents<ToolTip>())
            {
                var localized = LocalizableStringExtensions.Localize(tooltip, null, null, null);
                toolTip._localizableString = localized;
                toolTip._alternateLocalizableString = localized;
            }
        }

        public GameObject GetMainButton() => MainButton;

        protected GameObject MainButton;
        protected GameObject BadgeObject;
        protected TextMeshProUGUI BadgeText;
        protected MenuTab MenuTabComp;
    }
}
