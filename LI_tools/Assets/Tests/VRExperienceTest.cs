using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;

namespace Tests
{
    public class VRTestScript
    {
        [Test]
        public void HasExperienceElement()
        {
            //EditorSceneManager.OpenScene("Assets/Scenes/VRExperience.unity", OpenSceneMode.Single);
            var experienceElement = GameObject.FindGameObjectWithTag("ExperienceElement");
            Assert.IsNotNull(experienceElement);
        }

        [Test]
        public void HasMainAnimator()
        {
            var experienceElement = GameObject.FindGameObjectWithTag("ExperienceElement");
            var mainAnimator = GameObject.FindGameObjectWithTag("MainAnimator");
            var experienceElementAnimator = experienceElement.GetComponent<Animator>();
            var hasMainAnimator = (mainAnimator != null) || (experienceElementAnimator != null);
            Assert.IsTrue(hasMainAnimator);
        }

        [Test]
        public void HasCameraMount()
        {
            var cameraMount = GameObject.FindGameObjectWithTag("ExperienceCameraAnchor");
            Assert.IsNotNull(cameraMount);
        }

        [Test]
        public void HasPoiTrigger()
        {
            var poi = GameObject.FindGameObjectWithTag("PoiTrigger");
            Assert.IsNotNull(poi);
        }
    }
}
