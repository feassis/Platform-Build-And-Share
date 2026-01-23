using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] protected TileLibrary tileLibrary;
    [SerializeField] protected InteractableLibrary interactableLibrary;
    [SerializeField] protected float deathboxDownOffset = 20;
    [SerializeField] protected Deathbox deathboxPrefab;

    [field: SerializeField] public LevelPainterService LevelPainterService { get; private set; }
    public static LevelManager Instance { get; private set; }

    public LevelLoaderService LevelLoaderService { get; private set; }
    public LevelSaverService LevelSaverService { get; private set; }

    protected static bool isNewMap = true;
    protected static string levelName;

    public event Action OnPlaymodeStart;
    public event Action OnPlaymodeEnd;

    protected Deathbox deathbox;

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
            StartGameCleanUp();
        }
        else
        {
            gameMode = GameMode.MapEditorMode;
            OnPlaymodeEnd?.Invoke();
            EndGameCleanUp();
        }
    }

    public void EndGameLoop(EndGameType endGame)
    {
        if (endGame == EndGameType.Victory)
        {
            ToggleGameMode(false);
        }
    }

    protected virtual void StartGameCleanUp()
    {
        deathbox = Instantiate<Deathbox>(deathboxPrefab);
        deathbox.OnPlayerDeathboxEnter += Deathbox_OnPlayerDeathboxEnter;

        LevelPainterService.FitBoxColliderBelowLevel(deathbox.GetComponent<BoxCollider2D>(), deathboxDownOffset);
    }

    private void Deathbox_OnPlayerDeathboxEnter(Player player)
    {
        player.Death();
    }

    protected virtual void EndGameCleanUp()
    {
        deathbox.OnPlayerDeathboxEnter -= Deathbox_OnPlayerDeathboxEnter;
        Destroy(deathbox.gameObject);
    }


    

}
