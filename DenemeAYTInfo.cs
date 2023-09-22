using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DenemeAYTInfo : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI denemeadi, puani, toplamnet, Siralama;

    public void OnPointerClick(PointerEventData eventData)
    {
        // burda action menü aç sil yada yükle
        GetComponentInParent<KaydetSistem>().ActionPanelDokunmaCallback(transform.GetSiblingIndex());
    }

    public void setup(TytSonucu sonuc)
    {
        denemeadi.text = sonuc.Ad;
        if (sonuc.modu == 1){puani.text = sonuc.AytPuan;}else{puani.text= sonuc.TytPuan;}
        toplamnet.text = sonuc.ToplamNet;
        Siralama.text = $"Genel D:{sonuc.GenelDershane} Kurum D:{sonuc.KurumDershane}";
    }
}
