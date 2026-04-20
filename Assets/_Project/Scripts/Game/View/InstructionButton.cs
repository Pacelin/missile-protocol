using UnityEngine;

namespace Project.Game
{
    public class InstructionButton : MonoBehaviour
    {
        [SerializeField] private MachineButton _button;
        [SerializeField] private InstructionCanvas _instruction;
        
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
            _instruction.Open();
        }
    }
}