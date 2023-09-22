using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class YaziDecoder : MonoBehaviour
{
    [SerializeField] private TMP_InputField giris;

    Dictionary<string, (string, int)> ders_isimleri = new Dictionary<string, (string, int)> {
        {"TUR", ("Türkçe", 40)},
        {"TAR",("Tarih", 5)},
        {"COG",("Coğrafya", 5)},
        {"FEL",("Felsefe", 5)},
        {"DIN",("Din", 5)},
        {"T.MAT",("Tyt Mat", 40)},
        {"GEO",("Geometri", 0)},
        {"FIZ",("Fizik", 7)},
        {"KIM",("Kimya", 7)},
        {"BIY",("Biyoloji", 6)},
        {"SFEL",("S.Felsefe", 5)},
        {"SOZ EDB",("Edebiyat", 40)},
        {"TAR1",("Tarih1", 5)},
        {"COG1",("Coğrafya1", 5)},
        {"TAR2",("Tarih2", 5)},
        {"COG2",("Coğrafya2", 5)},
        {"FEL1",("Felsefe1", 5)},
        {"PSI",("PSI", 15)},
        {"SOS",("Sosyal", 5)},
        {"MAN",("MAN", 15)},
        {"DIN1",("Din1", 5)},
        {"SFEL1",("S.Felsefe1", 5)},
        {"MAT1",("Matematik1", 40)},
        {"GEO1",("Geometri1", 0)},
        {"EDB",("Edebiyat", 15)},
        {"KIM1",("Kimya1", 7)},
        {"BIY1",("Biyoloji1", 6)},
        };
        /*{"TOP N","Toplam Net"},
        {"G.DER","Genel Dershane"},
        {"K:DER","Kurumsal Dershane"}*/

    public TytSonucu ensonSonuc = null;
    public GameObject yesilButon;
    public string data;

    private void Start() {
        //DecodeText();
        Application.targetFrameRate = 60;
        //Screen.SetResolution(1080,1920,true);
        // s
    }

    public void oneditstring()
    {
        data = giris.text;

        if (!data.Contains("YKS SONUCU"))
        {
            yesilButon.SetActive(false);
            return;
        }
        
        DecodeText();
        yesilButon.SetActive(true);
    }

    /*
    1 - yazinin validliğini doğrula
    2 - parse
    */
    public void DecodeText()
    {

        if (!data.Contains("YKS SONUCU"))
        {
            return;
        }

        ensonSonuc = new TytSonucu();

        int index = data.IndexOf("YKS SONUCU");

        foreach(KeyValuePair<string,(string, int)> isim in ders_isimleri)
        {
            Match ders_indexsi = Regex.Match(data, isim.Key);
            int ders_indexi = ders_indexsi.Index;
            if (ders_indexsi.Success)
            {
                int dogruindexi = data.IndexOf("D:", ders_indexi); 
                int yanlisindexi = data.IndexOf("Y:", ders_indexi); 
                int netindexi = data.IndexOf("N:", ders_indexi); 

                string dogru = data.Substring(dogruindexi, yanlisindexi - dogruindexi).Replace("D:", "").Trim();
                string yanlis = data.Substring(yanlisindexi, netindexi - yanlisindexi).Replace("Y:", "").Trim();
                int bitis = data.IndexOf(" ", netindexi+3);
                string net = data.Substring(netindexi, bitis - netindexi).Replace("N:", "").Trim();


                DersSonucu x = dersparse(isim.Value.Item1, dogru, yanlis, net, isim.Value.Item2);
                ensonSonuc.dersler.Add(x);
            }
        }

        ensonSonuc.ToplamNet = GetToLastField("TOP N:");
        ensonSonuc.TytPuan = GetToLastField("TYT:");
        ensonSonuc.AytPuan = GetToLastField("AYT");
        ensonSonuc.GenelDershane = GetToLastField("G.DER");
        ensonSonuc.KurumDershane = GetToLastField("K.DER");

        
    }

    public string GetToLastField(string keyword)
    {
        Match match = Regex.Match(data, keyword);
        string sonuc = "";
        if (match.Success)
        {
            int d = data.IndexOf(":", match.Index);
            d+=1;
            bool sc = true;
            while (sc)
            {
                if (Char.IsDigit(data[d]))
                {
                    sonuc += data[d];
                }
                else
                {
                    if (!(data[d] == ' ' || data[d] == ','))
                    {
                        sc = false;
                    }
                    if (data[d] == ',')
                    {
                        sonuc += data[d];
                    }
                }
                d++;
            }
        }

        return sonuc;

    }

    public DersSonucu dersparse(string dersadi, string dogru, string yanlis, string net, int toplamsoru)
    {
        DersSonucu sonuc = new DersSonucu();

        sonuc.DersAdi = dersadi;
        sonuc.Dogru = dogru;
        sonuc.Yanlis = yanlis;
        sonuc.Net = net;

        if (toplamsoru != 0)
        {
            int bos = toplamsoru - (int.Parse(sonuc.Dogru) + int.Parse(sonuc.Yanlis));
            sonuc.bos = bos.ToString();
        } else {sonuc.bos = "-";}

        return sonuc;
    }
}

[System.Serializable]
public class TytSonucu
{
    public string Ad; // bu artık deneme adi (kaydetme için.)
    public int modu; // tytmi ayt mi
    public List<DersSonucu> dersler = new List<DersSonucu>();
    public string ToplamNet;
    public string TytPuan;
    public string AytPuan;
    public string GenelDershane;
    public string KurumDershane;

    public TytSonucu(string ad, List<DersSonucu> dersS, string toplamne, string tytpu, string aytpu, string genelders, string kurumders, int moduu)
    {
        Ad = ad;
        dersler = dersS;
        ToplamNet = toplamne;
        TytPuan = tytpu;
        AytPuan = aytpu;
        GenelDershane = genelders;
        KurumDershane = kurumders;
        modu = moduu;
    }

    public TytSonucu(){}
}

[System.Serializable]
public struct DersSonucu
{
    public string DersAdi;
    public string Dogru, Yanlis;
    public string Net;
    public string bos;
}
