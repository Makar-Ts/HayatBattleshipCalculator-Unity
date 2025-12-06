using HayatBattleshipCalculator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HayatBattleshipCalculatorUI
{
    public class MovementWindowController : MonoBehaviour
    {
        [SerializeField] private GameObject noTargetPanel;
        [SerializeField] private TMP_InputField xValueTextarea, yValueTextarea, headingTextarea;
        [SerializeField] private TMP_Dropdown forceTypeDropdown;
        [SerializeField] private Button applyHeadingButton, applyForceButton;


        void Awake()
        {
            CursorOnTargetDetector.TargetLocked.AddListener(OnLocked);
            CursorOnTargetDetector.TargetLost.AddListener(OnLost);

            noTargetPanel.SetActive(true);

            applyForceButton.onClick.AddListener(OnForceApply);
            applyHeadingButton.onClick.AddListener(OnHeadingApply);
        }


        private void OnForceApply()
        {
            if (
                CursorOnTargetDetector.CurrentTargetID == null ||
                !HayatBattleshipCalculator.Object.Objects.TryGetValue(CursorOnTargetDetector.CurrentTargetID, out var obj) ||
                !obj.gameObject.TryGetComponent<ManualMovementController>(out var controller)
            ) { return; }

            Vector2 input = new(float.Parse(xValueTextarea.text), float.Parse(yValueTextarea.text));
            switch (forceTypeDropdown.value)
            {
                case 0:
                    controller.ApplyForce(input);
                    break;
                case 1:
                    controller.OverrideVelocity(input);
                    break;
                case 2:
                    controller.ApplyForce(input, true);
                    break;
                case 3:
                    controller.OverrideVelocity(input, true);
                    break;
            }
        }


        private void OnHeadingApply()
        {
            if (
                CursorOnTargetDetector.CurrentTargetID == null ||
                !HayatBattleshipCalculator.Object.Objects.TryGetValue(CursorOnTargetDetector.CurrentTargetID, out var obj) ||
                !obj.gameObject.TryGetComponent<ManualMovementController>(out var controller)
            ) { return; }

            controller.SetHeading(-float.Parse(headingTextarea.text));
        }


        private void OnLocked(string id)
        {
            xValueTextarea.text = "0";
            yValueTextarea.text = "0";
            noTargetPanel.SetActive(false);

            if (
                CursorOnTargetDetector.CurrentTargetID == null ||
                !HayatBattleshipCalculator.Object.Objects.TryGetValue(CursorOnTargetDetector.CurrentTargetID, out var obj)
            ) { return; }

            headingTextarea.text = (-obj.transform.localRotation.eulerAngles.z).ToString();
        }


        private void OnLost(string id)
        {
            noTargetPanel.SetActive(true);
        }


        void OnDestroy()
        {
            CursorOnTargetDetector.TargetLocked.RemoveListener(OnLocked);
            CursorOnTargetDetector.TargetLost.RemoveListener(OnLost);
        }
    }
}