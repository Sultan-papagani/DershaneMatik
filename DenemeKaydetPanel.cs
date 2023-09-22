using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DenemeKaydetPanel : MonoBehaviour
{
    public SonucGosterici goster;

    public TMP_InputField isim;
    public Toggle tyt,ayt;
    
    public int tytayt;

    bool MAL_BUGU_ENGELLE = false;

    public void tyttiki(bool t)
    {
        if (MAL_BUGU_ENGELLE){MAL_BUGU_ENGELLE = false; return;}
        if (ayt.isOn){MAL_BUGU_ENGELLE= true;ayt.isOn = false;}
        if (t){tytayt = 0;}else{tytayt = 1; MAL_BUGU_ENGELLE= true; ayt.isOn = true; }
        //tytayt = 0;
    }

    public void ayttiki(bool t)
    {
        if (MAL_BUGU_ENGELLE){MAL_BUGU_ENGELLE = false; return;}
        if (tyt.isOn){MAL_BUGU_ENGELLE= true;tyt.isOn = false;}
        if (t){tytayt = 1;} else{tytayt=0; MAL_BUGU_ENGELLE = true; tyt.isOn = true;}
        //tytayt = 1; // ayt 1
    }

    public void Kaydet()
    {
        goster.KaydetmeEkraniCallback(isim.text, tytayt);
    }
}
