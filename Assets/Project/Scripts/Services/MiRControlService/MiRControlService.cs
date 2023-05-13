using System;
using MIR.Services.InputDevice;
using MIR.Services.PublisherMiRServices;
using UnityEngine;
using Zenject;

namespace MIR.Services.MiRControlService
{
    public class MiRControlService: IMiRControlService, IFixedTickable, IDisposable
    {
        private IInputUserDataControlService _inputUserDataControl;
        private IMovePublisherService _movePublisher;

        private bool _activeSelf;

        [Inject]
        private void Construct(IInputUserDataControlService inputUserDataControl, IMovePublisherService movePublisher)
        {
            _movePublisher = movePublisher;
            _inputUserDataControl = inputUserDataControl;
            Debug.Log("MiRControlService is started");
            Enable();
        }

        public void Enable()
        {
            _activeSelf = true;
            Debug.Log("MiRControlService is enabled");
        }

        public void Disable()
        {
            _activeSelf = false;
            Debug.Log("MiRControlService is disabled");
        }

        public void FixedTick()
        {
            if (_activeSelf)
                _movePublisher.Move(_inputUserDataControl.Movement);
        }

        public void Dispose()
        {
            _movePublisher.Move(Vector2.zero);
            Disable();
            Debug.Log("MiRControlService is disposed");
        }
    }
}