using Assets.Scripts.Poi;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Experience
{
    public enum ExperienceTypeEnum
    {
        ET_VR_Journey,
        ET_VR_Explore,
        ET_AR_Model,
        ET_None,
    }

    [Serializable]
    public class ExperienceData : ScriptableObject
    {
        public ExperienceTypeEnum ExperienceType = ExperienceTypeEnum.ET_None;
        public SkyboxType Skybox = SkyboxType.Earth_Day;
        public List<ExperienceElement> ExperienceElements = new List<ExperienceElement>();
        public PoiData PoiData;
        public AudioData AudioData;
    }
}
