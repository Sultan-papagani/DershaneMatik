using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BilgiPanel : MonoBehaviour
{
    public TextMeshProUGUI toplamnet, tytpuan, aytpuan, geneldershane, kurumdershane;

    public void setup(string _toplamnet, string _tytpuan, string _aytpuan, string _geneldershane, string _kurumdershane)
    {
        toplamnet.text = "Toplam Net: "+_toplamnet;
        tytpuan.text = "Tyt Puan: "+_tytpuan;
        aytpuan.text = "Ayt Puan: "+_aytpuan;
        geneldershane.text = "Genel Dershane: "+_geneldershane;
        kurumdershane.text = "Kurum Dershane: "+_kurumdershane;
    }
}
