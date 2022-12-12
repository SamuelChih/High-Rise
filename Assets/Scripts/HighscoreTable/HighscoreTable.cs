using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private RectTransform templateRectTransform;
    private List<Transform> highscoreEntryTransformList;
    private Highscores highscores;

    private float templateHeight = 40f;

    private void Awake()
    {
        PlayerPrefs.SetString("highscoreTable", "{}");
        PlayerPrefs.Save();
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        templateRectTransform = entryTemplate.GetComponent<RectTransform>();

        entryTemplate.gameObject.SetActive(false);

        // string jsonString = PlayerPrefs.GetString("highscoreTable");
        // highscores = JsonUtility.FromJson<Highscores>(jsonString);
    }

    public void ShowHighscores()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = templateRectTransform.anchoredPosition + new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
            default: rankString = rank + "TH"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        float score = highscoreEntry.score;
        TimeSpan time = TimeSpan.FromSeconds(score);
        entryTransform.Find("scoreText").GetComponent<Text>().text = time.ToString(@"mm\:ss\:fff");

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }

        switch (rank)
        {
            case 1:
                entryTransform.Find("medal").GetComponent<Image>().color = new Color(255/255.0f, 210/255.0f, 0/255.0f);
                break;
            case 2:
                entryTransform.Find("medal").GetComponent<Image>().color = new Color(198/255.0f, 198/255.0f, 198/255.0f);
                break;
            case 3:
                entryTransform.Find("medal").GetComponent<Image>().color = new Color(183/255.0f, 111/255.0f, 86/255.0f);
                break;
            default:
                entryTransform.Find("medal").gameObject.SetActive(false);
                break;
        }

        transformList.Add(entryTransform);
    }

    public void AddHighScoreEntry(float score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public float score;
        public string name;
    }

}
