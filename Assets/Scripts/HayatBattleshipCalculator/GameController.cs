using System.Collections.Generic;
using UnityEngine;

namespace HayatBattleshipCalculator
{
    public class GameController : MonoBehaviour
    {
        public static readonly string TAG = "GameController";


        void Start()
        {
            transform.tag = TAG;

            Object.ObjectCreated.AddListener((string id) => 
            {
                Debug.Log("created " + id);
            });

            Object.ObjectDeleted.AddListener((string id) => 
            {
                Debug.Log("deleted " + id);
            });
        }


        public void StartStep()
        {
            GameObject.FindGameObjectWithTag(SimulationController.TAG).GetComponent<SimulationController>().Step();
        }
    }
}

