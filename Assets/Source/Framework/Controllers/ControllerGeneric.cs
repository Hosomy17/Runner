using UnityEngine;
using GamepadFramework;

namespace ControllerFramework
{
    public abstract class ControllerGeneric
    {
        public abstract void NewInputs(GamepadInfo gamepadInfo);

        public abstract void TrackObject(GameObject obj);
    }
}