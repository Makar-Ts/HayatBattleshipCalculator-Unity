using HayatBattleshipCalculator;
using UnityEngine;

namespace HayatBattleshipCalculatorUI
{
    [RequireComponent(typeof(Slider))]
    public class StepSlider : MonoBehaviour
    {
        private Slider slider;

        void Start()
        {
            slider = GetComponent<Slider>();

            SimulationController.SimuationGoing.AddListener(UpdateSlider);
        }

        void UpdateSlider(SimulationController.SimuationGoingEventData data)
        {
            slider.Value = data.progress;
        }

        void OnDestroy()
        {
            SimulationController.SimuationGoing.RemoveListener(UpdateSlider);
        }
    }
}

