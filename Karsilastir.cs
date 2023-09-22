using UnityEngine;
using TMPro;
using System.Linq;
using System.Text;
using System;
using System.Collections;

public class Karsilastir : MonoBehaviour, PageNav
{
    public YaziDecoder decoder;

    public TMP_InputField InputField;
    public TMP_InputField SifreGiris;

    public TextMeshProUGUI warningtext;

    public void OnAcilis()
    {
        if (InputField.text == "")
        {
            InputField.text = decoder.data;
        }
    }

    IEnumerator warning(string text, bool sc = false)
    {
        warningtext.text = text;
        if (sc){warningtext.color = Color.green;}
        WaitForSecondsRealtime x = new WaitForSecondsRealtime(4f);
        yield return x;
        if (sc){warningtext.color = Color.black;}
        warningtext.text = "";
    }

    // ui
    public void Oncevir()
    {
        if (InputField.text != "" && SifreGiris.text != "")
        {
            string x = xorIt(SifreGiris.text, InputField.text);
            UniClipboard.SetText(x);
            StartCoroutine(warning("Sonuç panoya kopyalandi!", true));
        }
        else
        {
            StartCoroutine(warning("mesaji ve şifreyi girmelisin"));
        }
    }

    public void OnTersineCevir()
    {
        if (InputField.text != "" && SifreGiris.text != "")
        {
            string x = xorIt(SifreGiris.text, InputField.text);
            InputField.text = x;
            StartCoroutine(warning("Çözme işlemi başarili", true));
        }
        else
        {
            StartCoroutine(warning("mesaji ve şifreyi girmelisin tabikii ??"));
        }
    }

    public static string xorIt(string key, string input)
    {
        StringBuilder sb = new StringBuilder();
        for(int i=0; i < input.Length; i++)
            sb.Append((char)(input[i] ^ key[(i % key.Length)]));
        String result = sb.ToString ();

        return result;
    }

}
