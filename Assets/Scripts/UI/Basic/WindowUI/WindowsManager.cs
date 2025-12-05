using System.Collections.Generic;
using UnityEngine;

namespace WindowsUI
{
    public class WindowsManager : MonoBehaviour
    {
        public static string TAG = "WindowsManager";


        public readonly List<Window> windows = new();

        void Awake()
        {
            foreach (Transform win in transform)
            {
                if (win.TryGetComponent<Window>(out var component)) windows.Add(component);
            }
        }
    }
}
