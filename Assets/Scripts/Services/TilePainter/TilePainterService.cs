using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilePainterService : MonoBehaviour
{
    [SerializeField] private Tilemap BackGround;
    [SerializeField] private Tilemap BeforeGround;
    [SerializeField] private Tilemap Ground;    
    [SerializeField] private Tilemap InteractablesGround;
    [SerializeField] private Tilemap DecorationGround;
    [SerializeField] private Tilemap ForeGround;
    [SerializeField] private Image Cursor;

    [SerializeField] private TileLibrary tileLibrary;
    [SerializeField] private InteractableLibrary interactableLibrary;

    [SerializeField] private HoverDetection HoverDetection;

    [SerializeField] private Animator uiAnimator;

    [SerializeField] private List<TabUI> panelUIs;


    private const string MOVE_OUT_ANIM = "MoveOut";
    private const string MOVE_IN_ANIM = "MoveIn";

    private Dictionary<Vector3Int, Interactables> interactableObjects = new Dictionary<Vector3Int, Interactables>();

    private Interactables interactable;
    private TileBase currentTile;
    private Tilemap currentTileMap;
    private bool islocked;
    private bool isPlaying;

    private TileLayer currentLayer;

    private LevelCreatorManager levelCreatorManager;

    [Serializable]
    protected struct TileConfig
    {
        public TileType TileType;
        public TileBase Tile;
    }

    private void Awake()
    {
        TileSelectButton.OnTileSelect += TileSelectButton_OnTileSelect;
        InteractableSelectButton.OnInteractabelSelect += InteractableSelectButton_OnInteractabelSelect;
        Interactables.OnInteractableDestruction += Interactables_OnInteractableDestruction;

        HoverDetection.OnMouseEnter += () => islocked = true;
        HoverDetection.OnMouseExit += () => islocked = false;
        currentTileMap = Ground;

        panelUIs[0].Activate(true);

        foreach (var panel in panelUIs)
        {
            panel.OnTabButtonClicked += Panel_OnTabButtonClicked;
        }
    }

    public void UnselectEverything()
    {
        interactable = null;
        currentTile = null;
        Cursor.gameObject.SetActive(false);
    }

    public void Init(LevelCreatorManager levelCreatorManager)
    {
        this.levelCreatorManager = levelCreatorManager;

        this.levelCreatorManager.OnPlaymodeStart += LevelCreatorManager_OnPlaymodeStart;
        this.levelCreatorManager.OnPlaymodeEnd += LevelCreatorManager_OnPlaymodeEnd;
    }

    private void LevelCreatorManager_OnPlaymodeEnd()
    {
        isPlaying = false;
        uiAnimator.Play(MOVE_IN_ANIM);
    }

    private void LevelCreatorManager_OnPlaymodeStart()
    {
        isPlaying = true;
        Cursor.gameObject.SetActive(false);
        uiAnimator.Play(MOVE_OUT_ANIM);
    }

    private void Interactables_OnInteractableDestruction(Interactables interactableOnjectToBeRemoved)
    {
        if (interactableObjects.ContainsKey(interactableOnjectToBeRemoved.Position))
        {
            if (!interactableOnjectToBeRemoved.IsDestroyed())
            {
                Destroy(interactableOnjectToBeRemoved.gameObject);
            }
            interactableObjects.Remove(interactableOnjectToBeRemoved.Position);
        }
    }

    private void InteractableSelectButton_OnInteractabelSelect(InteractableType type)
    {
        var interactableEntry = interactableLibrary.GetInteractableEntry(type);

        interactable = interactableEntry.Interactable;

        currentLayer = TileLayer.Interactables;
        currentTileMap = InteractablesGround;
        Cursor.sprite = interactableEntry.CursorIcon;

        Cursor.gameObject.SetActive(true);
    }

    private void Panel_OnTabButtonClicked(TabUI tab)
    {
        SelectTileMap(tab.GetTileLayer());
        foreach (var panel in panelUIs)
        {
            if(panel == tab)
            {
                panel.Activate(true);
            }
            else
            {
                panel.Activate(false);
            }
        }
    }

    public void SelectTileMap(TileLayer tileLayer)
    {
        currentLayer = tileLayer;

        switch (tileLayer)
        {
            case TileLayer.BackGround:
                currentTileMap = BackGround;
                break;
            case TileLayer.BeforeGround:
                currentTileMap = BeforeGround;
                break;
            case TileLayer.Ground:
                currentTileMap = Ground;
                break;
            case TileLayer.Interactables:
                currentTileMap = InteractablesGround;
                break;
            case TileLayer.Decoration:
                currentTileMap = DecorationGround;
                break;
            case TileLayer.ForeGround:
                currentTileMap = ForeGround;
                break;
        }
    }

    private void TileSelectButton_OnTileSelect(TileType tyleType)
    {
        var desiredTile = tileLibrary.GetTileEntry(tyleType);

        Cursor.sprite = desiredTile.CursorIcon;

        SelectTile(desiredTile.Tile);
    }

    void Update()
    {
        if (!Camera.main) return;

        if (islocked || isPlaying) return;

        

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int cellPos = currentTileMap.WorldToCell(worldPos);

        Vector3 cellCenterWorldPos = currentTileMap.GetCellCenterWorld(cellPos);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(cellCenterWorldPos);

        Cursor.transform.position = screenPos;

        if (currentLayer == TileLayer.Interactables)
        {
            if (Input.GetMouseButtonDown(0) && interactable != null)
            {
                if (interactableObjects.ContainsKey(cellPos))
                {
                    DestroyInteractable(cellPos);
                }

                Interactables interactableObject = Instantiate(interactable);
                interactableObject.transform.position = cellCenterWorldPos;
                interactableObject.Position = cellPos;

                interactableObjects.Add(cellPos, interactableObject);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (interactableObjects.ContainsKey(cellPos))
                {
                    DestroyInteractable(cellPos);
                }
            }            
        }
        else
        {

            if (Input.GetMouseButton(0) && currentTile != null)
            {
                currentTileMap.SetTile(cellPos, currentTile);
            }

            if (Input.GetMouseButton(1))
            {
                currentTileMap.SetTile(cellPos, null);
            }
        }
    }

    private void DestroyInteractable(Vector3Int cellPos)
    {
        Destroy(interactableObjects[cellPos].gameObject);
        interactableObjects.Remove(cellPos);
    }

    private void SelectTile(TileBase tile)
    {
        currentTile = tile;
        Cursor.gameObject.SetActive(true);
    }
}

