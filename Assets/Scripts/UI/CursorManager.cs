using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D connectionCursor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelManager.Instance.OnGameModeChange += OnGameModeChange;
    }

    private void OnGameModeChange(GameMode gameMode)
    {
        Debug.Log($"Gamemode: {gameMode}");
        switch (gameMode)
        {
            case GameMode.MapEditorMode:
                Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
                break;
            case GameMode.PlayMode:
                break;
            case GameMode.StartUpMode:
                break;
            case GameMode.DeathMode:
                break;
            case GameMode.ConnectionMode:
                Cursor.SetCursor(connectionCursor, Vector2.zero, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }
}
