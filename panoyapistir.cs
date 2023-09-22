using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class panoyapistir : MonoBehaviour
{
    public TMP_InputField giris;

    public void OnClick()
    {
        giris.text = UniClipboard.GetText();
    }
}
