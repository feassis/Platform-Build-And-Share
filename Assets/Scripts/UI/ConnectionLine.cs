using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class ConnectionLine : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        LevelManager.Instance.OnGameModeChange += Instance_OnGameModeChange;
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

        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);
    }

    public void SetStartPos(Transform start) => this.startPoint = start; 
    public void SetEndPos(Transform end) => this.endPoint = end; 

    public void Init(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;
    }
}
