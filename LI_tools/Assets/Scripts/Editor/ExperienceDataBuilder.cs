using Assets.Scripts.Experience;
using Assets.Scripts.Poi;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [ExecuteInEditMode]
    public class ExperienceDataBuilder : MonoBehaviour
    {
        [MenuItem("Assets/Build POI Experience Data")]
        private static void BuildPoiExperienceData()
        {
            var elements = GameObject.FindGameObjectsWithTag("PoiTrigger");     
            var poiData = ScriptableObject.CreateInstance<PoiData>();

            foreach (var poiElement in elements)
            {
                var experienceElement = new PoiElement
                {
                    TriggerName = poiElement.name,
                };

                poiData.PoiExperienceElements.Add(experienceElement);

                //var triggerElement = poiElement.GetComponent<PoiTrigger>();

                //if (triggerElement != null)
                //{
                //    var prefab = PrefabUtility.GetCorrespondingObjectFromSource(poiElement);
                //    var prefabRoot = PrefabUtility.GetCorrespondingObjectFromSource(prefab);

                //    var experienceElement = new PoiElement
                //    {
                //        TriggerName = poiElement.name,
                //        Prefab = prefab,
                //        SpawnPoiPrefab = triggerElement.PoiPrefab,
                //        Position = poiElement.transform.position,
                //        Scale = poiElement.transform.localScale,
                //        Rot = poiElement.transform.rotation.eulerAngles
                //    };

                //    poiData.PoiExperienceElements.Add(experienceElement);
                //}
                //else
                //{
                //    var eventElement = poiElement.GetComponent<PoiEventListener>();

                //    var experienceElement = new PoiElement
                //    {
                //        PoiEvent = eventElement.PoiEvent,
                //        SpawnPoiPrefab = eventElement.PoiPrefab,
                //        Position = eventElement.transform.position,
                //        Scale = eventElement.transform.localScale,
                //        Rot = eventElement.transform.rotation.eulerAngles
                //    };

                //    poiData.PoiExperienceElements.Add(experienceElement);
                //}   
            }

            AssetDatabase.CreateAsset(poiData, "Assets/PoiData.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Create AudioData Data")]
        public static void CreateAudioData()
        {
            var audioData = ScriptableObject.CreateInstance<AudioData>();
            AssetDatabase.CreateAsset(audioData, "Assets/AudioData.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Build VR Journey Experience Data")]
        private static void BuildVRJourneyExperienceData()
        {
            BuildExperienceData(ExperienceTypeEnum.ET_VR_Journey);
        }

        [MenuItem("Assets/Build VR Explore Experience Data")]
        private static void BuildVRJourneyExploreData()
        {
            BuildExperienceData(ExperienceTypeEnum.ET_VR_Explore);
        }

        [MenuItem("Assets/Build AR Model Experience Data")]
        private static void BuildARModelExperienceData()
        {
            BuildExperienceData(ExperienceTypeEnum.ET_AR_Model);
        }
        
        private static void BuildExperienceData(ExperienceTypeEnum experienceType)
        {
            var experienceElements = GameObject.FindGameObjectsWithTag("ExperienceElement");

            var experienceData = ScriptableObject.CreateInstance<ExperienceData>();
            experienceData.ExperienceType = experienceType;

            foreach (var sceneElement in experienceElements)
            {
                var experienceElement = new ExperienceElement
                {                    
                    Prefab = PrefabUtility.GetCorrespondingObjectFromSource(sceneElement),
                    Position = sceneElement.transform.position,
                    Scale = sceneElement.transform.localScale,
                    Rot = sceneElement.transform.rotation.eulerAngles
                };

                experienceData.ExperienceElements.Add(experienceElement);
            }
            
            AssetDatabase.CreateAsset(experienceData, "Assets/" + experienceType.ToString() + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}