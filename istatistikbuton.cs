using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class istatistikbuton : MonoBehaviour
{
    public PageSelector selector;

    public void OnClick()
    {
        selector.PanelAc(PanelCesit.format);
        gameObject.SetActive(false);
    }
}
