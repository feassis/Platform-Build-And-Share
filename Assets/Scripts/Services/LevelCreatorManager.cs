using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreatorManager : MonoBehaviour
{
    private const string LEVEL_EDITOR_NAME = "MapCreator";

    [field: SerializeField] public TilePainterService TilePainterService { get; private set; }

    [SerializeField] private PlayButtonMode playButtonMode;
    [SerializeField] private string folderPath;
    [SerializeField] private TileLibrary tileLibrary;
    [SerializeField] private InteractableLibrary interactableLibrary;

    public static LevelCreatorManager Instance { get; private set; }
    public LevelSaverService LevelSaverService {  get; private set; }
    public LevelLoaderService LevelLoaderService { get; private set; }

    private static bool isNewMap;
    private static string levelName;

    public static void Open(bool newMap, string fileName = null)
    {
        SceneManager.LoadScene(LEVEL_EDITOR_NAME);

        isNewMap = newMap;
        levelName = fileName;
    }

    protected GameMode gameMode;

    public event Action OnPlaymodeStart;
    public event Action OnPlaymodeEnd;


    protected enum GameMode
    {
        MapEditor = 0,
        PlayMode = 1,
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);

            return;
        }

        Instance = this;

        CreateService();
        InitilizaServices();
    
    }

    private void CreateService()
    {
        LevelSaverService = new LevelSaverService(folderPath, tileLibrary, interactableLibrary);

        LevelLoaderService = new LevelLoaderService(folderPath, tileLibrary, interactableLibrary);
    }

    private void InitilizaServices()
    {
        playButtonMode.Init(this);
        TilePainterService.Init(this, LevelSaverService, LevelLoaderService);

        if (!isNewMap)
        {
            TilePainterService.LoadMap(levelName);
        }
    }

    public void ToggleGameMode(bool goToPlay)
    {
        if(goToPlay)
        {
            gameMode = GameMode.PlayMode;
            OnPlaymodeStart?.Invoke();
        }
        else
        {
            gameMode = GameMode.MapEditor;
            OnPlaymodeEnd?.Invoke();
        }
    }

    public void EndGameLoop(EndGameType endGame)
    {
        if(endGame == EndGameType.Victory)
        {
            ToggleGameMode(false);
        }
    }
}
