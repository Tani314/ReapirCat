using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Experience
{
    public class ExperienceEndEventController : MonoBehaviour
    {
        public void ExperienceEnd()
        {
            var vr = FindObjectOfType<VRExperienceController>();
            Assert.IsNotNull(vr);

            vr.OnHomeButtonPress();
        }
    }
}
