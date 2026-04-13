using UnityEngine;

public class DebugLines : MonoBehaviour
{
    public RobotDefinition robot;
    public Transform target;

    void Update()
    {
        Debug.DrawLine(robot.endEffector.position, target.position, Color.red);
    }
}