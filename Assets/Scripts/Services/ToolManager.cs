using Inputs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToolManager : MonoBehaviour
{
    [SerializeField] private ConnectionLine connectionLinePrefab;
    private ToolType currentTool = ToolType.None;

    private Camera cam;

    private Door currentDoor;
    private Switch currentSwitch;

    private PlayerInputs playerInput;

    HashSet<DoorConnection> connections = new HashSet<DoorConnection>();

    private void Awake()
    {
        playerInput = new PlayerInputs();
        playerInput.Tools.Click.performed += OnClick;
        playerInput.Tools.Click.canceled -= OnClick;
    }

    private void Start()
    {
        cam = Camera.main;
        LevelManager.Instance.OnGameModeChange += Instance_OnGameModeChange;
    }

    private void Instance_OnGameModeChange(GameMode mode)
    {
        if(mode == GameMode.ConnectionMode)
        {
            currentTool = ToolType.Connector;
        }
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            return;
        }

        playerInput.Enable();
    }



    private void OnDisable()
    {
        if (playerInput == null)
        {
            return;
        }

        playerInput.Disable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        HandleClick();
    }

    private void HandleClick()
    {
        if (currentTool != ToolType.Connector)
            return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 worldPos = cam.ScreenToWorldPoint(mouseScreenPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider == null)
            return;

        Interactables interactable = hit.collider.GetComponentInParent<Interactables>();

        if(interactable is Door)
        {
            currentDoor = (Door)interactable;
            Debug.Log($"Door selected: {interactable.name}");
        }
        else if (interactable is Switch)
        {
            currentSwitch = (Switch)interactable;
        }


        if(currentSwitch != null && currentDoor != null)
        {

            var newConnection = new DoorConnection(currentDoor, currentSwitch);

            if (connections.Add(newConnection))
            {
                currentDoor.Connect(currentSwitch);

                var connectionLine = Instantiate<ConnectionLine>(connectionLinePrefab);
                connectionLine.SetStartPos(currentDoor.transform);
                connectionLine.SetEndPos(currentSwitch.transform);

                newConnection.Connection = connectionLine;
            }

            currentDoor = null;
            currentSwitch = null;
        }
    }

    public void SetTool(ToolType tool)
    {
        currentTool = tool;
    }
}


public class DoorConnection
{
    public Door Door;
    public Switch Switch;
    public ConnectionLine Connection;

    public DoorConnection(Door door, Switch sw)
    {
        Door = door;
        Switch = sw;
    }

    public override bool Equals(object obj)
    {
        if (obj is DoorConnection other)
        {
            return Door == other.Door && Switch == other.Switch;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (Door, Switch).GetHashCode();
    }
}