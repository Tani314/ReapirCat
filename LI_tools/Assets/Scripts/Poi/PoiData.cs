using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Poi
{
    [Serializable]
    public class PoiData : ScriptableObject
    {
        public List<PoiElement> PoiExperienceElements = new List<PoiElement>();
    }
}
