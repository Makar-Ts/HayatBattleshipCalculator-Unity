using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform header;
    public RectTransform content;

    private RectTransform windowRectTransform;
    private Canvas canvas;

    private void Awake()
    {
        windowRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("DraggableWindow must be inside a Canvas.");
        }
    }

    private Vector2 pointerOffset;
    private Boolean isDragging = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter == header.gameObject)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    windowRectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out pointerOffset))
            {
                isDragging = true;
                Hide(true);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    void Update()
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


    public Boolean hidden
    {
        get { return content.gameObject.activeSelf; }
        private set { content.gameObject.SetActive(value); }
    }
    public void Hide()
    {
        hidden = !hidden;
    }
    public void Hide(Boolean state)
    {
        hidden = state;
    }
}
