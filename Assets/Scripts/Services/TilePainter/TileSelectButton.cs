using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileSelectButton : MonoBehaviour
{
    [SerializeField] private TileType tileType;
    [SerializeField] private Button tileButton;

    public static event Action<TileType> OnTileSelect;

    private void Awake()
    {
        tileButton.onClick.AddListener(OnTileButtonClicked);
    }

    private void OnTileButtonClicked()
    {
        OnTileSelect?.Invoke(tileType);
    }
}
