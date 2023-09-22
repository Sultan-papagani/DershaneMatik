using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavbarButton : MonoBehaviour, IPointerDownHandler
{
    public PanelCesit panel_cesiti;

    private PageSelector selector;

    private void Start() 
    {
        selector = GetComponentInParent<PageSelector>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selector.PanelAc(panel_cesiti);
    }
}
