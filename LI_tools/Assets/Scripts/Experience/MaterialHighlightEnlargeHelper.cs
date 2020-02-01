using UnityEngine;

namespace Assets.Scripts.Experience
{
    [RequireComponent(typeof(Transform))]
    public class MaterialHighlightEnlargeHelper : MonoBehaviour
    {
        private float _scaleAmount = 1.5f;

        private Transform _transform;
        public Vector3 startingScale;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            startingScale = transform.localScale;
        }

        public void SetStartingScale(Vector3 scale)
        {
            startingScale = scale;
        }

        public void HighlightOn()
        {
            var newScale = startingScale;
            newScale.x = newScale.x * _scaleAmount;
            newScale.z = newScale.z * _scaleAmount;
            _transform.localScale = newScale;
        }

        public void HighlightOff()
        {
            transform.localScale = startingScale;
        }
    }
}
