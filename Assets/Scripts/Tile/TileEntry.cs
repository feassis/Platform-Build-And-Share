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
    EndGameSpawn = 1,
    FloorTrap = 2,
    FloorSpike = 3,
    WallArrowTrapRight = 4,
    PassThroughPlatform = 5,
}