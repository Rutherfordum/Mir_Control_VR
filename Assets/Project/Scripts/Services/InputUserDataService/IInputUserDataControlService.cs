using UnityEngine;

namespace MIR.Services.InputDevice
{
    public interface IInputUserDataControlService: IInputUserDataService
    {
        public Vector2 Movement { get; }
    }
}