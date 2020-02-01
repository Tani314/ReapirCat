using Assets.Scripts.Poi;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Profiling;
using System.Collections.Generic;
//using HighlightPlus;
//using UnityEditor;

namespace Assets.Scripts.Experience
{
    public enum ExperienceDisplayType
    {
        Stereo,
        Mono,
    }

    public class ExperienceSettingsController : MonoBehaviour
    {
        public ExperienceTypeEnum CurrentExperienceType = ExperienceTypeEnum.ET_None;
        public BookTypeEnum CurrentBookType = BookTypeEnum.BT_None;
        public ExperienceDisplayType CurrentExperienceDisplay = ExperienceDisplayType.Mono;

        public Sprite PoiIndicatorImage;
        public BookData ExperienceOverride;
        private BookData bookData;

        private AssetBundle _assetBundle;

        //private HighlightProfile _highlightProfile;

        private void Awake()
        {
            Debug.Log("Creating settings controller");
            DontDestroyOnLoad(this.gameObject);
            Application.lowMemory += OnLowMemory;
        }

        public void LoadExperienceAssets()
        {
            Resources.UnloadUnusedAssets();



            if (ExperienceOverride != null)
            {
                //CompleteProgress();
                StartCoroutine(CompleteProgressOnDelay());
            }
            else
            {
                StartCoroutine(LoadExperienceFromBundle());
            }
        }

