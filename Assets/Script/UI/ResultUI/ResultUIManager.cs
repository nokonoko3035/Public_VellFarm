using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultUIManager : MonoBehaviour
{
    public Text[] rankTexts;

    /// <summary>
    /// ランキングの表示
    /// </summary>
    /// <param name="entries"></param>
    public void DisplayRanking(List<RankingEntry> entries)
    {
        Debug.Log("ランキングを出します");
        for (int i = 0; i < rankTexts.Length; i++)
        {
            if (i < entries.Count)
            {
                string name = entries[i].teamName;
                float time = entries[i].time;
                int minutes = Mathf.FloorToInt(time / 60f);
                int seconds = Mathf.FloorToInt(time % 60f);
                rankTexts[i].text = $"{i + 1}. {name} - {minutes:00}:{seconds:00}";
            }
            else
            {
                rankTexts[i].text = $"{i + 1}. ---";
            }
        }
    }
}
