using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WindowsUI
{
    [RequireComponent(typeof(Button))]
    public class WindowSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button controller;

        public Window windowForSelection;

        [SerializeField] private TMP_Text indicatorTextarea;
        [SerializeField] private TMP_Text titleTextarea;


        void Awake()
        {
            controller = GetComponent<Button>();
        }


        public void OnPointerEnter(PointerEventData data)
        {
            indicatorTextarea.text = ">";
        }

        public void OnPointerExit(PointerEventData data)
        {
            indicatorTextarea.text = "-";
        }


        void Start()
        {
            titleTextarea.text = windowForSelection.Title;

            controller.onClick.AddListener(Open);
        }

        private void Open()
        {
            windowForSelection.Close();
            indicatorTextarea.text = "-";
        }


        void OnDestroy()
        {
            controller.onClick.RemoveAllListeners();
        }
    }
}