using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.XR;

namespace Assets.Scripts.Experience
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Animator))]
    public class ElementController : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private Animator _animator;

        private void Start()
        {
            //_meshRenderer = GetComponent<MeshRenderer>();
            //_animator = GetComponent<Animator>();

            //Assert.IsNotNull(_meshRenderer, "Mesh Renderer Missing");
            //Assert.IsNotNull(_animator, "Animator Missing");

            //var collider = gameObject.AddComponent<ColliderReceiverController>();
            //collider.OnSelect = new UnityEvent();
            //collider.OnSelect.AddListener(OnSelect);
        }

        void Update()
        {
            return;

            if (XRSettings.enabled) return;

            if (Input.touchCount >= 1)
            {
                if (Input.GetTouch(0).phase != TouchPhase.Began) return;

                Ray ray;
                Vector2 vTouchPos = Input.GetTouch(0).position;
                ray = Camera.main.ScreenPointToRay(vTouchPos);

                RaycastHit vHit;
                if (Physics.Raycast(ray.origin, ray.direction, out vHit))
                {
                    if (vHit.transform.gameObject.GetInstanceID() == gameObject.GetInstanceID())
                    {
                        OnSelect();
                        SetRandomMaterialColor();
                    }
                }
            }
        }

        public void OnSelect()
        {
            _animator.speed = _animator.speed == 1f ? 0f : 1f;
        }

        private void SetRandomMaterialColor()
        {
            _meshRenderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }
}
