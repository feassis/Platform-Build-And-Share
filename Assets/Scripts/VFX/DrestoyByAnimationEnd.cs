using System.Collections;
using UnityEngine;

public class DrestoyByAnimationEnd : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private string animationName;

    private void Awake()
    {
        StartCoroutine(DestroyAtAnimationEnd(animationName));
    }

    private IEnumerator DestroyAtAnimationEnd(string stateName)
    {
        animator.Play(stateName);

        // Espera 1 frame para o Animator atualizar
        yield return null;

        // Espera a animação terminar
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
