using UnityEngine;

public class RobotResolve : MonoBehaviour
{
    public RobotDefinition robot;
    public AppState appState;

    public Transform target;

    [Header("IK Settings")]
    public int iterations = 10;
    public float threshold = 0.01f;

    void Update()
    {
        if (appState.mode == ControlMode.IK)
        {
            SolveIK();
        }
    }

    // IK solver uses CCD (Cyclic Coordinate Descent) method only for now.
    void SolveIK()
    {
        for (int iter = 0; iter < iterations; iter++)
        {
            for (int i = robot.joints.Count - 1; i >= 0; i--)
            {
                JointConfig joint = robot.joints[i];
                Transform jt = joint.transform;

                Vector3 toEnd = robot.endEffector.position - jt.position;
                Vector3 toTarget = target.position - jt.position;

                Vector3 axis = GetAxisVector(joint);

                Vector3 projToEnd = Vector3.ProjectOnPlane(toEnd, axis);
                Vector3 projToTarget = Vector3.ProjectOnPlane(toTarget, axis);

                if (projToEnd.magnitude < 0.0001f || projToTarget.magnitude < 0.0001f)
                    continue;

                float angle = Vector3.SignedAngle(projToEnd, projToTarget, axis);

                ApplyRotation(joint, angle);
            }

            float error = Vector3.Distance(robot.endEffector.position, target.position);
            if (error < threshold)
                break;
        }
    }

    // simple fk control
    public void SetJointAngle(int index, float angle)
    {
        JointConfig joint = robot.joints[index];
        // added a small buffer to prevent slider from hitting hard limits which can cause IK instability
        joint.currentAngle = Mathf.Clamp(angle, joint.minAngle + 0.1f, joint.maxAngle - 0.1f);
        ApplyLocalRotation(joint);
    }

    // helper functions
    void ApplyRotation(JointConfig joint, float delta)
    {
        joint.currentAngle += delta;
        joint.currentAngle = Mathf.Clamp(joint.currentAngle, joint.minAngle + 0.1f, joint.maxAngle - 0.1f);

        ApplyLocalRotation(joint);
    }

    void ApplyLocalRotation(JointConfig joint)
    {
        if (joint.axis == JointAxis.X)
            joint.transform.localRotation = Quaternion.Euler(joint.currentAngle, 0, 0);
        else
            joint.transform.localRotation = Quaternion.Euler(0, 0, joint.currentAngle);
    }

    Vector3 GetAxisVector(JointConfig joint)
    {
        if (joint.axis == JointAxis.X)
            return joint.transform.right;
        else
            return joint.transform.forward;
    }
}