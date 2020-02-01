using Assets.Scripts.Experience;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using System.Collections;
using Assets.Scripts.Poi;

namespace Assets.Scripts
{
    public class ARExperienceController : MonoBehaviour
    {
        [SerializeField] private ARRaycastManager _arRaycastManager;
        [SerializeField] private AnimatorUIController animatorUIController;

        private const float ScaleRate = 0.1f;
        private const float ScaleMax = 2f;
        private const float ScaleMin = 0.2f;

        private Vector2 _startingFingerPosition0 = Vector2.zero;
        private Vector2 _startingFingerPosition1 = Vector2.zero;

        private Vector2 _movingFingerPosition0 = Vector2.zero;
        private Vector2 _movingFingerPosition1 = Vector2.zero;

        private bool _firstDoubleTap = false;

        private List<Renderer> _spawnedRenderers;

        private AudioController _audioController;

        public void OnHomeButtonPress()
        {
            var settings = FindObjectsOfType<ExperienceSettingsController>();
            foreach (var s in settings)
            {
                s.RemoveSettings();
            }

            SceneManager.LoadScene("ScanBook", LoadSceneMode.Single);
        }

        public void OnReplayButtonPress()
        {
            StartCoroutine(Replay());
        }

        IEnumerator Replay()
        {
            foreach (Transform element in this.transform)
            {
                GameObject.Destroy(element.gameObject);
            }
            yield return null;
            
            var settings = SettingsControllerManager.GetSettingsController();
            Assert.IsNotNull(settings);
            settings.PrepareExperience();
            animatorUIController.ResetPlane();            
        }

        private void Start()
        {
            Screen.orientation = ScreenOrientation.Portrait;

            OnReplayButtonPress();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            SetSpawnedRenderers(true);
            animatorUIController.ToggleUIVisibility(true);
        }

        private void Update()
        {
            if (Input.touchCount < 1) return;

            Touch touch = Input.GetTouch(0);

            // Cache touch positions

            // 1st finger state
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _startingFingerPosition0 = Input.GetTouch(0).position;
                    _movingFingerPosition0 = Input.GetTouch(0).position;
                    _startingFingerPosition1 = Input.GetTouch(0).position;
                    _movingFingerPosition1 = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _movingFingerPosition0 = Input.GetTouch(0).position;
                }
            }

            // 2nd finger state
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    _startingFingerPosition1 = Input.GetTouch(1).position;
                    _movingFingerPosition1 = Input.GetTouch(1).position;
                }
                else if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    _movingFingerPosition1 = Input.GetTouch(1).position;
                }
            }

            // Handle touches
            if (Input.touchCount == 1) // double tap
            {
                if (touch.tapCount != 2) return;
                UpdateRaycaster(touch, true);
                animatorUIController.ToggleUIVisibility(true);
            }
            else if (Input.touchCount == 2) // Scale
            {
                if (Input.GetTouch(0).phase != TouchPhase.Moved && Input.GetTouch(1).phase != TouchPhase.Moved) return;

                var originalMag = (_startingFingerPosition1 - _startingFingerPosition0).magnitude;
                var currentMag = (_movingFingerPosition1 - _movingFingerPosition0).magnitude;
                var deltaMag = currentMag - originalMag;

                if (deltaMag > 0f)
                {
                    var newScale = transform.localScale.x + ScaleRate;
                    newScale = Mathf.Min(newScale, ScaleMax);
                    transform.localScale = Vector3.one * newScale;

                }
                else if (deltaMag < 0f)
                {
                    var newScale = transform.localScale.x - ScaleRate;
                    newScale = Mathf.Max(newScale, ScaleMin);
                    transform.localScale = Vector3.one * newScale;
                }
            }
        }

        private void UpdateRaycaster(Touch touch, bool useLookAt)
        {
            var s_Hits = new List<ARRaycastHit>();

            if (_arRaycastManager.Raycast(touch.position, s_Hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = s_Hits[0].pose;
                transform.position = hitPose.position;

                if (useLookAt)
                {
                    Vector3 targetPostition =
                    new Vector3(Camera.main.transform.position.x,
                                transform.position.y,
                                Camera.main.transform.position.z);

                    this.transform.LookAt(targetPostition);
                }
            }
        }


        private void SetSpawnedRenderers(bool enabled)
        {
            _spawnedRenderers = new List<Renderer>(transform.GetComponentsInChildren<Renderer>());
            if (_spawnedRenderers != null) _spawnedRenderers.ForEach(x => x.enabled = enabled);
        }
        
        //private void OnDisable()
        //{
        //    SettingsControllerManager.ClearSettings();
        //}
    }
}
