using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CursorLineRenderer2D : MonoBehaviour
{
    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    void Update()
    {
        line.SetPosition(0, transform.position);

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;

        line.SetPosition(1, mouseWorld);
    }
}
