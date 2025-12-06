using System;
using UnityEngine;
using Extensions;

namespace HayatBattleshipCalculator
{
    [RequireComponent(typeof(Rigidbody))]
    public class DynamicObject : HayatBattleshipCalculator.Object
    {
        private Rigidbody rigidbody;
        public Rigidbody Rigidbody
        {
            get { return rigidbody; }
        }

        protected override void Start()
        {
            base.Start();

            rigidbody = GetComponent<Rigidbody>();
        }



        public void ApplyForce(Vector2 vector, Boolean local = false)
        {
            rigidbody.linearVelocity += local ? transform.TransformDirection(vector) : vector;
        }

        public void OverrideVelocity(Vector2 vector, Boolean local = false)
        {
            rigidbody.linearVelocity = local ? transform.TransformDirection(vector) : vector;
        }


        public void SetHeading(float heading)
        {
            transform.localRotation = Quaternion.Euler(0, 0, heading);
        }
    }
}
