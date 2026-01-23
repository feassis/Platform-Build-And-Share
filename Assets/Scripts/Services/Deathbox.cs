using System;
using UnityEngine;

public class Deathbox : MonoBehaviour
{
    public event Action<Player> OnPlayerDeathboxEnter;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            OnPlayerDeathboxEnter?.Invoke(player);

            Debug.Log("Player Death");
        }
    }
}
