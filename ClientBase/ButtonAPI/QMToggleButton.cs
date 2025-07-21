using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ClientBase.ButtonAPI
{
    public class QMToggleButton : QMButtonBase
    {
        public QMToggleButton(QMMenuBase location, float btnXPos, float btnYPos, string btnText, Action onAction, Action offAction, string tooltip, bool defaultState = false)
        {
            btnQMLoc = location.GetMenuName();
            Initialize(btnXPos, btnYPos, btnText, onAction, offAction, tooltip, defaultState);
        }

        public QMToggleButton(DefaultVRCMenu location, float btnXPos, float btnYPos, string btnText, Action onAction, Action offAction, string tooltip, bool defaultState = false)
        {
            btnQMLoc = "Menu_" + location.ToString();
            Initialize(btnXPos, btnYPos, btnText, onAction, offAction, tooltip, defaultState);
        }

        public QMToggleButton(Transform target, float btnXPos, float btnYPos, string btnText, Action onAction, Action offAction, string tooltip, bool defaultState = false)
        {
            parent = target;
            Initialize(btnXPos, btnYPos, btnText, onAction, offAction, tooltip, defaultState);
        }

        private void Initialize(float btnXLocation, float btnYLocation, string btnText, Action onAction, Action offAction, string tooltip, bool defaultState)
        {
            if (parent == null)
            {
                parent = ApiUtils.QuickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/" + btnQMLoc).transform;
            }

            button = UnityEngine.Object.Instantiate(ApiUtils.GetQMButtonTemplate(), parent, true);
            button.name = string.Format("{0}-Toggle-Button-{1}", "VampClient", ApiUtils.RandomNumbers());

            button.transform.Find("Badge_MMJump").gameObject.SetActive(false);

            var rectTransform = button.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(200f, 176f);
            rectTransform.anchoredPosition = new Vector2(-68f, -250f);

            btnTextComp = button.GetComponentInChildren<TextMeshProUGUI>(true);
            btnTextComp.color = Color.white;

            btnComp = button.GetComponentInChildren<Button>(true);
            btnComp.onClick = new Button.ButtonClickedEvent();
            btnComp.onClick.AddListener(new Action(HandleClick));

            btnImageComp = button.transform.Find("Icons/Icon").GetComponentInChildren<Image>(true);
            btnImageComp.gameObject.SetActive(true);

            initShift[0] = 0;
            initShift[1] = 0;

            SetLocation(btnXLocation, btnYLocation);
            SetButtonText($"<color=#FFFFFF>{btnText}</color>");
            SetButtonActions(onAction, offAction);
            SetTooltip(tooltip);
            SetActive(true);

            currentState = defaultState;
            var sprite = currentState ? ApiUtils.OnIconSprite() : ApiUtils.OffIconSprite();
            btnImageComp.sprite = sprite;
            btnImageComp.overrideSprite = sprite;
        }

        private void HandleClick()
        {
            currentState = !currentState;
            var sprite = currentState ? ApiUtils.OnIconSprite() : ApiUtils.OffIconSprite();
            btnImageComp.sprite = sprite;
            btnImageComp.overrideSprite = sprite;

            if (currentState)
            {
                OnAction();
            }
            else
            {
                OffAction();
            }
        }

        public void SetButtonText(string buttonText)
        {
            var textComp = button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            textComp.richText = true;
            textComp.text = buttonText;
        }

        public void SetBackgroundImage(Sprite newImg)
        {
            var bgImage = button.transform.Find("Background").GetComponent<Image>();
            bgImage.sprite = newImg;
            bgImage.overrideSprite = newImg;
            RefreshButton();
        }

        public void ToggleBackgroundImage(bool state)
        {
            button.transform.Find("Background").gameObject.SetActive(state);
        }

        public void SetButtonActions(Action onAction, Action offAction)
        {
            OnAction = onAction;
            OffAction = offAction;
        }

        public void SetToggleState(bool newState, bool shouldInvoke = false)
        {
            try
            {
                var sprite = newState ? ApiUtils.OnIconSprite() : ApiUtils.OffIconSprite();
                btnImageComp.sprite = sprite;
                btnImageComp.overrideSprite = sprite;
                currentState = newState;

                if (shouldInvoke)
                {
                    if (newState)
                        OnAction();
                    else
                        OffAction();
                }
            }
            catch
            {
                
            }
        }

        public void SetInteractable(bool newState)
        {
            button.GetComponent<Button>().interactable = newState;
            RefreshButton();
        }

        public void ClickMe()
        {
            HandleClick();
        }

        public bool GetCurrentState()
        {
            return currentState;
        }

        private void RefreshButton()
        {
            button.SetActive(false);
            button.SetActive(true);
        }

        protected TextMeshProUGUI btnTextComp;
        protected Button btnComp;
        protected Image btnImageComp;
        protected bool currentState;
        protected Action OnAction;
        protected Action OffAction;
    }
}
