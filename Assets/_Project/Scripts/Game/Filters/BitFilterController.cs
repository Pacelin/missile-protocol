using DG.Tweening;
using Plugins.Audio;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Game
{
    public class BitFilterController : MonoBehaviour, IFilterController
    {
        [SerializeField] private Vector2Int _lengthRange;
        [Range(0, 1)]
        [SerializeField] private float _incorrectPercent = 0.2f;
        [SerializeField] private float _incorrectChance = 0.3f;
        [Space]
        [SerializeField] private string _bitFilterCorrectFormat;
        [SerializeField] private string _butFilterIncorrectFormat;
        [SerializeField] private float _offsetByIndex = -0.72f;
        [Space]
        [SerializeField] private Transform _transform;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private MachineButton _moveLeftButton;
        [SerializeField] private MachineButton _moveRightButton;
        [SerializeField] private MachineButton _switchButton;
        
        private BitFilter _filter;
        private int _currentIndex;
        
        private void OnEnable()
        {
            _moveLeftButton.OnClick += MoveLeftButtonClick;
            _moveRightButton.OnClick += MoveRightButtonClick;
            _switchButton.OnClick += SwitchButtonClick;
        }

        private void OnDisable()
        {
            _moveLeftButton.OnClick -= MoveLeftButtonClick;
            _moveRightButton.OnClick -= MoveRightButtonClick;
            _switchButton.OnClick -= SwitchButtonClick;
        }

        private void OnDestroy()
        {
            _transform.DOKill();
        }

        public void StartFilter(IFilterHandler filterHandler)
        {
            var length = Random.Range(_lengthRange.x, _lengthRange.y + 1);
            _filter = BitFilter.New(length,
                (int) (length * _incorrectPercent),
                _incorrectChance,
                _bitFilterCorrectFormat,
                _butFilterIncorrectFormat);
            _filter.SetHandler(filterHandler);

            _text.text = _filter.GetString();
            _currentIndex = 0;
            _transform.localPosition = new Vector3(0, 0, 0);
            gameObject.SetActive(true);
        }

        public void StopFilter()
        {
            gameObject.SetActive(false);
        }

        private void DoMove(int indexOffset)
        {
            var newIndex = Mathf.Clamp(_currentIndex + indexOffset, 0, _filter.GetLength());
            if (newIndex == _currentIndex)
                return;
            
            _currentIndex = newIndex;
            if (_filter.IsOne(_currentIndex))
                AudioSystem.Game_Machines_BitOne.PlayOneShot();
            else
                AudioSystem.Game_Machines_BitZero.PlayOneShot();
            
            _transform.DOKill();
            _transform.DOLocalMoveX(_offsetByIndex * _currentIndex, 0.1f);
        }
        
        private void SwitchButtonClick()
        {
            _filter.Switch(_currentIndex);
            _text.text = _filter.GetString();
            if (_filter.IsOne(_currentIndex))
                AudioSystem.Game_Machines_BitOne.PlayOneShot();
            else
                AudioSystem.Game_Machines_BitZero.PlayOneShot();
        }

        private void MoveRightButtonClick() => DoMove(1);
        private void MoveLeftButtonClick() => DoMove(-1);
    }
}