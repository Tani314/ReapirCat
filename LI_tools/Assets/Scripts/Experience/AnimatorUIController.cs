using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace Assets.Scripts.Experience
{
    public class AnimatorUIController : MonoBehaviour
    {
        [SerializeField] private Image _playbackButtonImage;
        [SerializeField] private Text _playbackText;
        [SerializeField] private Sprite _play;
        [SerializeField] private Sprite _pause;
        [SerializeField] private CanvasGroup _playbackUIGroup;
        [SerializeField] private ARSessionOrigin _arSession;
        private Animator _animator;

        private AudioController _audioController;

        private void Start()
        {
            if (_animator != null) return;
            ToggleUIVisibility(true);
        }

        public void InitUI(Animator animator)
        {
            if (animator == null) return;
            _animator = animator;
            _animator.speed = 0f;
            TogglePlaybackUI();
        }

        public void ResetPlane()
        {
            if (_arSession == null) return;
            _arSession.trackablesParent.gameObject.SetActive(_animator.speed == 0f);
        }

        public bool IsPlaying()
        {
            return _animator.speed > 0f;
        }

        public void ToggleUIVisibility(bool visible)
        {
            _playbackUIGroup.alpha = visible ? 1f : 0f;
            _playbackUIGroup.interactable = visible;
        }

        public void TogglePlayback()
        {
            _animator.speed = _animator.speed == 1f ? 0f : 1f;

            _audioController = FindObjectOfType<AudioController>();
            _audioController.SetAudioPlayback(_animator.speed == 1f);

            var particles = FindObjectsOfType<ParticleSystem>();

            foreach (var particle in particles)
            {
                if (_animator.speed == 1f) particle.Play();
                else particle.Pause();
            }

            TogglePlaybackUI();
            ResetPlane();
        }

        public void TogglePlaybackUI()
        {
            _playbackButtonImage.sprite = _animator.speed == 0f ? _play : _pause;
            _playbackText.text = _animator.speed == 0f ? "Play" : "Pause";
        }
    }
}
