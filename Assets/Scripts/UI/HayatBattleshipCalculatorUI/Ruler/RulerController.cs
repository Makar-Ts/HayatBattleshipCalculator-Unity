using System;
using TMPro;
using UnityEngine;

namespace HayatBattleshipCalculatorUI
{
    public class Ruler : MonoBehaviour
    {
        [SerializeField] private RectTransform ruler;
        [SerializeField] private TMP_Text distanceTextarea;

        private Vector3 startPosition;

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                ruler.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ruler.gameObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.R))
            {
                var cam = Camera.main;

                Vector3 curPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RectTransform canvasRect = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    cam.WorldToScreenPoint(startPosition),
                    cam,
                    out Vector2 startPos
                );

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    cam.WorldToScreenPoint(curPosition),
                    cam,
                    out Vector2 curPos
                );

                var diff = curPos - startPos;

                ((RectTransform)transform).anchoredPosition = startPos;

                float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg-90;
                ruler.localRotation = Quaternion.Euler(0, 0, angle);

                ruler.sizeDelta = new Vector2(ruler.sizeDelta.x, diff.magnitude-((RectTransform)transform).sizeDelta.y);

                float worldDistance = Vector3.Distance(
                    new Vector3(startPosition.x, startPosition.y, 0),
                    new Vector3(curPosition.x, curPosition.y, 0)
                );

                distanceTextarea.text = worldDistance.ToString("F0")+"m";
                Debug.Log(angle);
                ((RectTransform)distanceTextarea.transform).localRotation = Quaternion.Euler(0, 0, angle > 0 || angle < -180 ? -90 : 90);
            }
        }
    }
}
