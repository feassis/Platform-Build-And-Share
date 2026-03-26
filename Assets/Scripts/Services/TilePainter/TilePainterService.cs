using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilePainterService : LevelPainterService
{
    [SerializeField] private Image Cursor;

    [SerializeField] private HoverDetection HoverDetection;

    [SerializeField] private Animator uiAnimator;

    [SerializeField] private Button saveButton;

    [SerializeField] private SavePopupUI savePopupUI;

    [SerializeField] private List<TabUI> panelUIs;

    private const string MOVE_OUT_ANIM = "MoveOut";
    private const string MOVE_IN_ANIM = "MoveIn";

    
    private Interactables interactable;
    private TileBase currentTile;
    private Tilemap currentTileMap;
    private bool islocked;
    private bool isSaveLocked;
    private bool isPlaying;

    private void Awake()
    {
        TileSelectButton.OnTileSelect += TileSelectButton_OnTileSelect;
        InteractableSelectButton.OnInteractabelSelect += InteractableSelectButton_OnInteractabelSelect;

        savePopupUI.OnSaveConfirm += SavePopupUI_OnSaveConfirm;
        savePopupUI.OnSavePopupClose += SavePopupUI_OnSavePopupClose;

        HoverDetection.OnMouseEnter += () => islocked = true;
        HoverDetection.OnMouseExit += () => islocked = false;
        currentTileMap = Ground;

        panelUIs[0].Activate(true);

        foreach (var panel in panelUIs)
        {
            panel.OnTabButtonClicked += Panel_OnTabButtonClicked;
        }

        saveButton.onClick.AddListener(OnSaveButtonClicked);
    }

    private void SavePopupUI_OnSavePopupClose()
    {
        isSaveLocked = false;
    }

    private void OnSaveButtonClicked()
    {
        savePopupUI.gameObject.SetActive(true);
        isSaveLocked = true;
    }

    private void SavePopupUI_OnSaveConfirm(string saveName)
    {
        levelSaverService.SaveLevel(saveName, interactableObjects, BackGround, BeforeGround, Ground, InteractablesGround, DecorationGround, ForeGround);
    }

    public void UnselectEverything()
    {
        interactable = null;
        currentTile = null;
        Cursor.gameObject.SetActive(false);
    }

    public override void Init(LevelManager levelManager, LevelSaverService levelSaverService, LevelLoaderService levelLoaderService)
    {
        base.Init(levelManager, levelSaverService, levelLoaderService);

        this.levelManager.OnPlaymodeStart += LevelCreatorManager_OnPlaymodeStart;
        this.levelManager.OnPlaymodeEnd += LevelCreatorManager_OnPlaymodeEnd;

        savePopupUI.Init(levelLoaderService);
    }

    

    protected void LevelCreatorManager_OnPlaymodeEnd()
    {
        isPlaying = false;
        uiAnimator.Play(MOVE_IN_ANIM);
    }

    protected void LevelCreatorManager_OnPlaymodeStart()
    {
        isPlaying = true;
        Cursor.gameObject.SetActive(false);
        uiAnimator.Play(MOVE_OUT_ANIM);
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

    protected void SelectTileMap(TileLayer tileLayer)
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
        if(LevelManager.Instance.GetGameMode() != GameMode.MapEditorMode)
        {
            return;
        }

        if (!Camera.main) return;

        if (islocked || isPlaying || isSaveLocked) return;
             

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
        if (!interactableObjects.ContainsKey(cellPos))
            return;

        var interactable = interactableObjects[cellPos];

        Destroy(interactable.gameObject);
        interactableObjects[cellPos] = null;
        interactableObjects.Remove(cellPos);
    }

    private void SelectTile(TileBase tile)
    {
        currentTile = tile;
        Cursor.gameObject.SetActive(true);
    }
}
