using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// Slider based control from the UI for FK mode, 
// else updates the slider values when switching to IK mode.
public class FKUIController : MonoBehaviour
{
    public RobotResolve resolve;
    public AppState appState;

    public List<Slider> sliders;

    void Start()
    {
        // automatically setting the range now 
        // based on joint defs
        for (int i = 0; i < sliders.Count; i++)
        {
            var joint = resolve.robot.joints[i];

            sliders[i].minValue = joint.minAngle;
            sliders[i].maxValue = joint.maxAngle;
        }
    }
    void Update()
    {
        bool fkMode = appState.mode == ControlMode.FK;

        for (int i = 0; i < sliders.Count; i++)
        {
            sliders[i].interactable = fkMode;

            if (fkMode)
            {
                resolve.SetJointAngle(i, sliders[i].value);
            }
            else
            {
                sliders[i].value = resolve.robot.joints[i].currentAngle;
            }
        }
    }
}
