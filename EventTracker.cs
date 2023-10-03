using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTracker : MonoBehaviour
{
    
    public debugPanel panel;

    void Start()
    {
        panel.gameObject.SetActive(false);

        // keyler yoksa oluştur.
        if (!PlayerPrefs.HasKey("totalTime")){PlayerPrefs.SetFloat("totalTime", 0);}
        if (!PlayerPrefs.HasKey("totalOpenCount")){PlayerPrefs.SetInt("totalOpenCount", 0);}
        if (!PlayerPrefs.HasKey("eggTriggerCount")){PlayerPrefs.SetInt("eggTriggerCount", 0);}

        // open countu arttır.
        PlayerPrefs.SetInt("totalOpenCount", PlayerPrefs.GetInt("totalOpenCount") + 1);
    }

    public void SpawnPanel()
    {
        panel.gameObject.SetActive(true);
        panel.OnScreen(PlayerPrefs.GetFloat("totalTime").ToString(), PlayerPrefs.GetInt("eggTriggerCount").ToString(), PlayerPrefs.GetInt("totalOpenCount").ToString());
    }

    public void OnApplicationQuit()
    {
        // total süreyi kaydet
        PlayerPrefs.SetFloat("totalTime", PlayerPrefs.GetFloat("totalTime")+Time.realtimeSinceStartup);
    }
}
