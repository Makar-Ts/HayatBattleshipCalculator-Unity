using UnityEngine;

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
    }
}
