using TMPro;
using UnityEngine;
using HayatBattleshipCalculator;

namespace HayatBattleshipCalculatorUI
{
    public class ShowObjectInfo : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;


        void Update()
        {
            if (CursorOnTargetDetector.CurrentTargetID == null)
                text.text = "<align=\"center\">-- No Target --";
            else
            {
                var obj = HayatBattleshipCalculator.Object.Objects[CursorOnTargetDetector.CurrentTargetID];
                if (obj == null ||
                    !obj.TryGetComponent<ObjectInfoCollector>(out var info))
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
        }
    }
}
