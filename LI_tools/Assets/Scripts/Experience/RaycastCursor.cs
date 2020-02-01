using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Experience
{
    public class RaycastCursor : MonoBehaviour
    {
        [SerializeField] private Image _ring;
        [SerializeField] private Image _cursor;
        [SerializeField] private AudioSource _audioSource;
        private List<ColliderReceiverController> _cachedColliders = new List<ColliderReceiverController>();
        private const float SelectionTime = 1.5f;
        private float _currentTime = 0f;

        private void Update()
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit vHit;

            if (Physics.Raycast(ray, out vHit, Mathf.Infinity, 1 << 9)) //LayerMask "Collider" = 9
            {
                Debug.Log("Hit: " + vHit.collider.transform.gameObject.name);

                var collider = vHit.collider.transform.GetComponent<ColliderReceiverController>();

                if (collider == null)
                {
                    ClearColliders();
                    return;
                }

                if (collider && !_cachedColliders.Contains(collider))
                {
                    ClearColliders();
                    _cachedColliders.Add(collider);
                    if (collider.OnHighlight != null)
                    {
                        _audioSource.Play();
                        collider.OnHighlight.Invoke();
                    }
                }
                else if (_currentTime > SelectionTime)
                {
                    if (_cachedColliders.Count > 0)
                    {
                        var activeCollider = _cachedColliders[_cachedColliders.Count - 1];
                        if (activeCollider.OnSelect != null) activeCollider.OnSelect.Invoke();
                        ClearColliders();
                    }
                }
                else if (_cachedColliders.Count > 0)
                {
                    _currentTime += Time.deltaTime;
                    _ring.fillAmount = _currentTime / SelectionTime;
                    //transform.position = vHit.point;
                    //transform.forward = vHit.normal;
                }
            }
            else if (_cachedColliders.Count > 0)
            {
                ClearColliders();
            }
            //Color cursorColor = _cursor.color;
            //cursorColor.a = _cachedColliders.Count > 0 ? 0f : 0.6f;
            //_cursor.color = cursorColor;
        }

        private void ClearColliders()
        {
            foreach (var collider in _cachedColliders)
            {
                if (collider == null || collider.OnLeave == null) continue;
                collider.OnLeave.Invoke();
            }

            _currentTime = 0f;
            _ring.fillAmount = 0f;

            _cachedColliders.Clear();

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}