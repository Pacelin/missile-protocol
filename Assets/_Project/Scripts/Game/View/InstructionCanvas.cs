using Plugins.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Game
{
    public class InstructionCanvas : MonoBehaviour
    {
        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private GameObject[] _pages;

        private int _currentIndex;
        
        private void Awake()
        {
            _currentIndex = 0;
            _leftButton.interactable = false;
            _rightButton.interactable = true;
            _pages[0].SetActive(true);
            for (int i = 1; i < _pages.Length; i++)
                _pages[i].SetActive(false);
        }

        private void OnEnable()
        {
            _leftButton.onClick.AddListener(GoLeft);
            _rightButton.onClick.AddListener(GoRight);
            foreach (var button in _closeButtons)
                button.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _leftButton.onClick.RemoveListener(GoLeft);
            _rightButton.onClick.RemoveListener(GoRight);
            foreach (var button in _closeButtons)
                button.onClick.RemoveListener(Hide);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            AudioSystem.Game_Machines_TutorialOpen.PlayOneShot();
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
            AudioSystem.Game_Machines_TutorialClose.PlayOneShot();
        }

        private void GoRight()
        {
            AudioSystem.Game_Machines_TutorialSlide.PlayOneShot();
            _pages[_currentIndex].SetActive(false);
            _currentIndex++;
            _pages[_currentIndex].SetActive(true);
            _rightButton.interactable = _currentIndex < _pages.Length - 1;
            _leftButton.interactable = true;
        }

        private void GoLeft()
        {
            AudioSystem.Game_Machines_TutorialSlide.PlayOneShot();
            _pages[_currentIndex].SetActive(false);
            _currentIndex--;
            _pages[_currentIndex].SetActive(true);
            _leftButton.interactable = _currentIndex > 0;
            _rightButton.interactable = true;
        }
    }
}