using UnityEngine;
using UnityEngine.SceneManagement;

namespace HayatBattleshipCalculator
{
    [RequireComponent(typeof(SimulationController))]
    public class SimulationLoader : MonoBehaviour
    {
        public string simulationSceneName;

        private SimulationController controller;

        void Awake()
        {
            controller = GetComponent<SimulationController>();

            LoadSceneParameters param = new(LoadSceneMode.Additive, LocalPhysicsMode.Physics3D);
            Scene scene = SceneManager.LoadScene(simulationSceneName, param);
            
            controller.SceneLoaded(scene.GetPhysicsScene());
        }
    }
}
