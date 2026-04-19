using UnityEngine;

namespace Project.Game
{
    public class RotateButton : MonoBehaviour
    {
        [SerializeField] private MachineButton _button;
        [SerializeField] private int _sign;
        
        private void OnEnable()
        {
            _button.OnPress += OnPress;
            _button.OnRelease += OnRelease;
        }

        private void OnDisable()
        {
            _button.OnPress -= OnPress;
            _button.OnRelease -= OnRelease;
        }

        private void OnRelease()
        {
            G.ShipModel.SetRotateSign(0);
        }

        private void OnPress()
        {
            G.ShipModel.SetRotateSign(_sign);
        }
    }
}