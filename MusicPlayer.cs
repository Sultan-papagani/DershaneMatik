using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicPlayer : MonoBehaviour
{
    //public Dictionary<AudioClip, string> sesler = new Dictionary<AudioClip, string>();
    public List<AuidoText> sesler = new List<AuidoText>();
    public AudioSource source;
    AudioClip calinan;
    bool donePlaying = true;
    public RectTransform infoPanel;
    public TextMeshProUGUI yaziadi;
    public int slideval = 50;
    public RectTransform animPoint;
    
    void Start()
    {
        infoPanel.gameObject.SetActive(false);

        DateTime x = DateTime.Now;
        if (6 >= x.Hour)
        {
            calinan = GetMusic((int) x.DayOfWeek);
            source.clip = calinan;
            Invoke(nameof(MuzikCal), 15f);
        } 
    }

    public AudioClip GetMusic(int day)
    {
        AuidoText x = sesler[day];
        yaziadi.text = x.name;
        return x.audioClip;
    }

    public void Update() 
    {
        if (donePlaying){return;}
        if (source.clip.length - source.time <= 6f)
        {
            StartCoroutine(AudioFadeScript.FadeOut(source, 6f));
            donePlaying = true;
        }
    }

    public void MuzikCal()
    {
        // süpriz şeysini arttır
        PlayerPrefs.SetInt("eggTriggerCount", PlayerPrefs.GetInt("eggTriggerCount") + 1);

        StartCoroutine(SlideIn());
        StartCoroutine(AudioFadeScript.FadeIn(source, 6f));
        donePlaying = false;
    }

    public IEnumerator SlideIn()
    {
        WaitForEndOfFrame x = new WaitForEndOfFrame();
        infoPanel.gameObject.SetActive(true);

        float orginalPosY = infoPanel.anchoredPosition.y;

        bool arrived = true;
        int LIMIT = 0;
        while (arrived)
        {
            if (infoPanel.anchoredPosition.y == animPoint.anchoredPosition.y)
            {
                arrived = false;
            }
            else
            {
                infoPanel.anchoredPosition = infoPanel.anchoredPosition - new Vector2(0, 1);
            }
            LIMIT++;
            if (LIMIT > 400){arrived = false;}
            yield return x;
        }

        /*for (int i = 0; i<slideval; i++)
        {
            infoPanel.transform.position = 
            infoPanel.transform.position = infoPanel.transform.position - new Vector3(0, 1, 0);
            yield return x;
        }*/

        yield return new WaitForSecondsRealtime(3f);

        LIMIT = 0;
        arrived = true;
        while (arrived)
        {
            if (infoPanel.anchoredPosition.y == orginalPosY)
            {
                arrived = false;
            }
            else
            {
                infoPanel.anchoredPosition = infoPanel.anchoredPosition + new Vector2(0, 1);
            }
            LIMIT++;
            if (LIMIT > 400){arrived = false;}
            yield return x;
        }

        /*
        for (int i = 0; i<slideval; i++)
        {
            infoPanel.transform.position = infoPanel.transform.position + new Vector3(0, 1, 0);
            yield return x;
        }*/

        infoPanel.gameObject.SetActive(false);
    }

}

[System.Serializable]
public struct AuidoText
{
    public AudioClip audioClip;
    public string name;
}
