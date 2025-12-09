using UnityEngine;

public class SpawnFlag : Interactables
{
    public static SpawnFlag Instance;

    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform spawnPos;

    private Player player;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;

        LevelCreatorManager.Instance.TilePainterService.UnselectEverything();
    }

    private void Start()
    {
        LevelCreatorManager.Instance.OnPlaymodeStart += Instance_OnPlaymodeStart;
        LevelCreatorManager.Instance.OnPlaymodeEnd += Instance_OnPlaymodeEnd;
    }

    private void Instance_OnPlaymodeEnd()
    {
        Destroy(player.gameObject);
    }

    private void Instance_OnPlaymodeStart()
    {
        player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
    }
}

