using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class KaydetSistem : MonoBehaviour, PageNav
{
    public JsonTytSonuc data;

    public YaziDecoder decoder;
	public string path;

    public Transform listeobjesi;
    public GameObject TytObjesi, AytObjesi;

    public GameObject ActionPanel;

    public PageSelector pageSelector;

    public SonucGosterici sonucGosterici;

    bool yuklendi = false;

    int callbackindex = 0;
    public void ActionPanelDokunmaCallback(int which)
    {
        callbackindex = which;
        ActionPanel.SetActive(true);
    }

    public void ActionPanelIptal()
    {
        ActionPanel.SetActive(false);
    }

    public void ActionPanelVeriyiAc()
    {
        ActionPanel.SetActive(false);
        decoder.ensonSonuc = data.sonuc[callbackindex];
        
        sonucGosterici.gameObject.SetActive(true); // kapalıdır zaten
        sonucGosterici.KaydetSistemdenGelenIndex(callbackindex); // bunu paneli açtıktan sonra çağır ki işlesin

        pageSelector.PanelAc(PanelCesit.format); // paneli açıyoruz ama bizide kapatıyor :/
    }

    public void ActionPanelVeriyiSil()
    {
        ActionPanel.SetActive(false);
        data.sonuc.RemoveAt(callbackindex);
        SaveState();
        YenileUIButon();
    }

	public void Start ()
	{
		path = System.IO.Path.Combine(Application.persistentDataPath, "data.json");
        path = path.Replace(@"\", "/");
	}

    public void SaveState()
    {
        Start();
        string jsonDataString = JsonUtility.ToJson(data, true);
		File.WriteAllText(path, jsonDataString);
    }

    public void SaveEditData(TytSonucu sonuc, int orginalIndex)
    {
        Start(); // EFSANE SİSTEM
        // eski veriyi oku
        if(File.Exists(path))
        {
            string loadedJsonDataString = File.ReadAllText(path);
            data = JsonUtility.FromJson<JsonTytSonuc>(loadedJsonDataString);
        }
        else
        {
            data = new JsonTytSonuc();
            data.sonuc = new List<TytSonucu>();
        }

        // değşitir
        data.sonuc[orginalIndex] = sonuc;
        
        // kaydet
        string jsonDataString = JsonUtility.ToJson(data, true);
		File.WriteAllText(path, jsonDataString);

        YenileUIButon();
    }

    public void SaveCustomData(TytSonucu sonuc)
    {
        Start(); // EFSANE SİSTEM
        // eski veriyi oku
        if(File.Exists(path))
        {
            string loadedJsonDataString = File.ReadAllText(path);
            data = JsonUtility.FromJson<JsonTytSonuc>(loadedJsonDataString);
        }
        else
        {
            data = new JsonTytSonuc();
            data.sonuc = new List<TytSonucu>();
        }

        // append (değiştirildi)
        data.sonuc.Add(sonuc);
        
        // kaydet
        string jsonDataString = JsonUtility.ToJson(data, true);
		File.WriteAllText(path, jsonDataString);

        YenileUIButon();
    }

    public void SaveNewData()
    {
        Start(); // EFSANE SİSTEM
        // eski veriyi oku
        if(File.Exists(path))
        {
            string loadedJsonDataString = File.ReadAllText(path);
            data = JsonUtility.FromJson<JsonTytSonuc>(loadedJsonDataString);
        }
        else
        {
            data = new JsonTytSonuc();
            data.sonuc = new List<TytSonucu>();
        }

        // append
        data.sonuc.Add(decoder.ensonSonuc);
        
        // kaydet
        string jsonDataString = JsonUtility.ToJson(data, true);
		File.WriteAllText(path, jsonDataString);

        YenileUIButon();
    }

    public void ReciveNewData()
    {
        if(!File.Exists(path)) {return;}
        if (yuklendi){return;}

        string loadedJsonDataString = File.ReadAllText(path);

		data = JsonUtility.FromJson<JsonTytSonuc>(loadedJsonDataString);

        foreach (Transform child in listeobjesi) 
        {
	        GameObject.Destroy(child.gameObject);
        }

        foreach(TytSonucu sonuc in data.sonuc)
        {
            // ayt
            if (sonuc.modu == 1)
            {
                GameObject x = Instantiate(AytObjesi, listeobjesi);
                x.GetComponent<DenemeAYTInfo>().setup(sonuc);
            }
            else // tyt
            {
                GameObject x = Instantiate(TytObjesi, listeobjesi);
                x.GetComponent<DenemeAYTInfo>().setup(sonuc);
            }
        }

        yuklendi = true;

    }

    public void YenileUIButon()
    {
        yuklendi = false;
        ReciveNewData();
    }

    public void OnAcilis()
    {
        // sayfayı açınca yükle.
        Start();
        ReciveNewData();
    }
}

[System.Serializable]
public class JsonTytSonuc
{
    public List<TytSonucu> sonuc;

    public JsonTytSonuc (List<TytSonucu> sonucc)
    {
        sonuc = sonucc;
    }

    public JsonTytSonuc (){}
}
