using Project.Game.Missles;
using UnityEngine;

namespace Project.Game
{
    public class SignalScreenController : MonoBehaviour
    {
        public GameObject WhenNothing => _whenNothing;
        public BitFilterController BitFilterController => _bitFilterController;
        public WaveFilterController WaveFilterController => _waveFilterController;
        public MissleViewGroup Missles => _missles;
        public MachineButton SnapTargetButton => _snapTargetButton;
        public SolvePanelView SolvePanel => _solvePanel;

        public GameObject WhenTargetLost => _whenTargetLost;
        public GameObject WhenSolveFilter => _whenSolveFilter;
        public GameObject WhenTargetDestroyed => _whenTargetDestroyed;
        public GameObject WhenTargetError => _whenTargetError;
        public float MessageDuration => _messageDuration;
        
        [SerializeField] private GameObject _whenNothing;
        [SerializeField] private BitFilterController _bitFilterController;
        [SerializeField] private WaveFilterController _waveFilterController;
        [SerializeField] private MissleViewGroup _missles;
        [Space]
        [SerializeField] private GameObject _whenTargetLost;
        [SerializeField] private GameObject _whenSolveFilter;
        [SerializeField] private GameObject _whenTargetDestroyed;
        [SerializeField] private GameObject _whenTargetError;
        [SerializeField] private float _messageDuration;
        [Space]
        [SerializeField] private MachineButton _snapTargetButton;
        [SerializeField] private MachineSliderHandle _radarSlider;
        [SerializeField] private SolvePanelView _solvePanel;

        private SignalScreenState _state;
        private bool _sliderHandleMoved;
        
        private void Awake()
        {
            _state = new SignalScreenSetupState(this);
            _state.OnEnter();
            _sliderHandleMoved = false;
            _radarSlider.OnValueChanged += OnRadarSliderValueChanged;
        }

        private void OnDestroy()
        {
            _state?.OnExit();
            _radarSlider.OnValueChanged -= OnRadarSliderValueChanged;
        }

        private void Update()
        {
            _state?.OnUpdate();
        } 

        public void SwitchState(SignalScreenState state)
        {
            _state?.OnExit();
            _state = state;
            _state?.OnEnter();
        }

        public void ResetSliderHandleMoved()
        {
            _sliderHandleMoved = false;
        }
        
        public bool CheckSliderMove() => _sliderHandleMoved;
        
        public void SwitchTargetLost()
        {
            SwitchState(new SignalScreenMessageState(
                this, WhenTargetLost, true,
                () => SwitchState(new SignalScreenIdleState(this))));
        }

        private void OnRadarSliderValueChanged()
        {
            _sliderHandleMoved = true;
        }
    }
}