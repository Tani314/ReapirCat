using System;
using UnityEngine;

namespace Assets.Scripts.Experience
{
    public class VRMenuController : MonoBehaviour
    {
        public Action OnHomeSelectAction = delegate { };
        public Action OnCardboardSelectAction = delegate { };
        public Action OnInfoSelectAction = delegate { };
        public Action OnDestroy = delegate { };

        private const int HoverTimeout = 5;
        private float _noHoverTime;
        private bool _isHovering;

        public void OnButtonHover()
        {
            _isHovering = true;
            _noHoverTime = 0f;
        }

        public void OnButtonLeave()
        {
            _isHovering = false;
        }

        public void OnHomeSelect()
        {
            OnHomeSelectAction.Invoke();
        }

        public void OnCardboardSelect()
        {
            OnCardboardSelectAction.Invoke();
        }

        public void OnInfoSelect()
        {
            OnInfoSelectAction.Invoke();
        }
        
        private void Update()
        {
            if (!_isHovering)
            {
                _noHoverTime += Time.deltaTime;
            }

            if (!_isHovering && _noHoverTime > HoverTimeout)
            {
                OnDestroy.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
