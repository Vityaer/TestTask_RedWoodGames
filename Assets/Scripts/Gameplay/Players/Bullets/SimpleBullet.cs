using UnityEngine;

namespace Gameplay.Players.Bullets
{
    public class SimpleBullet : AbstractBullet
    {
        [SerializeField] private Transform _body;

        public override void SetData(Vector3 startPosition, Vector2 direction)
        {
            var localScale = _body.localScale;
            localScale.y = Mathf.Sign(direction.x);
            _body.localScale = localScale;
            base.SetData(startPosition, direction);
        }
    }
}
