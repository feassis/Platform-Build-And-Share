using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class ConnectionLine : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private LineRenderer line;
    private EdgeCollider2D edge;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        edge = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        LevelManager.Instance.OnGameModeChange += Instance_OnGameModeChange;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnGameModeChange -= Instance_OnGameModeChange;
    }

    private void Instance_OnGameModeChange(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.ConnectionMode:
                line.enabled = true;
                break;
            default:
                line.enabled = false;
                break;
        }
    }

    private void Update()
    {
        if (startPoint == null || endPoint == null)
            return;

        Vector3 startWorld = startPoint.position;
        Vector3 endWorld = endPoint.position;

        line.SetPosition(0, startWorld);
        line.SetPosition(1, endWorld);

        Vector2 startLocal = transform.InverseTransformPoint(startWorld);
        Vector2 endLocal = transform.InverseTransformPoint(endWorld);

        edge.points = new Vector2[] { startLocal, endLocal };

    }

    public void SetStartPos(Transform start) => this.startPoint = start; 
    public void SetEndPos(Transform end) => this.endPoint = end; 

    public void Init(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;
    }
}
