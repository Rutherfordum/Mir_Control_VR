using System;
using Zenject;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace MIR.Services.InputDevice
{
    public class InputUserDataService : IInputUserDataControlService, IInitializable, IDisposable 
    {
        public Vector2 Movement { get; private set; }

        private MiRInputActions _miRInputActions;

        public void Initialize()
        {
            _miRInputActions = new MiRInputActions();
            Debug.Log("InputUserDataService is started");
            Enable();
        }

        public void Enable()
        {
            _miRInputActions.MiR.Movement.started += MovementInputCallback;
            _miRInputActions.MiR.Movement.performed += MovementInputCallback;
            _miRInputActions.MiR.Movement.canceled += MovementInputCallback;
            _miRInputActions.Enable();
            Debug.Log("InputUserDataService is enabled");
        }

        public void Disable()
        {
            _miRInputActions.MiR.Movement.started -= MovementInputCallback;
            _miRInputActions.MiR.Movement.performed -= MovementInputCallback;
            _miRInputActions.MiR.Movement.canceled -= MovementInputCallback;
            _miRInputActions.Disable();
            Debug.Log("InputUserDataService is disabled");
        }

        private void MovementInputCallback(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            _miRInputActions.Disable();
            _miRInputActions.Dispose();
            Debug.Log("InputUserDataService is disposed");
        }
    }
}