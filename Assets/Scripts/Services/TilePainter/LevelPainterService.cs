using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelPainterService : MonoBehaviour
{
    [SerializeField] protected Tilemap BackGround;
    [SerializeField] protected Tilemap BeforeGround;
    [SerializeField] protected Tilemap Ground;
    [SerializeField] protected Tilemap InteractablesGround;
    [SerializeField] protected Tilemap DecorationGround;
    [SerializeField] protected Tilemap ForeGround;

    [SerializeField] protected TileLibrary tileLibrary;
    [SerializeField] protected InteractableLibrary interactableLibrary;

    protected TileLayer currentLayer;

    protected LevelManager levelManager;
    protected LevelSaverService levelSaverService;
    protected LevelLoaderService levelLoaderService;

    protected Dictionary<Vector3Int, Interactables> interactableObjects = new Dictionary<Vector3Int, Interactables>();

    public virtual void Init(LevelManager levelManager, LevelSaverService levelSaverService, LevelLoaderService levelLoaderService)
    {
        this.levelManager = levelManager;

        this.levelSaverService = levelSaverService;
        this.levelLoaderService = levelLoaderService;
    }

    


    public void LoadMap(string mapName)
    {
        levelLoaderService.LoadLevel(mapName, interactableObjects, BackGround, BeforeGround, Ground,
            InteractablesGround, DecorationGround, ForeGround);
    }

}