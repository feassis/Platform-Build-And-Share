using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class LevelSaverService
{
    protected string folderPath;
    protected TileLibrary tileLibrary;
    protected InteractableLibrary interactableLibrary;

    public LevelSaverService(string folderPath, TileLibrary tileLibrary, 
        InteractableLibrary interactableLibrary)
    {
        this.folderPath = folderPath;
        this.tileLibrary = tileLibrary;
        this.interactableLibrary = interactableLibrary;
    }

    public void SaveLevel(string saveName, Dictionary<Vector3Int, Interactables> interactables, Tilemap background,
        Tilemap beforeground, Tilemap ground, Tilemap interactablesground, Tilemap decorationground, Tilemap foreground)
    {
        LevelData levelData = new LevelData();

        levelData.backgroundtiles = ExtractTilesFromTileMap(background);
        levelData.beforegroundtiles = ExtractTilesFromTileMap(beforeground);
        levelData.groundtiles = ExtractTilesFromTileMap(ground);
        levelData.interactablesgroundtiles = ExtractTilesFromTileMap(interactablesground);
        levelData.decorationgroundtiles = ExtractTilesFromTileMap(decorationground);
        levelData.foregroundtiles = ExtractTilesFromTileMap(foreground);

        List<InteractablesRocord> interactableRecords = new List<InteractablesRocord>();

        foreach (KeyValuePair<Vector3Int, Interactables> entry in interactables)
        {
            Vector3Int pos = entry.Key;
            interactableRecords.Add(new InteractablesRocord
            {
                interactableType = entry.Value.Type,
                x = pos.x,
                y = pos.y,
                z = pos.z
            });


        }

        levelData.interactables = interactableRecords;

        string json = JsonUtility.ToJson(levelData, true);
        string path = Application.dataPath + $"/{folderPath}";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        File.WriteAllText(path + $"/{saveName}", json);
        Debug.Log($"Mapa salvo! At: {path + $"/{saveName}"}  | data: {json}");


    }

    public List<TileRecord> ExtractTilesFromTileMap(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds; 
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds); 

        List<TileRecord> tiles = new List<TileRecord>();

        for (int x = 0; x < bounds.size.x; x++) 
        { 
            for (int y = 0; y < bounds.size.y; y++) 
            { 
                TileBase tile = allTiles[x + y * bounds.size.x]; 
                if (tile != null) 
                { 
                    tiles.Add(new TileRecord { tileType = tileLibrary.GetTileType(tile), x = x + bounds.x, y = y + bounds.y }); 
                } 
            } 
        }

        return tiles;
    }
}

[System.Serializable] 
public class TileRecord 
{ 
    public TileType tileType; 
    public int x, y; 
}

[System.Serializable]
public class InteractablesRocord
{
    public InteractableType interactableType;
    public int x, y, z;
}

[System.Serializable] 
public class LevelData 
{ 
    public List<TileRecord> backgroundtiles = new List<TileRecord>(); 
    public List<TileRecord> beforegroundtiles = new List<TileRecord>(); 
    public List<TileRecord> groundtiles = new List<TileRecord>(); 
    public List<TileRecord> interactablesgroundtiles = new List<TileRecord>(); 
    public List<TileRecord> decorationgroundtiles = new List<TileRecord>(); 
    public List<TileRecord> foregroundtiles = new List<TileRecord>(); 

    public List<InteractablesRocord> interactables = new List<InteractablesRocord>();
}
