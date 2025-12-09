using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDetection : MonoBehaviour
{
    public event Action OnMouseEnter;
    public event Action OnMouseExit;

    RectTransform rt;

    bool isHovering = false;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(rt, mousePos) && !isHovering)
        {
            isHovering = true;
            OnMouseEnter?.Invoke();

        }

        if (!RectTransformUtility.RectangleContainsScreenPoint(rt, mousePos) && isHovering)
        {
            isHovering = false;
            OnMouseExit?.Invoke();

        }
    }
}
