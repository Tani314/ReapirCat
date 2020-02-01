using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts.Poi
{
    public enum PoiEvent
    {
        None,
        Test,
        Test2,        
    }

    public class PoiEventListener : MonoBehaviour
    {
        public PoiEvent PoiEvent = PoiEvent.None;

        public GameObject PoiPrefab;

        private PoiEventBroadcaster _broadcaster;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            var animator = GameObject.FindGameObjectWithTag("MainAnimator");
            Assert.IsNotNull(animator);
            
            _broadcaster = animator.GetComponent<PoiEventBroadcaster>();
            if (_broadcaster == null) _broadcaster = animator.AddComponent<PoiEventBroadcaster>();
            _broadcaster.OnBroadcastPoiEvent += CreatePoi;
        }

        private void CreatePoi(PoiEvent poiEvent)
        {
            if (poiEvent != PoiEvent) return;
            _broadcaster.OnBroadcastPoiEvent -= CreatePoi;
            Instantiate(PoiPrefab);
        }
    }
}
