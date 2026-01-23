using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    protected PlayerMovement playerMovement;
    protected SpawnFlag spawnFlag;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetSpawnFlag(SpawnFlag flag)
    {
        spawnFlag = flag;
    }

    public void Death()
    {
        spawnFlag.Respawn(this);
    }
}
