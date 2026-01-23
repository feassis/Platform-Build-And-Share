using UnityEngine;

public class Spike : Interactables
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.Death();
        }


    }

}