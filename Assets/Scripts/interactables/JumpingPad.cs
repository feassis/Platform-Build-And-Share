using System.Collections.Generic;
using UnityEngine;

public class JumpingPad : Interactables
{
    [SerializeField] private PlayerDetection jumpPadDetection;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float force;
    [SerializeField] private float duration = 0.3f;

    [SerializeField] private AudioClip bounceSound;

    private void Awake()
    {
        jumpPadDetection.OnPlayerEnter += JumpPadDetection_OnPlayerEnter;
    }

    private void JumpPadDetection_OnPlayerEnter(Player player)
    {
        Debug.Log("Try to Bounce on Jump pad");

        PlayerMovement pMov = player.gameObject.GetComponent<PlayerMovement>();

        pMov.Bounce(direction.normalized * force, duration);
        SoundManager.Instance.PlaySFX(bounceSound, transform.position);
    }
}
