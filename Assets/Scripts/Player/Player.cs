using UnityEngine;

public class Player : MonoBehaviour
{
    protected PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
}
