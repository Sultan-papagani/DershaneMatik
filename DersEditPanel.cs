using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DersEditPanel : MonoBehaviour
{
    public TMP_InputField dersAdi, dogru, yanlis, bos, net;

    public bool Validate()
    {
        bool x = !(dersAdi.text.Trim() == "");

        if (dogru.text.Trim() == ""){dogru.text = "0";};
        if (yanlis.text.Trim() == ""){yanlis.text = "0";};
        if (bos.text.Trim() == ""){bos.text = "0";};
        if (net.text.Trim() == ""){net.text = "0";};

        return x;
    }
}
