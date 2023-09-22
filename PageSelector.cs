using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageSelector : MonoBehaviour
{
    [SerializeField] private GameObject YaziPaneli;
    [SerializeField] private GameObject FormatPaneli;
    [SerializeField] private GameObject KarsilastirPaneli;
    [SerializeField] private GameObject KaydetPaneli;

    public RectTransform rc;
    public Camera cam;

    private GameObject Onceki;

    private void Start() {
        Onceki = YaziPaneli;
        YaziPaneli.SetActive(true);
        FormatPaneli.SetActive(false);
        KarsilastirPaneli.SetActive(false);
        KaydetPaneli.SetActive(false);
    }


    private void PaneliAc(GameObject x)
    {
        Onceki.SetActive(false);
        x.SetActive(true);
        if (x.TryGetComponent(out PageNav n))
        {
            n.OnAcilis();
        }
        Onceki = x;
    }
    
    // UI
    public void PanelAc(PanelCesit panel)
    {
        switch (panel)
        {
            case PanelCesit.yazi:
                PaneliAc(YaziPaneli);
                break;

            case PanelCesit.format:
                PaneliAc(FormatPaneli);
                break;

            case PanelCesit.karsilastir:
                PaneliAc(KarsilastirPaneli);
                break;

            case PanelCesit.kaydet:
                PaneliAc(KaydetPaneli);
                break;
        }
    }

}



[System.Serializable]
public enum PanelCesit
{
    yazi,
    format,
    karsilastir,
    kaydet
}
