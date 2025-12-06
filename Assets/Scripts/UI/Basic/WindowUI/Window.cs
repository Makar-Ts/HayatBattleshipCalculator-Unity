using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WindowsUI
{
    public enum WindowState
    {
        ACTIVE,
        HIDDEN,
        CLOSED
    }


    public class Window : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;

                if (titleTexarea != null) titleTexarea.text = _title;
            }
        }
        public TMP_Text titleTexarea;
        public RectTransform header;
        public RectTransform content;

        private RectTransform windowRectTransform;
        private Canvas canvas;

        private void Awake()
        {
            windowRectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            
            if (titleTexarea.text.Length != 0)
            {
                Title = titleTexarea.text;
            }
            else
            {
                Title = RandomStringGenerator.GenerateRandomString(7);
            }
        }

        private Vector2 pointerOffset;
        private Boolean isDragging = false;
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerEnter == header.gameObject || eventData.pointerEnter == titleTexarea.gameObject)
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        windowRectTransform,
                        eventData.position,
                        eventData.pressEventCamera,
                        out pointerOffset))
                {
                    isDragging = true;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;
        }

        void LateUpdate()
        {
            if (isDragging)
            {
                Vector2 localPointerPosition;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.transform as RectTransform,
                    Input.mousePosition,
                    Camera.main,
                    out localPointerPosition))
                {
                    windowRectTransform.localPosition = localPointerPosition - pointerOffset;
                }
            }
        }


        public WindowState state
        {
            get { 
                return  !header.gameObject.activeSelf ? WindowState.CLOSED :
                        !content.gameObject.activeSelf ? WindowState.HIDDEN :
                        WindowState.ACTIVE;
            }
            private set
            {
                switch (value)
                {
                    case WindowState.CLOSED:
                        header.gameObject.SetActive(false);
                        content.gameObject.SetActive(false);

                        break;
                    case WindowState.HIDDEN:
                        header.gameObject.SetActive(true);
                        content.gameObject.SetActive(false);

                        break;
                    case WindowState.ACTIVE:
                    default:
                        header.gameObject.SetActive(true);
                        content.gameObject.SetActive(true);

                        break;
                }
            }
        }
        public void Hide()
        {
            state = state == WindowState.ACTIVE ? WindowState.HIDDEN : WindowState.ACTIVE;
        }
        public void Close()
        {
            state = state == WindowState.ACTIVE || state == WindowState.HIDDEN ? WindowState.CLOSED : WindowState.ACTIVE;
        }
        public void SetState(WindowState s)
        {
            state = s;
        }
    }
}
