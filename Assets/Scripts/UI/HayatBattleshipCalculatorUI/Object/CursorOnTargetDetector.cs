using HayatBattleshipCalculator;
using UnityEngine;
using UnityEngine.Events;
using Extensions;
using System;

namespace HayatBattleshipCalculatorUI
{
    public class CursorOnTargetDetector : MonoBehaviour
    {
        public static UnityEvent<string> TargetLocked = new();
        public static UnityEvent<string> TargetLost = new();
        public static string CurrentTargetID = null;


        private string lastTargetId = null;
        private Boolean lockTarget = false;
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);


            if (Input.GetMouseButtonDown(1))
            {
                if (lockTarget)
                {
                    lockTarget = false;
                }
                else if (lastTargetId != null)
                {
                    lockTarget = true;
                }
            }
            else if (lockTarget)
            {
                if (lastTargetId == null)
                {
                    lockTarget = false;
                    return;
                }

                var obj = HandleObject(HayatBattleshipCalculator.Object.Objects[lastTargetId].gameObject);

                if (obj == lastTargetId)
                    return;
                else
                    lockTarget = false;
            }


            var sim = GameObject.FindGameObjectWithTag(SimulationController.TAG).GetComponent<SimulationController>().Simulation;


            if (sim != null && (sim?.Raycast(ray.origin, ray.direction, out var hit, 100, LayerMask.GetMask("Object")) ?? false))
            {
                var h = HandleHit(hit);

                if (h != lastTargetId)
                {
                    if (h == null)
                        TargetLost.Invoke(lastTargetId);
                    else
                        TargetLocked.Invoke(h);

                    lastTargetId = h;
                    CurrentTargetID = h;
                }
            }
            else
            {
                if (lastTargetId != null)
                {
                    TargetLost.Invoke(lastTargetId);

                    lastTargetId = null;
                    CurrentTargetID = null;
                }
            }
        }


        private string HandleHit(RaycastHit hit) => HandleObject(hit.transform.gameObject.FindNearestParentWithTag("Object"));

        private string HandleObject(GameObject obj)
        {
            if (obj == null ||
                !obj.TryGetComponent<ObjectInfoCollector>(out var info)) 
                return null;
            
            var data = info.GetInfo();
            if (data.TryGetValue("id", out var id)) 
                return id;
            return null;
        }
    }
}
