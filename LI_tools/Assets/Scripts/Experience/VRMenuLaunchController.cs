using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

namespace Assets.Scripts.Experience
{
    public class VRMenuLaunchController : MonoBehaviour
    {
        [SerializeField] private VRExperienceController _vrExperienceController;
        [SerializeField] private GameObject _menuPrefab;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _menuIcon;

        private GameObject _menuInstance;

        private const float PitchAlphaMax = 40f;

        private float _dampVelocityX;
        private float _dampVelocityY;

        private Vector3 _menuOffset = new Vector3(0f, 2f, 0f);

        private bool _launcherIsLow = false;

        public void OnLauncherHighlight()
        {
            Debug.Log("OnLauncherHighlight");
        }

        public void OnLauncherLeave()
        {
            Debug.Log("OnLauncherLeave");
        }

        public void OnLauncherSelect()
        {
            Debug.Log("OnLauncherSelect");
            if (_menuInstance != null) return;
            _menuInstance = Instantiate(_menuPrefab, _collider.transform.position + _menuOffset, Camera.main.transform.rotation, Camera.main.transform.parent);

            var menu = _menuInstance.GetComponent<VRMenuController>();

            menu.OnDestroy += () =>
            {
                SetLauncher(true);
            };

            menu.OnHomeSelectAction += OnHomeSelect;
            menu.OnCardboardSelectAction += OnCardboardSelect;
            menu.OnInfoSelectAction += OnReplaySelect;

            if (_launcherIsLow) menu.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

            SetLauncher(false);
        }

        public void ShowLauncher(bool show)
        {
            if (_menuInstance != null)
            {
                Destroy(_menuInstance);
            }
            SetLauncher(show);
        }

        public void SetLauncherToLowerPosition()
        {
            var newMenuLocation = transform.localPosition;
            newMenuLocation.y = -30;
            transform.localPosition = newMenuLocation;
            _menuIcon.transform.localEulerAngles = new Vector3(30, 180, 0);
            var scale = new Vector3(0.6f, 0.6f, 0.6f);
            _menuIcon.transform.localScale = scale;
            _menuIcon.GetComponent<MaterialHighlightEnlargeHelper>().SetStartingScale(scale);
            _launcherIsLow = true;
        }

        public void OnHomeSelect()
        {
            _vrExperienceController.OnHomeButtonPress();
        }

        public void OnCardboardSelect()
        {
            _vrExperienceController.OnCardboardTogglePress();
        }

        public void OnReplaySelect()
        {
            _vrExperienceController.OnReplayButtonPress();
        }

        public void SetLauncher(bool enable)
        {
            _renderer.enabled = enable;
            _collider.enabled = enable;
        }

        private void LateUpdate()
        {
            if (_renderer.enabled)
            {
                //var newRotY = Mathf.SmoothDampAngle(transform.localRotation.eulerAngles.y, Camera.main.transform.localRotation.eulerAngles.y, ref _dampVelocityY, 0.2f);
                //transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, newRotY, transform.localRotation.eulerAngles.z);

                if (_menuInstance != null)
                {
                    _menuInstance.transform.rotation = Camera.main.transform.rotation;
                }
            }
        }
    }
}
