using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    public class ExperienceTagging : MonoBehaviour
    {
        [MenuItem("LI/Tag Main Animator")]
        static void TagMainAnimator()
        {
           var gameObject = Selection.activeGameObject as GameObject;
           gameObject.tag = "MainAnimator";
        }

        [MenuItem("LI/Tag Poi")]
        static void TagPoi()
        {
           var gameObject = Selection.activeGameObject as GameObject;
           gameObject.tag = "PoiTrigger";
        }

        [MenuItem("LI/Tag Experience Element")]
        static void TagExperienceElement()
        {
           var gameObject = Selection.activeGameObject as GameObject;
           gameObject.tag = "ExperienceElement";
        }

        [MenuItem("LI/Tag Camera Mount")]
        static void TagCameraMount()
        {
           var gameObject = Selection.activeGameObject as GameObject;
           gameObject.name = "CameraMount";
           gameObject.tag = "ExperienceCameraAnchor";
        }
    }
}
