using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HayatBattleshipCalculator
{
    public struct BasicObjectInfo
    {
        public string id;
        private string visibleName;

        public string Name
        {
            readonly get { return id ?? visibleName; }
            set { visibleName = value; }
        }
    }


    public class Object : MonoBehaviour
    {
        /* --------------------------------- Static --------------------------------- */


        private static Dictionary<string, Object> _objects = new();
        public static Dictionary<string, Object> Objects
        {
            get { return _objects; }
        }

        public static UnityEvent<string> ObjectCreated = new();
        public static UnityEvent<string> ObjectDeleted = new();



        /* ---------------------------------- Other --------------------------------- */


        private BasicObjectInfo objectInfo = new();


        private GameController controller;

        void Start()
        {
            controller = GameObject.FindGameObjectWithTag(GameController.TAG).GetComponent<GameController>();

            objectInfo.id ??= Guid.NewGuid().ToString();
            _objects.Add(objectInfo.id, this);

            ObjectCreated.Invoke(objectInfo.id);
        }

        void OnDestroy()
        {
            _objects.Remove(objectInfo.id);

            ObjectDeleted.Invoke(objectInfo.id);
        }
    }
}

