using Gameplay.Players;
using UnityEngine;

namespace Models.Games.Bullets
{
    public struct BulletCollision
    {
        public AbstractBullet Bullet { get; private set; }  
        public GameObject Target { get; private set; }  
        public float Damage { get; private set; }

        public BulletCollision(AbstractBullet bullet, GameObject target, float damage)
        {
            Bullet = bullet;
            this.Target = target;
            this.Damage = damage;
        }
    }
}
