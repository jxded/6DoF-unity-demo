using UnityEngine;
using UnityEngine.InputSystem;

 // simple drag target script to allow moving the IK target in the scene view, 
 // using new input system and a plane for stable dragging behavior. 
 // trying to emulate three.js drag controls (crudely lol)
public class DragTarget : MonoBehaviour
{
    public Camera cam;

    private bool dragging = false;

    private Plane dragPlane;
    private Vector3 offset;

    void Start()
    {
        if (!cam) cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);

        // Mouse down
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    dragging = true;

                    dragPlane = new Plane(-cam.transform.forward, transform.position);

                    if (dragPlane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        offset = transform.position - hitPoint;
                    }
                }
            }
        }

        // Mouse up
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            dragging = false;
        }

        // Dragging
        if (dragging)
        {
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 point = ray.GetPoint(enter);
                transform.position = point + offset;
            }
        }
    }
}