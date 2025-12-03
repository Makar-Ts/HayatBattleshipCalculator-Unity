using HayatBattleshipCalculator;
using TMPro;
using UnityEngine;

namespace HayatBattleshipCalculatorUI
{
    [RequireComponent(typeof(TMP_Text))]
    public class StepCounter : MonoBehaviour
    {
        private TMP_Text text;

        void Start()
        {
            text = GetComponent<TMP_Text>();

            SimulationController.SimuationStarted.AddListener(UpdateText);
        }

        void UpdateText()
        {
            text.text = "#"+SimulationController.currentStep;
        }

        void OnDestroy()
        {
            SimulationController.SimuationStarted.RemoveListener(UpdateText);
        }
    }
}

