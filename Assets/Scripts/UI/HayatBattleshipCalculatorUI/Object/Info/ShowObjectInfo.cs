using TMPro;
using UnityEngine;
using Extensions;
using HayatBattleshipCalculator;

namespace HayatBattleshipCalculatorUI
{
    public class ShowObjectInfo : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;


        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);


            var sim = GameObject.FindGameObjectWithTag(SimulationController.TAG).GetComponent<SimulationController>().Simulation;


            if (sim != null && (sim?.Raycast(ray.origin, ray.direction, out var hit, 100, LayerMask.GetMask("Object")) ?? false))
            {
                Debug.Log("HIT: " + hit.transform.name);
                var obj = hit.transform.gameObject.FindNearestParentWithTag("Object");
                Debug.Log(obj);
                ObjectInfoCollector info;

                if (obj == null ||
                    !obj.TryGetComponent<ObjectInfoCollector>(out info))
                {
                    text.text = "<align=\"center\">-- No Target --";
                    return;
                }

                string t = "";
                foreach (var r in info.GetInfo())
                {
                    t += string.Format("<b>{0}:</b> {1}\n", r.Key, r.Value);
                }

                text.text = t;
            }
            else
            {
                text.text = "<align=\"center\">-- No Target --";
            }
        }
    }
}
