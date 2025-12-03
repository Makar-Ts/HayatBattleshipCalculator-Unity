using System.Collections.Generic;
using HayatBattleshipCalculator;
using UnityEngine;

namespace HayatBattleshipCalculatorUI
{
    public class OnEdgeFinder : MonoBehaviour
    {
        [SerializeField] private GameObject finder;
        [SerializeField] private Vector2 padding;
        [SerializeField] private Camera uiCamera;

        private readonly Dictionary<string, RectTransform> _finders = new();


        private void Start()
        {
            HayatBattleshipCalculator.Object.ObjectCreated.AddListener(OnObjectCreated);
            HayatBattleshipCalculator.Object.ObjectDeleted.AddListener(OnObjectDeleted);
        }


        private void OnObjectCreated(string id)
        {
            if (_finders.ContainsKey(id)) 
                return;

            var go = Instantiate(finder, transform);
            go.name = id;

            var rt = go.GetComponent<RectTransform>();
            if (rt == null)
            {
                Debug.LogError($"Finder prefab '{finder.name}' must contain a RectTransform.");
                Destroy(go);
                return;
            }

            _finders[id] = rt;
        }


        private void OnObjectDeleted(string id)
        {
            if (!_finders.TryGetValue(id, out var rt))
                return;

            if (rt != null)
                Destroy(rt.gameObject);

            _finders.Remove(id);
        }


        private void Update()
        {
            Camera cam = uiCamera != null ? uiCamera : Camera.main;
            if (cam == null)
                return;

            foreach (var kvp in _finders)
            {
                string id = kvp.Key;
                RectTransform finderTransform = kvp.Value;

                if (finderTransform == null)
                    continue;

                if (!HayatBattleshipCalculator.Object.Objects.TryGetValue(id, out var obj) || obj == null)
                    continue;

                Transform objTransform = obj.transform;
                if (objTransform == null)
                    continue;

                Vector2 localPos;
                RectTransform canvasRect = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();

                Vector3 screenPos = cam.WorldToScreenPoint(objTransform.position);

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasRect,
                    screenPos,
                    cam,
                    out localPos
                );

                float halfX = canvasRect.sizeDelta.x * 0.5f;
                float halfY = canvasRect.sizeDelta.y * 0.5f;

                float clampedX = Mathf.Clamp(localPos.x, -halfX + padding.x, halfX - padding.x);
                float clampedY = Mathf.Clamp(localPos.y, -halfY + padding.y, halfY - padding.y);

                finderTransform.localPosition = new Vector2(clampedX, clampedY);
            }
        }


        private void OnDestroy()
        {
            HayatBattleshipCalculator.Object.ObjectCreated.RemoveListener(OnObjectCreated);
            HayatBattleshipCalculator.Object.ObjectDeleted.RemoveListener(OnObjectDeleted);
        }
    }
}
