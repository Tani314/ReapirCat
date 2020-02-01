using System;
using UnityEngine;

namespace Assets.Scripts.Poi
{
    public class PoiEventBroadcaster : MonoBehaviour
    {    
        public Action<PoiEvent> OnBroadcastPoiEvent = delegate { };

        public void SendPoiEvent(PoiEvent poiEvent)
        {
            OnBroadcastPoiEvent(poiEvent);
        }
    }
}
