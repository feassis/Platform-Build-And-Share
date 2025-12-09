using UnityEngine;

public class TileInfo : MonoBehaviour
{
    [SerializeField] private TileType tileType;

    public TileType GetTileType() => tileType;
}
