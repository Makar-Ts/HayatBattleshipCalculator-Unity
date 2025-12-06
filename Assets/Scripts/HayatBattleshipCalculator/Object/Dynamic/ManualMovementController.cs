using System;
using UnityEngine;

namespace HayatBattleshipCalculator
{
    [RequireComponent(typeof(DynamicObject))]
    public class ManualMovementController : MonoBehaviour
    {
        private DynamicObject obj;


        void Awake()
        {
            obj = GetComponent<DynamicObject>();
        }


        public void ApplyForce(Vector2 vector, Boolean local = false) => obj.ApplyForce(vector, local);
        public void OverrideVelocity(Vector2 vector, Boolean local = false) => obj.OverrideVelocity(vector, local);


        public void SetHeading(float heading) => obj.SetHeading(heading);
    }
}