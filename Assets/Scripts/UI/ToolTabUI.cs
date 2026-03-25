using UnityEngine;
using UnityEngine.UI;

public class ToolTabUI : TabUI
{
    [SerializeField] private Button connectionButton;

    protected override void Awake()
    {
        base.Awake();

        connectionButton.onClick.AddListener(OnConnectionButtonClicked);
    }

    private void OnConnectionButtonClicked()
    {
        LevelManager.Instance.ChangeGameMode(GameMode.ConnectionMode);
    }

    public override void Activate(bool isActive)
    {
        base.Activate(isActive);
        LevelManager.Instance.ChangeGameMode(GameMode.MapEditorMode);

    }
}
