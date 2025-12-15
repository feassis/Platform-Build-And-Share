using UnityEngine;

public class EndFlag : Interactables
{
    public static EndFlag Instance;

    private Player player;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;

        LevelCreatorManager.Instance.TilePainterService.UnselectEverything();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out player))
        {
            LevelCreatorManager.Instance.EndGameLoop(EndGameType.Victory);
        }
    }
}