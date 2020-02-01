using UnityEngine;

namespace Assets.Scripts.Poi
{
    public class PoiController : MonoBehaviour
    {
        private float LifeTimeSec = 50f;

        private void Start()
        {
            Destroy(gameObject, LifeTimeSec);

            var root = transform.GetChild(0);

            if (root != null)
            {
                root.gameObject.SetActive(true);
            }
        }
    }
}
