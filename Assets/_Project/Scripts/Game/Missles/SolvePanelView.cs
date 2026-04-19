using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Game.Missles
{
    public class SolvePanelView : MonoBehaviour
    {
        public MachineButton ApplyButton => _applyButton;
        
        [SerializeField] private MachineSwitchButton[] _numButtons;
        [SerializeField] private MachineSwitchButton _thermal;
        [SerializeField] private MachineSwitchButton _laser;
        [Space]
        [SerializeField] private MachineButton _applyButton;

        public bool IsCorrect(MissleConfiguration missleConfiguration)
        {
            var numbers = new[] { '1', '2', '3', '4', '5', '6', '7', '8' };
            var solveCode = missleConfiguration.SolveCode;
            var requireNumButtons = new HashSet<MachineSwitchButton>();
            var requireSeek = _thermal;

            foreach (var solveSymb in solveCode)
            {
                if (numbers.Contains(solveSymb))
                {
                    var index = Array.IndexOf(numbers, solveSymb);
                    requireNumButtons.Add(_numButtons[index]);
                }
                else if (solveSymb == 'L')
                    requireSeek = _laser;
                else
                    requireSeek = _thermal;
            }

            if (!requireSeek.IsOn)
                return false;

            foreach (var button in _numButtons)
            {
                if (requireNumButtons.Contains(button))
                {
                    if (button.IsOn)
                        continue;
                    return false;
                }

                if (button.IsOn)
                    return false;
            }

            return true;
        }
    }
}