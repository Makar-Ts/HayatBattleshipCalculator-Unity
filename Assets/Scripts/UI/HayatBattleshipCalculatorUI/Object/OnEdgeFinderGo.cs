using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HayatBattleshipCalculatorUI
{
    public class OnEdgeFinderGo : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent<string> onClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke(gameObject.name);
        }
    }
}