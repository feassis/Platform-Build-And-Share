using UnityEngine;

public class Interactables : MonoBehaviour
{
    public static System.Action<Interactables> OnInteractableDestruction;

    public Vector3Int Position {  get; set; } 
    [field: SerializeField] public InteractableType Type { get; set; }


    public virtual void OnDestroy()
    {
        OnInteractableDestruction?.Invoke(this);
    }

}
