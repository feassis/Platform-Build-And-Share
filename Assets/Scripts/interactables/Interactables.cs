using UnityEngine;

public class Interactables : MonoBehaviour
{
    public static System.Action<Interactables> OnInteractableDestruction;

    public Vector3Int Position {  get; set; } 
    [field: SerializeField] public InteractableType Type { get; set; }
    [SerializeField] private SpriteRenderer selectedOutline;


    public virtual void OnDestroy()
    {
        OnInteractableDestruction?.Invoke(this);
    }

    public virtual void Selected()
    {
        selectedOutline.gameObject.SetActive(true);
    }

    public virtual void Deselected()
    {
        selectedOutline.gameObject.SetActive(false);
    }
}
