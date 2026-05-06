
using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class Switch : Interactables, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnActivation;
    private bool isHovering = false;
    [SerializeField] private SpriteRenderer leftLever;
    [SerializeField] private SpriteRenderer rightLever;

    private bool isLeft = true;


    public void OnPointerEnter(PointerEventData eventData)
    {
       isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void Update()
    {
        if(LevelManager.Instance.GetGameMode() != GameMode.MapEditorMode)
        {
            return;
        }


    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        
        if ((collision.TryGetComponent<Player>(out Player player)))
        {
            OnActivation?.Invoke();

            isLeft = !isLeft;

            leftLever.gameObject.SetActive(isLeft);
            rightLever.gameObject.SetActive(!isLeft);
        }
    }



}
