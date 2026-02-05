using System;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public event Action<Player> OnPlayerEnter;
    public event Action<Player> OnPlayerExit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            OnPlayerEnter?.Invoke(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            OnPlayerExit?.Invoke(player);
        }
    }
}