using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Experience
{
    public class SettingsControllerManager : MonoBehaviour
    {
        public static void InitSettingsController(BookTypeEnum bookType, ExperienceTypeEnum experienceType)
        {
            var settingsPrefab = Resources.Load("Settings");
            Assert.IsNotNull(settingsPrefab);
            var go = (GameObject)Instantiate(settingsPrefab);
            var settings = go.GetComponent<ExperienceSettingsController>();

            settings.CurrentBookType = bookType;
            settings.CurrentExperienceType = experienceType;
        }

        public static ExperienceSettingsController GetSettingsController()
        {
            var settings = FindObjectOfType<ExperienceSettingsController>();
            if((settings != null) && (settings.CurrentExperienceType != ExperienceTypeEnum.ET_None
                && settings.CurrentExperienceType != ExperienceTypeEnum.ET_None))
            {
                return settings;
            }
            return null;
        }

        public static void RemoveSettingsIfExist()
        {
            var settings = FindObjectsOfType<ExperienceSettingsController>();
            foreach (var s in settings)
            {
                s.RemoveSettings();
            }
        }
    }
}