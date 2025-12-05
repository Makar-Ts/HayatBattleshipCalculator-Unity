using System.Collections.Generic;
using UnityEngine;

namespace HayatBattleshipCalculator
{
    [RequireComponent(typeof(HayatBattleshipCalculator.Object))]
    public class ObjectInfoCollector : MonoBehaviour
    {
        protected virtual Object obj { get; set; }


        protected virtual void Start()
        {
            obj = GetComponent<HayatBattleshipCalculator.Object>();
        }


        public virtual Dictionary<string, string> GetInfo()
        {
            var info = obj.ObjectInfo;

            return new Dictionary<string, string>()
            {
                ["id"] = info.id,
                ["name"] = info.Name,
                ["heading"] 
                    = string.Format("{0}deg", Mathf.Round(obj.transform.rotation.z * -1).ToString()),
                ["x"] = Mathf.Round(obj.transform.position.x).ToString(),
                ["y"] = Mathf.Round(obj.transform.position.y).ToString(),
            };
        }
    }
}
