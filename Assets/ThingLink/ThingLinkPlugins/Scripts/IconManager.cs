using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    [HideInInspector]
    public Sprite normalIcon;

    [HideInInspector]
    public Sprite hoverIcon;

    [HideInInspector]
    public GameObject pluginWindow;

    private Image spriteRenderer;

    private bool isDragging;

    private Vector3 leftBound;
    void Start()
    {
        spriteRenderer = GetComponent<Image>();
    }

    void LoadResources()
    {
        normalIcon = Resources.Load<Sprite>("ThinglinkLogo");
        hoverIcon = Resources.Load<Sprite>("ThinglinkLogo-hover");
    }

    public void ShowHoverIcon()
    {
        spriteRenderer.sprite = hoverIcon;
    }

    public void ShowNormalIcon()
    {
        spriteRenderer.sprite = normalIcon;
    }

    public void TogglePlugin()
    {

        leftBound = spriteRenderer.transform.TransformPoint(spriteRenderer.sprite.bounds.min);

        if (!isDragging && Input.mousePosition.x > leftBound.x)
        {
            pluginWindow.SetActive(!pluginWindow.activeSelf);
        }
           
    }

    public void StartDragging()
    {
        isDragging = true;
    }

    public void StopDragging()
    {
        isDragging = false;
    }

}
