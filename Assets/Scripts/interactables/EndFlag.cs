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

        if (LevelManager.Instance.LevelPainterService is TilePainterService)
        {
            (LevelManager.Instance.LevelPainterService as TilePainterService).UnselectEverything();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out player))
        {
            LevelManager.Instance.EndGameLoop(EndGameType.Victory);
        }
    }
}