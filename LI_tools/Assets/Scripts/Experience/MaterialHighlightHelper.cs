using UnityEngine;

namespace Assets.Scripts.Experience
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialHighlightHelper : MonoBehaviour
    {
        public Color HighlightColor;

        private MeshRenderer _meshRenderer;
        private Color _startingColor;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _startingColor = _meshRenderer.material.color;
        }

        public void HighlightOn()
        {
            _meshRenderer.material.color = HighlightColor;
        }

        public void HighlightOff()
        {
            _meshRenderer.material.color = _startingColor;
        }
    }
}
