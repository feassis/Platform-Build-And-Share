using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct TileEntry
{
    public TileType TileType;
    public TileBase Tile;
    public Sprite CursorIcon;
}

public enum InteractableType
{
    PlayerSpawn = 0,
}