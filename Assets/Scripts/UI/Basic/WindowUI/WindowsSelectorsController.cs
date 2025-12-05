using UnityEngine;

namespace WindowsUI
{
    public class WindowsSelectorsController : MonoBehaviour
    {
        [SerializeField] private WindowSelector selectorPrefab;

        private WindowsManager manager;


        void Awake()
        {
            manager = GameObject.FindGameObjectWithTag(WindowsManager.TAG).GetComponent<WindowsManager>();
        }


        void Start()
        {
            foreach (var item in manager.windows)
            {
                var i = Instantiate(selectorPrefab.gameObject, transform);

                i.GetComponent<WindowSelector>().windowForSelection = item;
            }
        }
    }
}