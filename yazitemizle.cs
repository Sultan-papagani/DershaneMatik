using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class yazitemizle : MonoBehaviour
{
    public TMP_InputField text;

    public void OnClick()
    {
        text.text = "";
    }
}
