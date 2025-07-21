using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnhollowerRuntimeLib;

namespace ClientBase.ButtonAPI
{
    public class QMSingleButton : QMButtonBase
    {
        public QMSingleButton(QMMenuBase btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, bool halfBtn = false, Sprite icon = null, Sprite bgImage = null)
        {
            btnQMLoc = btnMenu.GetMenuName();
            if (halfBtn) btnYLocation -= 0.21f;
            Initialize(btnXLocation, btnYLocation, btnText, btnAction, tooltip, icon, halfBtn, bgImage);
            if (halfBtn) button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
        }

        public QMSingleButton(DefaultVRCMenu btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, bool halfBtn = false, Sprite sprite = null, Sprite bgImage = null)
        {
            btnQMLoc = "Menu_" + btnMenu.ToString();
            if (halfBtn) btnYLocation -= 0.21f;
            Initialize(btnXLocation, btnYLocation, btnText, btnAction, tooltip, sprite, halfBtn, bgImage);
            if (halfBtn) button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
        }

        public QMSingleButton(string btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, bool halfBtn = false, Sprite sprite = null, Sprite bgImage = null)
        {
            btnQMLoc = btnMenu;
            if (halfBtn) btnYLocation -= 0.21f;
            Initialize(btnXLocation, btnYLocation, btnText, btnAction, tooltip, sprite, halfBtn, bgImage);
            if (halfBtn) button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
        }

        public QMSingleButton(Transform target, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, bool halfBtn = false, Sprite sprite = null, Sprite bgImage = null)
        {
            parent = target;
            if (halfBtn) btnYLocation -= 0.21f;
            Initialize(btnXLocation, btnYLocation, btnText, btnAction, tooltip, sprite, halfBtn, bgImage);
            if (halfBtn) button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
        }

        private void Initialize(float btnXLocation, float btnYLocation, string btnText, Action btnAction, string tooltip, Sprite sprite, bool halfBtn, Sprite bgImage = null)
        {
            if (parent == null)
                parent = ApiUtils.QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/" + btnQMLoc).transform;

            button = UnityEngine.Object.Instantiate(ApiUtils.GetQMButtonTemplate(), parent, true);
            button.transform.Find("Badge_MMJump").gameObject.SetActive(false);
            button.name = $"ClientBase-Single-Button-{ApiUtils.RandomNumbers()}";
            var textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.fontSize = 30f;
            textComponent.color = Color.white;
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 176f);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-68f, -250f);

            if (sprite == null)
            {
                button.transform.Find("Icons/Icon").GetComponent<Image>().gameObject.SetActive(false);
            }
            else
            {
                if (!halfBtn)
                {
                    var img = button.transform.Find("Icons/Icon").GetComponent<Image>();
                    img.overrideSprite = sprite;
                    img.sprite = sprite;

                    var img2 = button.transform.Find("Background").GetComponent<Image>();
                    img2.overrideSprite = bgImage;
                    img2.sprite = bgImage;
                }
                else
                {
                    var img = button.transform.Find("Icons");
                    img.gameObject.SetActive(false);

                    var img2 = button.transform.Find("Background").GetComponent<Image>();
                    img2.overrideSprite = bgImage;
                    img2.sprite = bgImage;
                }
            }

            textComponent.rectTransform.anchoredPosition += new Vector2(0f, 50f);

            initShift[0] = 0;
            initShift[1] = 0;

            SetLocation(btnXLocation, btnYLocation);

            SetButtonText($"<color=#FFFFFF>{btnText}</color>", sprite != null, halfBtn);
            SetAction(btnAction);
            SetActive(true);
            SetTooltip(tooltip);
        }

        public void SetBackgroundImage(Sprite newImg)
        {
            var img = button.transform.Find("Background").GetComponent<Image>();
            img.sprite = newImg;
            img.overrideSprite = newImg;
            RefreshButton();
        }

        public void ToggleBackgroundImage(bool state)
        {
            button.transform.Find("Background").gameObject.SetActive(state);
        }

        public void SetButtonText(string buttonText, bool hasIcon, bool halfBtn)
        {
            var tmp = button.GetComponentInChildren<TextMeshProUGUI>();
            tmp.richText = true;
            tmp.text = buttonText;

            if (hasIcon)
            {
                tmp.transform.position -= new Vector3(0f, 0.025f, 0f);
            }

            if (halfBtn)
            {
                // Optional: additional adjustments for half buttons.
            }
        }

        public void SetAction(Action buttonAction)
        {
            var btn = button.GetComponent<Button>();
            btn.onClick = new Button.ButtonClickedEvent();

            if (buttonAction != null)
                btn.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
        }

        public void SetInteractable(bool newState)
        {
            button.GetComponent<Button>().interactable = newState;
            RefreshButton();
        }

        public void SetFontSize(float size)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().fontSize = size;
        }

        public void ClickMe()
        {
            button.GetComponent<Button>().onClick.Invoke();
        }

        public Image GetBackgroundImage()
        {
            return button.transform.Find("Background").GetComponent<Image>();
        }

        private void RefreshButton()
        {
            button.SetActive(false);
            button.SetActive(true);
        }
    }
}
