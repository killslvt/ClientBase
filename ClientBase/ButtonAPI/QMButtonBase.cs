using UnityEngine;
using VRC.Localization;

namespace ClientBase.ButtonAPI
{
    public class QMButtonBase
    {
        public GameObject GetGameObject() => button;

        public void SetActive(bool state)
        {
            if (button != null)
                button.SetActive(state);
        }

        public void SetLocation(float buttonXLoc, float buttonYLoc)
        {
            if (button == null)
                return;

            var rect = button.GetComponent<RectTransform>();
            if (rect == null)
                return;

            rect.anchoredPosition += Vector2.right * (232f * (buttonXLoc + initShift[0]));
            rect.anchoredPosition += Vector2.down * (210f * (buttonYLoc + initShift[1]));
        }

        public void SetTooltip(string tooltip)
        {
            if (button == null)
                return;

            foreach (var toolTip in button.GetComponents<ToolTip>())
            {
                if (toolTip != null)
                {
                    var localized = LocalizableStringExtensions.Localize(tooltip);
                    toolTip._localizableString = localized;
                    toolTip._alternateLocalizableString = localized;
                }
            }
        }

        public void DestroyMe()
        {
            if (button != null)
            {
                Object.Destroy(button);
                button = null;
            }
        }

        protected void Initialize(GameObject buttonObj, string location, Transform parentTransform, int shiftX = 0, int shiftY = 0)
        {
            button = buttonObj;
            btnQMLoc = location;
            parent = parentTransform;
            initShift = new[] { shiftX, shiftY };
        }

        protected GameObject button;
        protected string btnQMLoc;
        protected Transform parent;
        protected int[] initShift = new int[2];
    }
}
