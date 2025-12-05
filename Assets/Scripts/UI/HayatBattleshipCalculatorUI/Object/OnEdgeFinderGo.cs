using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HayatBattleshipCalculatorUI
{
    public class OnEdgeFinderGo : MonoBehaviour, IPointerClickHandler
    {
        public GameObject corners;
        public UnityEvent<string> onClick;


        void Awake()
        {
            CursorOnTargetDetector.TargetLocked.AddListener(OnLocked);
            CursorOnTargetDetector.TargetLost.AddListener(OnLost);
        }

        private void OnLocked(string id)
        {
            if (id == gameObject.name)
            {
                corners.SetActive(true);
            }
            else
            {
                corners.SetActive(false);
            }
        }


        private void OnLost(string id)
        {
            if (id == gameObject.name)
            {
                corners.SetActive(false);
            }
        }


        void OnDestroy()
        {
            CursorOnTargetDetector.TargetLocked.RemoveListener(OnLocked);
            CursorOnTargetDetector.TargetLost.RemoveListener(OnLost);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke(gameObject.name);
        }
    }
}