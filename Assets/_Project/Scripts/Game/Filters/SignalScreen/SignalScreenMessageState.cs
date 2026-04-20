using System;
using Plugins.Audio;
using UnityEngine;

namespace Project.Game
{
    public class SignalScreenMessageState : SignalScreenState
    {
        private float _time;
        
        private readonly GameObject _message;
        private readonly bool _playSound;
        private readonly Action _continuation;
        
        public SignalScreenMessageState(SignalScreenController screenController,
            GameObject message, bool playSound, Action continuation) : base(screenController)
        {
            _message = message;
            _playSound = playSound;
            _continuation = continuation;
        }

        public override void OnEnter()
        {
            _time = ScreenController.MessageDuration;
            _message.gameObject.SetActive(true);
            if (_playSound)
                AudioSystem.Game_Machines_ScreenMessage.PlayOneShot();
        }

        public override void OnExit()
        {
            _message.gameObject.SetActive(false);
        }

        public override void OnUpdate()
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
                _continuation.Invoke();
        }
    }
}