using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Utils.Asyncs;

namespace Gameplay.Enviroments
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private List<BackgroundLayerContainer> _layerContainers = new();

        private Dictionary<Transform, Tween> _tweens = new();
        private Transform _camera;
        private Vector3 _previousPosition;
        private CancellationTokenSource _cancellationTokenSource;

        private void Start()
        {
            _camera = Camera.main.transform;
            _previousPosition = _camera.position;

            foreach (var container in _layerContainers)
                _tweens.Add(container.Layer, null);

            _cancellationTokenSource = new();
            CustomUpdate(_cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid CustomUpdate(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Yield(token);
                var delta = _camera.position.x - _previousPosition.x;
                foreach ( var container in _layerContainers )
                {
                    _tweens[container.Layer].Kill();
                    var currentPosition = container.Layer.position;
                    var targetPosition = currentPosition.x + delta * container.CameraDelta;
                    _tweens[container.Layer] = container.Layer
                        .DOMoveX(targetPosition, container.Speed)
                        .SetSpeedBased(true);
                }
                _previousPosition = _camera.position;
            }
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.TryCancel();
        }
    }
}
