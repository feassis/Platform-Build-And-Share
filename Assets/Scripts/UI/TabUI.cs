using System;
using UnityEngine;
using UnityEngine.UI;

public class TabUI : MonoBehaviour
{
    [SerializeField] private TileLayer tileLayer;
    [SerializeField] private GameObject deactivationLayer;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button button;

    public event Action<TabUI> OnTabButtonClicked;

    public TileLayer GetTileLayer() => tileLayer;

    private void Awake()
    {
        button.onClick.AddListener(() => OnTabButtonClicked?.Invoke(this));
    }

    public void Activate(bool isActive)
    {
        deactivationLayer.SetActive(!isActive);
        panel.SetActive(isActive);
    }
}
