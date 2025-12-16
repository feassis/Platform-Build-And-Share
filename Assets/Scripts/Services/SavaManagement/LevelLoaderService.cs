using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class LevelLoaderService
{
    protected string folderPath;
    protected TileLibrary tileLibrary;
    protected InteractableLibrary interactableLibrary;

    public LevelLoaderService(
        string folderPath,
        TileLibrary tileLibrary,
        InteractableLibrary interactableLibrary)
    {
        this.folderPath = folderPath;
        this.tileLibrary = tileLibrary;
        this.interactableLibrary = interactableLibrary;
    }

    public void LoadLevel(
        string saveName,
        Dictionary<Vector3Int, Interactables> interactables,
        Tilemap background,
        Tilemap beforeground,
        Tilemap ground,
        Tilemap interactablesground,
        Tilemap decorationground,
        Tilemap foreground,
        Transform interactablesParent = null)
    {
        string path = Application.dataPath + $"/{folderPath}/{saveName}";

        if (!File.Exists(path))
        {
            Debug.LogError($"Arquivo de level não encontradoLevel file not found: {path}");
            return;
        }

        string json = File.ReadAllText(path);
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        // limpa tilemaps
        ClearTilemap(background);
        ClearTilemap(beforeground);
        ClearTilemap(ground);
        ClearTilemap(interactablesground);
        ClearTilemap(decorationground);
        ClearTilemap(foreground);

        // limpa interactables existentes
        foreach (var obj in interactables.Values)
        {
            if (obj != null)
                Object.Destroy(obj.gameObject);
        }
        interactables.Clear();

        // recria tilemaps
        ApplyTiles(levelData.backgroundtiles, background);
        ApplyTiles(levelData.beforegroundtiles, beforeground);
        ApplyTiles(levelData.groundtiles, ground);
        ApplyTiles(levelData.interactablesgroundtiles, interactablesground);
        ApplyTiles(levelData.decorationgroundtiles, decorationground);
        ApplyTiles(levelData.foregroundtiles, foreground);

        // recria interactables
        foreach (InteractablesRocord record in levelData.interactables)
        {
            Interactables prefab =
                interactableLibrary.GetInteractable(record.interactableType);

            if (prefab == null)
            {
                Debug.LogWarning($"Interactable not found: {record.interactableType}");
                continue;
            }

            Vector3Int cellPos = new Vector3Int(record.x, record.y, record.z);
            Vector3 worldPos = ground.GetCellCenterWorld(cellPos);

            Interactables instance = Object.Instantiate(
                prefab,
                worldPos,
                Quaternion.identity,
                interactablesParent
            );
            interactables[cellPos] = instance;
        }

        Debug.Log($"Mapa carregado! From: {path}");
    }

    // ----------------------------
    // Helpers
    // ----------------------------

    void ApplyTiles(List<TileRecord> records, Tilemap tilemap)
    {
        foreach (TileRecord record in records)
        {
            TileBase tile = tileLibrary.GetTile(record.tileType);
            if (tile == null)
            {
                Debug.LogWarning($"Tile não encontrado: {record.tileType}");
                continue;
            }

            tilemap.SetTile(
                new Vector3Int(record.x, record.y, 0),
                tile
            );
        }
    }

    void ClearTilemap(Tilemap tilemap)
    {
        if (tilemap != null)
            tilemap.ClearAllTiles();
    }
}
