using UnityEngine;

public enum JointAxis
{
    X,
    Z
}

[System.Serializable]
public class JointConfig
{
    public Transform transform;

    public JointAxis axis;

    public float minAngle = -180f;
    public float maxAngle = 180f;

    [HideInInspector]
    public float currentAngle;
}