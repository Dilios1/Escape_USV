using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 lastMousePosition;
    private float x, y, z;

    public bool blockX, blockY, blockZ;

    public bool toOrginal;

    private Vector3 orginalPosition;

    private void Start()
    {
        orginalPosition = transform.localPosition;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 curremtMousePosition = eventData.position;
        Vector2 diff = curremtMousePosition - lastMousePosition;

        RectTransform rectTransform = GetComponent<RectTransform>();

        x = y = z = 0;

        if (!blockX)
            x = diff.x;

        if (!blockY)
            y = diff.y;

        if (!blockZ)
            z = transform.localPosition.z;

        rectTransform.position = rectTransform.position + new Vector3(x, y, z);

        lastMousePosition = curremtMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (toOrginal)
            transform.localPosition = orginalPosition;
    }
}