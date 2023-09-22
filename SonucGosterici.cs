using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SonucGosterici : MonoBehaviour, PageNav
{
    [SerializeField] private YaziDecoder decoder;

    public KaydetSistem kaydetSistem;

    public GameObject DersSonucObjesi;
    public GameObject BilgiPaneliObjesi;

    public GameObject KaydetmeEkrani;

    public Transform spawnlist;

    public void UIKaydet()
    {
        if (decoder.ensonSonuc == null){return;}
        KaydetmeEkrani.SetActive(true);
    }

    public void KaydetmeEkraniCallback(string denemeadi, int denememodu)
    {
        if (denemeadi == "")
        {
            KaydetmeEkrani.SetActive(false);
            return;
        }

        kaydetSistem.gameObject.SetActive(true);
        decoder.ensonSonuc.Ad = denemeadi;
        decoder.ensonSonuc.modu = denememodu;
        kaydetSistem.SaveNewData();
        KaydetmeEkrani.SetActive(false);
        kaydetSistem.gameObject.SetActive(false);
    }


    public void OnAcilis()
    {
        if (decoder.ensonSonuc == null){return;}
        if (!(decoder.ensonSonuc.dersler.Count > 0)){return;} // bazı yerlerde ensonsonuc u tanımlıyoz

        foreach (Transform child in spawnlist) 
        {
	        GameObject.Destroy(child.gameObject);
        }


        GameObject bilgipanel = Instantiate(BilgiPaneliObjesi, spawnlist);
        bilgipanel.GetComponent<BilgiPanel>().setup(decoder.ensonSonuc.ToplamNet, decoder.ensonSonuc.TytPuan, decoder.ensonSonuc.AytPuan, decoder.ensonSonuc.GenelDershane, decoder.ensonSonuc.KurumDershane);

        foreach(DersSonucu x in decoder.ensonSonuc.dersler)
        {
            GameObject y = Instantiate(DersSonucObjesi, spawnlist);
            y.GetComponent<DersListe>().Setup(x);
        }

    }
}
