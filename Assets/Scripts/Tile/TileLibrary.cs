using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileLibrary", menuName = "Setup/TileLibrary")]
public class TileLibrary : ScriptableObject
{
    public List<TileEntry> tileLibrary;
    public TileBase GetTile(TileType tileType) => tileLibrary.Find(t => t.TileType == tileType).Tile;

    public TileEntry GetTileEntry(TileType tileType) => tileLibrary.Find(t => t.TileType == tileType);

    public TileType GetTileType(TileBase tile) => tileLibrary.Find(t => t.Tile.Equals(tile)).TileType;
}
