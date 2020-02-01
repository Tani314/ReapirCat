using Assets.Scripts.Experience;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Assets.Scripts.Experience
{
    public class VRExperienceController : MonoBehaviour
    {
        [SerializeField] private Material _skyboxMaterial;
        [SerializeField] private Transform _experienceAnchor;
        [SerializeField] private Camera _camera;
        [SerializeField] private VRMenuLaunchController _menuController;

        private bool _enableManualHeadTracking = false;

        public void SetSkybox(SkyboxType skybox, float time = 0)
        {
            _skyboxMaterial.DOFloat((int)skybox, "_Blend", time);
        }

        public void OnHomeButtonPress()
        {
        }

        public void OnReplayButtonPress()
        {
            StartCoroutine(ResetScene());
        }

        IEnumerator ResetScene()
        {
            _camera.transform.SetParent(null, false);
            _menuController.transform.SetParent(null, false);
            yield return null;
            foreach (Transform element in _experienceAnchor.transform)
            {
                GameObject.Destroy(element.gameObject);
            }
            yield return null;
            var settings = SettingsControllerManager.GetSettingsController();
            Toggle3DLauncher();
            Assert.IsNotNull(settings);
            settings.PrepareExperience();
        }

        public void OnCardboardTogglePress()
        {
            //if (Input.GetTouch(0).phase != TouchPhase.Ended) return;
            if (XRSettings.enabled) _enableManualHeadTracking = true;
            else SwitchToVrMode();
            Toggle3DLauncher();
        }

        private void Start()
        {
            Screen.orientation = ScreenOrientation.Landscape;

            var settings = SettingsControllerManager.GetSettingsController();
            Debug.Log("starting scene");
            Assert.IsNotNull(settings);

            SwitchToVrMode(settings.CurrentExperienceDisplay == ExperienceDisplayType.Mono);
            settings.PrepareExperience();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyUp(KeyCode.R))
            {
                OnReplayButtonPress();
            }
#endif

            if (_enableManualHeadTracking)
            {
                XRSettings.enabled = false;
                Camera.main.ResetAspect();
                Camera.main.GetComponent<Transform>().localRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("Exit cardboard");
                OnHomeButtonPress();
            }
        }

        private void Toggle3DLauncher()
        {
            _menuController.ShowLauncher(!_enableManualHeadTracking);
        }

        private void SwitchToVrMode(bool enableManualTracking = false)
        {
            StartCoroutine(SwitchToVR(enableManualTracking));
        }

        private IEnumerator SwitchToVR(bool enableManualTracking)
        {
            _enableManualHeadTracking = enableManualTracking;
            string desiredDevice = "cardboard";
            Camera.main.transform.localRotation = Quaternion.identity;
            if (string.Compare(XRSettings.loadedDeviceName, desiredDevice, true) != 0)
            {
                XRSettings.LoadDeviceByName(desiredDevice);
                yield return null;
            }
            XRSettings.enabled = true;
            Toggle3DLauncher();
        }

        private void SwitchTo2DMode(Action action)
        {
            StartCoroutine(SwitchTo2D(action));
        }

        private IEnumerator SwitchTo2D(Action act)
        {
            XRSettings.LoadDeviceByName(string.Empty);
            yield return null;
            XRSettings.enabled = false;
            ResetCameras();
            yield return null;
            act();
        }

        private void ResetCameras()
        {
            for (int i = 0; i < Camera.allCameras.Length; i++)
            {
                Camera camera = Camera.allCameras[i];
                if (camera.enabled)
                {
                    camera.transform.localPosition = Vector3.zero;
                    camera.transform.localRotation = Quaternion.identity;
                }
            }
            Camera.main.ResetAspect();
        }
    }
}
