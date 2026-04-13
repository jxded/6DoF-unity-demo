using UnityEngine;

public enum ControlMode
{
    FK,
    IK
}

public class AppState : MonoBehaviour
{
    public ControlMode mode = ControlMode.FK;

    public void ToggleMode()
    {
        mode = mode == ControlMode.FK ? ControlMode.IK : ControlMode.FK;
        Debug.Log("New mode: " + mode);
    }
}

