using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using Utils.Asyncs;

namespace Gameplay.AudioSystems
{
    public class SFX : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private CancellationTokenSource _cancellationTokenSource;
        private ReactiveCommand<SFX> _onSFXEnd = new();

        public IObservable<SFX> OnSFXEnd => _onSFXEnd;

        public void Play(AudioClip audioClip, float pitch = 1f)
        {
            _audioSource.clip = audioClip;
            _audioSource.pitch = pitch;
            _audioSource.Play();

            _cancellationTokenSource.TryCancel();
            _cancellationTokenSource = new();
            WaitSFXEnd(audioClip, pitch, _cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid WaitSFXEnd(AudioClip audioClip, float pitch, CancellationToken token)
        {
            var time = audioClip.length / pitch;
            await UniTask.Delay(Mathf.RoundToInt(time * 1000), cancellationToken: token);
            _onSFXEnd.Execute(this);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.TryCancel();
        }
    }
}
