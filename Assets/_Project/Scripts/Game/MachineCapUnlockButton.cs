using System;
using UnityEngine;

namespace Project.Game
{
    public class MachineCapUnlockButton : MonoBehaviour
    {
        [SerializeField] private MachineButton _button;
        [SerializeField] private MachineCap _cap;

        private void OnEnable()
        {
            _button.OnClick += OnClick;
        }

        private void OnDisable()
        {
            _button.OnClick -= OnClick;
        }

        private void OnClick()
        {
            _button.Interactable = false;
            _cap.Release();
        }
    }
}