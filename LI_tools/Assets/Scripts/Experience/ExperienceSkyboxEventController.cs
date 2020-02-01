using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Experience
{
    public enum SkyboxType
    {
        //None = 
        Earth_Day = 0,
        Space = 1,
    }

    public class ExperienceSkyboxEventController : MonoBehaviour
    {
        public void SetSkyboxDay(float time)
        {
            var vr = FindObjectOfType<VRExperienceController>();
            Assert.IsNotNull(vr);
            vr.SetSkybox(SkyboxType.Earth_Day, time);
        }

        public void SetSkyboxSpace(float time)
        {
            var vr = FindObjectOfType<VRExperienceController>();
            Assert.IsNotNull(vr);
            vr.SetSkybox(SkyboxType.Space, time);
        }
    }
}
