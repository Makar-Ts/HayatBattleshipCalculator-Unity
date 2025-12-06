using System;
using System.Collections.Generic;
using UnityEngine;

namespace HayatBattleshipCalculator
{
    [RequireComponent(typeof(HayatBattleshipCalculator.DynamicObject))]
    public class DynamicObjectInfoCollector : ObjectInfoCollector
    {
        protected override Object obj { get; set; }

        protected override void Start()
        {
            obj = GetComponent<DynamicObject>();
        }


        public override Dictionary<string, string> GetInfo()
        {
            var rb = ((DynamicObject)obj).Rigidbody;


            var angle = Mathf.Round(Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg - 90);
            if (angle < -180) angle += 360;


            return DictionaryHelper.Merge<string, string>(
                base.GetInfo(), 
                new Dictionary<string, string>()
                {
                    ["speed"] 
                        = string.Format("{0}m/s", Mathf.Round(rb.linearVelocity.magnitude).ToString()),
                    ["speedDirection"] 
                        = string.Format("{0}deg", angle.ToString()),
                }
            );
        }
    }
}
