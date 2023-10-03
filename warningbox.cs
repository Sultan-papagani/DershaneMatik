using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class warningbox : MonoBehaviour
{
    public TextMeshProUGUI textt, title;

    public void Warning(string text, string _title)
    {
        textt.text = text;
        title.text = _title;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
