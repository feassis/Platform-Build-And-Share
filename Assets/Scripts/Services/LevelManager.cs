using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] protected TileLibrary tileLibrary;
    [SerializeField] protected InteractableLibrary interactableLibrary;

    [field: SerializeField] public LevelPainterService LevelPainterService { get; private set; }
    public static LevelManager Instance { get; private set; }

    public LevelLoaderService LevelLoaderService { get; private set; }
    public LevelSaverService LevelSaverService { get; private set; }

    protected static bool isNewMap = true;
    protected static string levelName;

    public event Action OnPlaymodeStart;
    public event Action OnPlaymodeEnd;

    protected GameMode gameMode;

    protected enum GameMode
    {
        MapEditorMode = 0,
        PlayMode = 1,
        StartUpMode = 2,
        DeathMode = 3
    }


    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);

            return;
        }

        Instance = this;

        CreateService();
        InitilizaServices();

    }


    protected virtual void CreateService()
    {
        LevelLoaderService = new LevelLoaderService(GameConstants.SAVE_FOLDER_PATH, tileLibrary, interactableLibrary);
        LevelSaverService = new LevelSaverService(GameConstants.SAVE_FOLDER_PATH, tileLibrary, interactableLibrary);
    }

    protected virtual void InitilizaServices()
    {

        LevelPainterService.Init(this, LevelSaverService, LevelLoaderService);

        if (!isNewMap)
        {
            LevelPainterService.LoadMap(levelName);
        }
    }


    public void ToggleGameMode(bool goToPlay)
    {
        if (goToPlay)
        {
            gameMode = GameMode.PlayMode;
            OnPlaymodeStart?.Invoke();
        }
        else
        {
            gameMode = GameMode.MapEditorMode;
            OnPlaymodeEnd?.Invoke();
        }
    }

    public void EndGameLoop(EndGameType endGame)
    {
        if (endGame == EndGameType.Victory)
        {
            ToggleGameMode(false);
        }
    }
}
