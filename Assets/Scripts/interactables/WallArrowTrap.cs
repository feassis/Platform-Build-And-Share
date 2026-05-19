
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallArrowTrap : Interactables
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private float cooldown;
    [SerializeField] private TrapProjectile projectilePrefab;
    [SerializeField] private AudioClip dangerSound;


    private bool isActive = true;

    private List<Player> players = new List<Player>();

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        if(players.Count > 0)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            players.Add(player);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            players.Remove(player);
        }
    }

    private void Shoot()
    {
        isActive = false;
        var projectile = Instantiate<TrapProjectile>(projectilePrefab);
        projectile.transform.position = spawnPoint.position;

        projectile.Setup(projectileSpeed, projectileLifeTime, (shootingPoint.position - spawnPoint.position).normalized);

        SoundManager.Instance.PlaySFX(dangerSound, transform.position);

        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);

        isActive = true;
    }
}
