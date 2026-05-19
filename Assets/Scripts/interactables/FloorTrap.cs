using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : Interactables
{
    [SerializeField] private Animator trapAnimator;

    [SerializeField] private float activeStateDuration = 2f;
    [SerializeField] private float cooldownDuration = 2f;
    [SerializeField] private AudioClip dangerSound;

    private const string IdleAnim = "idle";
    private const string ActivationAnim = "activating";
    private const string ActiveAnim = "ActiveState";
    private const string deactivateAnim = "Deactivation";


    private List<Player> playersOnRange = new List<Player>();

    private Coroutine activationCoroutine;

    private enum TrapMode
    {
        Idle = 0,
        Activating = 1,
        Active = 2,
        Cooldown = 3
    }

    private TrapMode currentMode;

    private void Update()
    {
        if(playersOnRange.Count == 0)
        {
            return;
        }

        if(currentMode == TrapMode.Idle)
        {

            activationCoroutine = StartCoroutine(ActivationRoutine());
        }

        if(currentMode == TrapMode.Active)
        {
            ProcessCollition();
        }
    }

    private IEnumerator ActivationRoutine()
    {
        currentMode = TrapMode.Activating;

        SoundManager.Instance.PlaySFX(dangerSound, transform.position);
        trapAnimator.Play(ActivationAnim);

        yield return CoroutineManager.Instance.WaitForAnimation(trapAnimator, ActivationAnim, () => { });

        currentMode = TrapMode.Active;
        trapAnimator.Play(ActiveAnim);

        yield return new WaitForSeconds(activeStateDuration);

        currentMode = TrapMode.Cooldown;

        trapAnimator.Play(deactivateAnim);

        yield return new WaitForSeconds(cooldownDuration);

        currentMode = TrapMode.Idle;

        trapAnimator.Play(IdleAnim);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            playersOnRange.Add(player);
            ProcessCollition();
        }

        
    }


    private void ProcessCollition()
    {
        if(currentMode == TrapMode.Active)
        {
            DamagePlayersOnRange();
        }
    }


    private void DamagePlayersOnRange()
    {
        foreach(Player player in playersOnRange)
        {
            player.Death();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            playersOnRange.Remove(player);
        }
    }
}
