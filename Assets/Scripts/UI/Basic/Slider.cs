using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Slider : MonoBehaviour
{
    [SerializeField] private RectTransform slider;

    private RectTransform self;


    public float Value
    {
        get { return slider.rect.width / self.rect.width; }
        set
        {
            Vector2 size = slider.sizeDelta;
            size.x = self.rect.width * (value - 1);
            slider.sizeDelta = size;
        }
    }


    void Start()
    {
        self = GetComponent<RectTransform>();
    }
}
