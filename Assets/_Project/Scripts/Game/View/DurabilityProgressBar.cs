using UnityEngine;

namespace Project.Game
{
    public class DurabilityProgressBar : MonoBehaviour
    {
        [SerializeField] private DurabilityProgressBarItem[] _items;
        [SerializeField] private float _delayPerItem;
        
        private int _currentAmount;
        
        private void OnEnable()
        {
            G.ShipModel.OnDamage += OnShipDamage;
            G.ShipModel.OnHeal += OnShipHeal;
            
            _currentAmount = G.ShipModel.Durability;
            for (int i = 0; i < _currentAmount; i++)
                _items[i].Setup(true);
            for (int i = _currentAmount; i < _items.Length; i++)
                _items[i].Setup(false);
        }

        private void OnDisable()
        {
            G.ShipModel.OnDamage -= OnShipDamage;
            G.ShipModel.OnHeal -= OnShipHeal;
        }

        private void OnShipHeal()
        {
            var newAmount = G.ShipModel.Durability;
            for (int i = _currentAmount; i < newAmount; i++)
            {
                var delay = (i - _currentAmount) * _delayPerItem;
                _items[i].Appear(delay);
            }

            _currentAmount = newAmount;
        }

        private void OnShipDamage()
        {
            var newAmount = G.ShipModel.Durability;
            for (int i = _currentAmount - 1; i >= newAmount; i--)
            {
                var delay = (_currentAmount - i - 1) * _delayPerItem;
                _items[i].Disappear(delay);
            }
            
            _currentAmount = newAmount;
        }
    }
}