using Assets.Scripts.Poi;
using UnityEngine;

namespace Assets.Scripts.Experience
{
    public class SetPoisActiveEventController : MonoBehaviour
    {
        public void SetPoisActive()
        {
            PoiTrigger.PoisAreActive = true;
        }
    }
}
