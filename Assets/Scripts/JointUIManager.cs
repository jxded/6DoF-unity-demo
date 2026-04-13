using UnityEngine;
using TMPro;
using System.Collections.Generic;
// Displays joint angles as floating text above each joint, with colour coding for limits
public class JointUIManager : MonoBehaviour
{
    public RobotDefinition robot;
    public Camera cam;

    [System.Serializable]
    public class JointUIEntry
    {
        public Transform uiTransform;
        public TextMeshProUGUI label;
    }

    public List<JointUIEntry> uiEntries;

    void LateUpdate()
    {
        for (int i = 0; i < robot.joints.Count; i++)
        {
            var joint = robot.joints[i];
            var ui = uiEntries[i];

            // Position UI slightly above joint
            ui.uiTransform.position = joint.transform.position + Vector3.up * 0.1f;

            // Billboard mode (brought into single script)
            ui.uiTransform.forward = cam.transform.forward;

            float angle = joint.currentAngle;

            ui.label.text = $"{joint.transform.name}\n{angle:F1}°";

            if (angle < joint.minAngle || angle > joint.maxAngle)
                ui.label.color = Color.red;
            else if (Mathf.Abs(angle - joint.minAngle) < 5f || Mathf.Abs(angle - joint.maxAngle) < 5f)
                ui.label.color = Color.yellow;
            else
                ui.label.color = Color.white;
        }
    }
}