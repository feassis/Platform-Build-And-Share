using System;
using UnityEngine;

public class LevelCreatorManager : MonoBehaviour
{
    [field: SerializeField] public TilePainterService TilePainterService { get; private set; }

    [SerializeField] private PlayButtonMode playButtonMode;

    public static LevelCreatorManager Instance { get; private set; }

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

        InitilizaServices();
    
    }

    private void InitilizaServices()
    {
        playButtonMode.Init(this);
        TilePainterService.Init(this);
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
