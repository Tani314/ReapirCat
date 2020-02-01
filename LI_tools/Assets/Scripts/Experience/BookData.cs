using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Experience
{
    public enum BookTypeEnum
    {
        BT_Test = 0,
        BT_Moon = 1,
        BT_Galaxies = 2,
        BT_Asteroids = 3,
        BT_TheSun = 4,
        BT_TheEarth = 5,
        BT_GreatWhiteSharks = 6,
        BT_PolarBears = 7,
        BT_India = 8,
        BT_China = 9,
        BT_MedicalRobots = 10,
        BT_Airplane = 11,
        BT_Firefighter = 12,
        BT_None = -1,
    }

    [Serializable]
    [CreateAssetMenu(menuName = "LI Assets/Book Data")]
    public class BookData : ScriptableObject
    {
        public BookTypeEnum BookId = BookTypeEnum.BT_None;
        public List<ExperienceData> ExperienceData;
    }
}
