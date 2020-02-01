using System;
using UnityEngine;

namespace Assets.Scripts.Poi
{
    [Serializable]
    public class PoiElement
    {
        public string TriggerName;
        public GameObject Prefab;
        //public GameObject SpawnPoiPrefab;
        public PoiEvent PoiEvent;
        //public Vector3 Position;
        //public Vector3 Scale;
        //public Vector3 Rot;

        public Vector3 IndicatorPosition;
        public Vector3 IndicatorScale;
        public bool HideTriggerOnPoiSpawn;
        public float Duration;
    }
}