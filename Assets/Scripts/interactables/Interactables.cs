using UnityEngine;

public class Interactables : MonoBehaviour
{
    public static System.Action<Interactables> OnInteractableDestruction;

    public Vector3Int Position {  get; set; } 

    public virtual void OnDestroy()
    {
        OnInteractableDestruction?.Invoke(this);
    }

}

