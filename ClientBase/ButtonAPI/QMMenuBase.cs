using TMPro;
using UnityEngine;
using VRC.UI.Elements;

namespace ClientBase.ButtonAPI
{
    public class QMMenuBase
    {
        public string GetMenuName() => MenuName;

        public UIPage GetMenuPage() => MenuPage;

        public GameObject GetMenuObject() => MenuObject;

        public void SetMenuTitle(string newTitle)
        {
            if (MenuObject == null) return;

            var titleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
            if (titleText != null)
            {
                titleText.text = newTitle;
                titleText.richText = true;
            }
        }

        public void ClearChildren()
        {
            if (MenuObject == null) return;

            for (int i = MenuObject.transform.childCount - 1; i >= 0; i--)
            {
                var child = MenuObject.transform.GetChild(i);
                if (child.name != "Header_H1" && child.name != "ScrollRect")
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }

        protected string btnQMLoc;
        protected GameObject MenuObject;
        internal TextMeshProUGUI MenuTitleText;
        protected UIPage MenuPage;
        protected string MenuName;
    }
}
