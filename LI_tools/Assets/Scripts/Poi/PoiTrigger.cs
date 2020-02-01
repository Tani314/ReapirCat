using Assets.Scripts.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Poi
{
    //[RequireComponent(typeof(MeshRenderer))]
    public class PoiTrigger : MonoBehaviour
    {
        public static bool PoisAreActive = false;

        public GameObject PoiPrefab;

        private static GameObject _poiInstance = null;

        public bool HideTrigger = false;

        public GameObject PoiIndicator;

        public float Duration = 3f;

        public bool FirstSetActive = true;
        public bool PoiSelected = false;

		public void OnPoiHighlight()
        {
            if (_poiInstance != null || !PoisAreActive) return;
        }

        public void OnPoiSelect()
        {
            if (_poiInstance != null || !PoisAreActive) return;
            _poiInstance = Instantiate(PoiPrefab, transform.position, Quaternion.identity, transform);

            // Orient Poi
            var canvas = _poiInstance.transform.Find("CanvasPivot");
            if (canvas == null) return;

            canvas.transform.LookAt(Camera.main.transform);
            
            var lineRenderer = _poiInstance.GetComponentInChildren<LineRenderer>();
            var line0 = _poiInstance.transform.FirstChildOrDefault(x => x.name == "Line_0");
            var line1 = canvas.transform.FirstChildOrDefault(x => x.name == "Line_1");

            if (lineRenderer != null && line0 != null && line1 != null)
            {
                lineRenderer.SetPosition(0, line0.position);
                lineRenderer.SetPosition(1, line1.position);
            }
            
            // TODO: clean this up
            Destroy(_poiInstance, Duration);            
        }

        public void OnPoiLeave()
        {
            if (_poiInstance != null || !PoisAreActive) return;
        }

        private void Update()
        {
            GetComponent<Collider>().enabled = PoisAreActive && _poiInstance == null;
            PoiIndicator.transform.LookAt(Camera.main.transform, Camera.main.transform.up);

            var showPoiIndicator = PoisAreActive ? (_poiInstance == null) : false;
            if (PoiIndicator.activeSelf != showPoiIndicator) PoiIndicator.SetActive(showPoiIndicator);
        }
    }
}
