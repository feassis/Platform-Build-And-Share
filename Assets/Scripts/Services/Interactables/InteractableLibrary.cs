using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableLibrary", menuName = "Setup/InteractableLibrary")]
public class InteractableLibrary : ScriptableObject
{
    public List<InteractableEntry> interactableLibrary;
    public Interactables GetInteractable(InteractableType interactableType) => interactableLibrary.Find(t => t.InteractableType == interactableType).Interactable;

    public InteractableEntry GetInteractableEntry(InteractableType interactableType) => interactableLibrary.Find(t => t.InteractableType == interactableType);

    public InteractableType GetInteractableTile(Interactables interactable) => interactableLibrary.Find(t => t.Interactable.Equals(interactable)).InteractableType;
}
