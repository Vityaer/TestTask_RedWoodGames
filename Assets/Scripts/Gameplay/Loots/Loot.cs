using Infrastructures.Common;
using TMPro;
using UniRx;
using UnityEngine;
using Utils;

namespace Gameplay.Loots
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private AudioClip _getUpSFX;

        private int _ammoBonus;
        private ReactiveCommand<Loot> _onPickUp = new();
        private bool _work;

        public ReactiveCommand<Loot> OnPickUp => _onPickUp;
        public int AmmoBonus => _ammoBonus;
        public AudioClip GetUpSFX => _getUpSFX;

        public void SetData(Vector3 position, int ammoBonus)
        {
            _work = true;
            transform.position = position;
            _ammoBonus = ammoBonus;
            _countText.text = $"{ammoBonus}";
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if(!_work)
                return;

            if(LayerUtils.CheckIntersection(collision.gameObject, GameConstants.Fight.PlayerLayer))
                _onPickUp.Execute(this);
        }

        public void Stop()
        {
            _work = false;
        }
    }
}
