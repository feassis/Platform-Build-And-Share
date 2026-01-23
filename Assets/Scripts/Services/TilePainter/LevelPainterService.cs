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


    public BoundsInt GetCombinedTilemapsBounds()
    {
        Tilemap[] tilemaps =
        {
        BackGround,
        BeforeGround,
        Ground,
        InteractablesGround,
        DecorationGround,
        ForeGround
    };

        bool hasBounds = false;
        int minX = 0, minY = 0;
        int maxX = 0, maxY = 0;

        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap == null)
                continue;

            BoundsInt bounds = tilemap.cellBounds;

            if (!hasBounds)
            {
                minX = bounds.xMin;
                minY = bounds.yMin;
                maxX = bounds.xMax;
                maxY = bounds.yMax;
                hasBounds = true;
            }
            else
            {
                minX = Mathf.Min(minX, bounds.xMin);
                minY = Mathf.Min(minY, bounds.yMin);
                maxX = Mathf.Max(maxX, bounds.xMax);
                maxY = Mathf.Max(maxY, bounds.yMax);
            }
        }

        return new BoundsInt(
            new Vector3Int(minX, minY, 0),
            new Vector3Int(maxX - minX, maxY - minY, 1)
        );
    }


    public void FitBoxColliderBelowLevel(
    BoxCollider2D boxCollider,
    float extraDepth = 2f // how much lower than the lowest tile
)
    {
        BoundsInt bounds = GetCombinedTilemapsBounds();

        // Convert cell bounds to world
        Vector3 minWorld = Ground.CellToWorld(
            new Vector3Int(bounds.xMin, bounds.yMin, 0)
        );

        Vector3 maxWorld = Ground.CellToWorld(
            new Vector3Int(bounds.xMax, bounds.yMax, 0)
        );

        float levelWidth = maxWorld.x - minWorld.x;

        // Desired collider dimensions
        float colliderWidth = levelWidth * 2f;
        float colliderHeight = extraDepth;

        // Position collider so its TOP is exactly at lowest tile
        Vector2 colliderCenter = new Vector2(
            (minWorld.x + maxWorld.x) * 0.5f,
            minWorld.y - (colliderHeight * 0.5f)
        );

        boxCollider.size = new Vector2(colliderWidth, colliderHeight);
        boxCollider.offset = boxCollider.transform.InverseTransformPoint(colliderCenter);
    }
}