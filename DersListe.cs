using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DersListe : MonoBehaviour
{
    public TextMeshProUGUI ad ,dogru, yanlis, bos, net;

    public void Setup(DersSonucu ders)
    {
        ad.text = ders.DersAdi;
        dogru.text = ders.Dogru;
        yanlis.text = ders.Yanlis;
        net.text = ders.Net;
        bos.text = ders.bos;
    }
}
