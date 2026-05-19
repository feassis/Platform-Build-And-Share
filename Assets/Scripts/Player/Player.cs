using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
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
        SoundManager.Instance.PlaySFX(deathSound, transform.position);
        spawnFlag.Respawn(this);
    }
}
