using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Common.WrapperPools
{
    public class WrapperPool<T>
           where T : MonoBehaviour
    {
        protected readonly ObjectPool<T> Pool;
        protected readonly Action<T> ActionOnCreate;
        protected readonly T Prefab;
        protected readonly Transform Parent;

        public WrapperPool(T prefab, Action<T> actionOnCreate, Transform parent = null)
        {
            Pool = new ObjectPool<T>(Create, actionOnRelease: ActionOnRelease);
            ActionOnCreate = actionOnCreate;
            Prefab = prefab;
            Parent = parent;
        }

        public virtual T Get()
        {
            var result = Pool.Get();
            result.gameObject.SetActive(true);
            return result;
        }

        public virtual void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            Pool.Release(obj);
        }

        protected void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual T Create()
        {
            var result = UnityEngine.Object.Instantiate(Prefab, Parent);

            if (ActionOnCreate != null)
                ActionOnCreate(result);

            return result;
        }
    }
}
