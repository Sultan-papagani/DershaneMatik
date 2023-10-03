using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ManuelDenemeEkle : MonoBehaviour
{
    public TMP_InputField denemeadi, toplamnot, tytpuan, aytpuan, genelDers, kurumDers;
    public Toggle tyt_tiki, ayt_tiki;

    public GameObject ListeTransform;
    public GameObject[] DersPaneliPrefabs; 
    public List<DersEditPanel> DenemePaneller = new List<DersEditPanel>();

    public KaydetSistem kaydetSistem;

    public warningbox WarningPanel;

    public PageSelector pageSelector;

    bool editGeldi = false;
    int editIndex = 0;

    // 1 - ayt
    // 0 - tyt

    int i = 0;
    int max_spawn = 0;
    public void DersPaneliSpawnla()
    {
        if (i == 0){i = 1;}
        else{i = 0;}
        DenemePaneller.Add(Instantiate(DersPaneliPrefabs[i], ListeTransform.transform).GetComponent<DersEditPanel>());
    }

    public int tytayt = 0;
    bool MAL_BUGU_ENGELLE = false;

    public void tyttiki(bool t)
    {
        if (MAL_BUGU_ENGELLE){MAL_BUGU_ENGELLE = false; return;}
        if (ayt_tiki.isOn){MAL_BUGU_ENGELLE= true;ayt_tiki.isOn = false;}
        if (t){tytayt = 0;}else{tytayt = 1; MAL_BUGU_ENGELLE= true; ayt_tiki.isOn = true; }
    }

    public void ayttiki(bool t)
    {
        if (MAL_BUGU_ENGELLE){MAL_BUGU_ENGELLE = false; return;}
        if (tyt_tiki.isOn){MAL_BUGU_ENGELLE= true;tyt_tiki.isOn = false;}
        if (t){tytayt = 1;} else{tytayt=0; MAL_BUGU_ENGELLE = true; tyt_tiki.isOn = true;}
    }

    public void DersEkleButon()
    {
        max_spawn++;
        if (max_spawn > 20) {return;}
        DersPaneliSpawnla();
    }

    public void DersSilButon()
    {
        if (!(max_spawn == 0)){ max_spawn--;}
        if (DenemePaneller.Count == 0) {return;}
        Destroy(DenemePaneller[DenemePaneller.Count -1].gameObject);
        DenemePaneller.RemoveAt(DenemePaneller.Count -1);
    }
    
    public void KaydetUI()
    {
        if (denemeadi.text.Trim() == ""){Error("Denemeye isim vermelisin."); return;}

        int i = 1;
        foreach (DersEditPanel x in DenemePaneller)
        {
            if (!x.Validate())
            {
                Error($"{i}.Derse isim vermelisin.");
                return;
            }
            i++;
        }

        Kaydet();   

    }

    public void SonucGostericidenEditIcinYukle(int index)
    {
        editGeldi = true;
        editIndex = index; // AMINA KODUMUN BUGU NE UZUN SÜRDÜ 

        pageSelector.PanelAc(PanelCesit.karsilastir); // burayı tekrar açarak navigation bugu engelle.

        // json listesindeki index.
        kaydetSistem.gameObject.SetActive(true);
        TytSonucu sonuc = kaydetSistem.data.sonuc[index];
        //sonuc.dersler = new List<DersSonucu>();
        //sonuc.dersler = kaydetSistem.data.sonuc[index].dersler;
        kaydetSistem.gameObject.SetActive(false);

        denemeadi.text = sonuc.Ad;
        toplamnot.text = sonuc.ToplamNet;
        tytpuan.text = sonuc.TytPuan;
        aytpuan.text = sonuc.AytPuan;
        genelDers.text = sonuc.GenelDershane;
        kurumDers.text = sonuc.KurumDershane;

        if (sonuc.modu == 1)
        {
            // ayt
            ayttiki(true);
        }
        else
        {
            // tyt
            tyttiki(true);
        }

        tytayt = sonuc.modu;

        int p = 0;

        // dersleride ekle.
        foreach (DersSonucu ders in sonuc.dersler)
        {
            DersEkleButon();
            DersEditPanel y = DenemePaneller[p];
            y.dersAdi.text = ders.DersAdi;
            y.dogru.text = ders.Dogru;
            y.yanlis.text = ders.Yanlis;
            y.bos.text = ders.bos;
            y.net.text = ders.Net;

            p++;
        }

    }

    public void Kaydet()
    {
        kaydetSistem.gameObject.SetActive(true);
        
        TytSonucu sonuc = new TytSonucu();
        sonuc.Ad = denemeadi.text;
        sonuc.modu = tytayt;
        sonuc.ToplamNet = toplamnot.text;
        sonuc.TytPuan = tytpuan.text;
        sonuc.AytPuan = aytpuan.text;
        sonuc.GenelDershane = genelDers.text;
        sonuc.KurumDershane = kurumDers.text;
        //sonuc.dersler = new List<DersSonucu>();

        foreach(DersEditPanel x in DenemePaneller)
        {
            DersSonucu ders = new DersSonucu();
            ders.DersAdi = x.dersAdi.text;
            ders.Dogru = x.dogru.text;
            ders.Yanlis = x.yanlis.text;
            ders.Net = x.net.text;
            ders.bos = x.net.text;
            sonuc.dersler.Add(ders);
        }

        if (editGeldi)
        {
            // bu oluşturulanı index ile değiştirip kaydetcez, yeni eklenmeyecek
           
            kaydetSistem.SaveEditData(sonuc, editIndex);
        }
        else
        {
            // yeni veri
            kaydetSistem.SaveCustomData(sonuc);
        }


        kaydetSistem.gameObject.SetActive(false);
        Error("Başariyla Kaydedildi", "Tebrikleeer");
        Resetstate();
    }

    public void Error(string x, string y = "Hata")
    {
        WarningPanel.gameObject.SetActive(true);
        WarningPanel.Warning(x, y);
    }

    public void Resetstate()
    {
        denemeadi.text = "";
        toplamnot.text = "";
        tytpuan.text = "";
        aytpuan.text = "";
        genelDers.text = "";
        kurumDers.text = "";

        foreach (DersEditPanel x in DenemePaneller)
        {
            Destroy(x.gameObject);
        }

        DenemePaneller.Clear();
        editGeldi = false;
    }
}
