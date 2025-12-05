#nullable enable

using UnityEngine;
using UnityEngine.Events;

namespace HayatBattleshipCalculator
{
    public class SimulationController : MonoBehaviour
    {
        public static readonly string TAG = "SimulationController";
        public static readonly float STEP_TIME = 6f;
        public static int currentStep = 0;

        public static UnityEvent SimuationStarted = new();
        public struct SimuationGoingEventData
        {
            public float stepTime;
            public float simulationTime;
            public float progress;
        }
        public static UnityEvent<SimuationGoingEventData> SimuationGoing = new();
        public static UnityEvent SimuationEnded = new();


        private PhysicsScene? simulation = null;
        private float simulationLifetime = 0f;
        private float timeScale = 0f;


        public float TimeScale
        {
            get { return timeScale; }
        }
        public PhysicsScene? Simulation
        {
            get { return simulation; }
        }


        void Start()
        {
            transform.tag = TAG;
        }


        public void SceneLoaded(PhysicsScene scene)
        {
            simulation = scene;
            simulationLifetime = 0f;
        }


        private SimulationStepInfo simulationStepInfo = new(STEP_TIME, 2f);
        private SimulatedStep? step;

        public float StepProgress
        {
            get { return (step?.lifetime ?? 0) / STEP_TIME; }
        }

        public void Step()
        {
            if (simulation == null || step != null) return;

            step = new(simulationStepInfo);
            currentStep += 1;
            SimuationStarted.Invoke();
        }

        void Update()
        {
            timeScale = step?.Step(Time.deltaTime) ?? timeScale;

            if (timeScale <= 0 && step != null && step.lifetime >= STEP_TIME / 2)
            {
                step = null;

                SimuationEnded.Invoke();
            } 
            else
            {
                SimuationGoing.Invoke(new() {
                    stepTime        = step?.lifetime ?? STEP_TIME,
                    simulationTime  = simulationLifetime,
                    progress        = StepProgress
                });
            }
        }

        private void FixedUpdate()
        {
            if (timeScale <= 0) return;
            simulation?.Simulate(Time.fixedDeltaTime * timeScale);
            simulationLifetime += Time.fixedDeltaTime * timeScale;
        }
    }
}
