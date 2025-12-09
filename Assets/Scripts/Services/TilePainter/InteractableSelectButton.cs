using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableSelectButton : MonoBehaviour
{
    [SerializeField] private InteractableType interactableType;

    [SerializeField] private Button interactButton;

    public static event Action<InteractableType> OnInteractabelSelect;

    private void Awake()
    {
        interactButton.onClick.AddListener(OnInteractabelSelectedClicked);
    }

    private void OnInteractabelSelectedClicked()
    {
        OnInteractabelSelect?.Invoke(interactableType);
    }
}