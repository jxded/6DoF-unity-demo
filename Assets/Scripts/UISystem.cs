using UnityEngine;
using TMPro;

// positions and error immediateGUI
// lowkey not performant but good enough eh
public class UISystem : MonoBehaviour
{
    public RobotDefinition robot;
    public Transform target;

    public TextMeshProUGUI endText;
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI errorText;

    void Update()
    {
        Vector3 e = robot.endEffector.position;
        Vector3 t = target.position;

        float error = Vector3.Distance(e, t);

        endText.text = $"End: {e:F2}";
        targetText.text = $"Target: {t:F2}";
        errorText.text = $"Error: {error:F3}";
    }
}