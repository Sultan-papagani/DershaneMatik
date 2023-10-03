using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class debugPanel : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void OnScreen(string totaltime, string triggerCount, string openingCount)
    {
        text.text = $"Toplam Süre (Saniye):{totaltime}\nTrigger Count:{triggerCount}\nApp launch count:{openingCount}";
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