        private IEnumerator LoadExperienceFromBundle()
        {
            Debug.Log(SceneManager.sceneCount);
            AssetBundle[] bundles = Resources.FindObjectsOfTypeAll<AssetBundle>();
            Debug.Log("Number of asset bundles loaded" + bundles.Length);
            var path = string.Empty;

#if UNITY_ANDROID
            path = Application.streamingAssetsPath + "/AssetBundles/Android/";
#elif UNITY_IOS
                path = Application.streamingAssetsPath + "/AssetBundles/iOS/";
#endif

            Assert.IsNull(_assetBundle);

            var bundle = AssetBundle.LoadFromFileAsync(Path.Combine(path, CurrentBookType.ToString().ToLower()));

            while (!bundle.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return bundle;

            _assetBundle = bundle.assetBundle;

            if (_assetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                HandleLoadError();
                yield break;
            }


            var bundleAsset = _assetBundle.LoadAssetAsync<BookData>(CurrentBookType.ToString().ToLower());

            while (!bundleAsset.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            bookData = bundleAsset.asset as BookData;
            if (bookData == null)
            {
                Debug.Log("Failed to load book data");
                HandleLoadError();
                yield break;
            }
        }

        private void HandleLoadError()
        {
            Resources.UnloadUnusedAssets();
            AssetBundle.UnloadAllAssetBundles(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PrepareExperience() {
            if (ExperienceOverride != null)
            {
                LoadExperienceFromBook(ExperienceOverride);
            }
            else
            {
                LoadExperienceFromBook(bookData);
            }
        }

        private IEnumerator CompleteProgressOnDelay()
        {
            yield return new WaitForSeconds(2f);
        }

        private void LoadExperienceFromBook(BookData bookData)
        {
            PoiTrigger.PoisAreActive = false;

            var experienceData = bookData.ExperienceData.Find(x => x.ExperienceType == CurrentExperienceType);

			Assert.IsNotNull(experienceData);

            GameObject root = new GameObject("ExperienceRoot");

            var anchor = GameObject.FindGameObjectWithTag("ExperienceAnchor");

            if (anchor != null) root.transform.SetParent(anchor.transform, false);

            foreach (var i in experienceData.ExperienceElements)
            {
                GameObject spawnable = Instantiate(i.Prefab, i.Position, Quaternion.Euler(i.Rot));
                spawnable.transform.SetParent(root.transform, false);
                spawnable.transform.localScale = i.Scale; //Probably don't need a scale
                spawnable.AddComponent<ElementController>();
            }

            var cameraAnchor = GameObject.FindGameObjectWithTag("ExperienceCameraAnchor");

            if (cameraAnchor != null)
            {
                Camera.main.transform.SetParent(cameraAnchor.transform, false);

                var menu = FindObjectOfType<VRMenuLaunchController>();
                menu.transform.SetParent(cameraAnchor.transform, false);

                if (cameraAnchor.transform.rotation.y < -0.5f) 
                {
                    menu.SetLauncherToLowerPosition();
                }
            }

            var mainAnimator = GameObject.FindGameObjectWithTag("MainAnimator");

            // In case the ExperienceElement root also owns the animator
            if (mainAnimator == null)
            {
                mainAnimator = GameObject.FindGameObjectWithTag("ExperienceElement");
            }

            if (mainAnimator !=  null)
            {
                if (CurrentExperienceType == ExperienceTypeEnum.ET_AR_Model)
                {
                    Debug.Log("AR");
                    var animator = mainAnimator.GetComponent<Animator>();
                    var animUiController = FindObjectOfType<AnimatorUIController>();

                    Assert.IsNotNull(animUiController);

                    animUiController.InitUI(animator);
                    animator.gameObject.AddComponent<AudioEventController>();
                }
                else //VR Journey & Explore
                {
                    var vr = FindObjectOfType<VRExperienceController>();
                    Assert.IsNotNull(vr);
                    vr.SetSkybox(experienceData.Skybox);

                    mainAnimator.gameObject.AddComponent<ExperienceEndEventController>();
                    mainAnimator.gameObject.AddComponent<ExperienceSkyboxEventController>();
                    mainAnimator.gameObject.AddComponent<SetPoisActiveEventController>();
                    mainAnimator.gameObject.AddComponent<AudioEventController>();
                }
            }

            var poiData = experienceData.PoiData;

            //var pois = GameObject.FindGameObjectsWithTag("PoiTrigger").ToList();

            var pois = Resources
                .FindObjectsOfTypeAll<Collider>()
                .Where(x => x.CompareTag("PoiTrigger"))
                .ToList();

            var triggersTouched = new List<string>();

            if (poiData != null)
            {
                foreach (var poi in pois)
                {
                    var data = poiData.PoiExperienceElements.Find(x => x.TriggerName == poi.name);

                    if (data == null || triggersTouched.Contains(poi.name)) continue;

                    triggersTouched.Add(poi.name);

                    var colliderReceiver = poi.gameObject.AddComponent<ColliderReceiverController>();

                    var audioEventController = poi.gameObject.AddComponent<AudioEventController>();
                    var poiTrigger = poi.gameObject.AddComponent<PoiTrigger>();
                                        
                    poiTrigger.PoiPrefab = data.Prefab;
                    poiTrigger.HideTrigger = false;
                    poiTrigger.Duration = data.Duration;

                    var poiIndicator = new GameObject();
                    var spriteRenderer = poiIndicator.AddComponent<SpriteRenderer>();
                    poiIndicator.transform.SetParent(poi.gameObject.transform, true);
                    poiIndicator.transform.localPosition = data.IndicatorPosition;
                    poiIndicator.transform.localScale = data.IndicatorScale;
                    poiIndicator.SetActive(false);
                    poiIndicator.name = "PoiIndicator";
                    poiTrigger.PoiIndicator = poiIndicator;
                    spriteRenderer.sprite = PoiIndicatorImage;


                    //var poiHighlight = poiTrigger.gameObject.AddComponent<cakeslice.Outline>();
                    //if (poiHighlight != null) ConfigurePoiHighlight(poiHighlight);

                    //Debug.Log("POI Highlight 1 : " + poiHighlight != null);

                    colliderReceiver.OnHighlight = new UnityEvent();
                    colliderReceiver.OnHighlight.AddListener(poiTrigger.OnPoiHighlight);

                    colliderReceiver.OnSelect = new UnityEvent();
                    colliderReceiver.OnSelect.AddListener(poiTrigger.OnPoiSelect);

                    colliderReceiver.OnLeave = new UnityEvent();
                    colliderReceiver.OnLeave.AddListener(poiTrigger.OnPoiLeave);
                }
            }

            //todo: 2 tier poi highlighters
            // var highlighters = Resources
            //     .FindObjectsOfTypeAll<Renderer>()
            //     .Where(x => x.CompareTag("PoiHighlight"))
            //     .ToList();

            // foreach (var highlighter in highlighters)
            // {
            //     var poiHighlight = highlighter.gameObject.GetComponent<cakeslice.Outline>();

            //     if (poiHighlight == null)
            //     {
            //         poiHighlight = highlighter.gameObject.AddComponent<cakeslice.Outline>();
            //     }

            //     //if (poiHighlight != null) ConfigurePoiHighlight(poiHighlight);

            //     Debug.Log("SUNNY : POI Highlight 2 : " + poiHighlight != null);
            // }

            // Init audio
            var audioControllerGo = new GameObject("AudioController");
            audioControllerGo.transform.SetParent(root.transform);
            var backgroundAudioSource = audioControllerGo.AddComponent<AudioSource>();
            var clipAudioSource = audioControllerGo.AddComponent<AudioSource>();
            var clipAudioSource2 = audioControllerGo.AddComponent<AudioSource>();

            var audioController = audioControllerGo.AddComponent<AudioController>();
            audioController.AudioBackgroundSource = backgroundAudioSource;
            audioController.AudioClipSource = clipAudioSource;
            audioController.AudioClipSource2 = clipAudioSource2;
            audioController.AudioData = experienceData.AudioData;
        }

        public void RemoveSettings()
        {
            Debug.Log("Removing settings controller");
            if (_assetBundle != null) _assetBundle.Unload(true);
            AssetBundle.UnloadAllAssetBundles(true);
            Destroy(gameObject, 1f);
        }

        private void OnLowMemory()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}
