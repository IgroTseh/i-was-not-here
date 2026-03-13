using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsGameLevelManager : MonoBehaviour
{
    private GameManager gameManager;
    private List<int> records;

    public void Awake()
    {
        gameManager = GameManager.Instance;
        records = new List<int>();

        records.Add(PlayerPrefs.GetInt("Top1"));
        records.Add(PlayerPrefs.GetInt("Top2"));
        records.Add(PlayerPrefs.GetInt("Top3"));
        records.Add(PlayerPrefs.GetInt("Top4"));
        records.Add(PlayerPrefs.GetInt("Top5"));
        records.Add(PlayerPrefs.GetInt("Top6"));
        records.Add(PlayerPrefs.GetInt("Top7"));
        records.Add(PlayerPrefs.GetInt("Top8"));
        records.Add(PlayerPrefs.GetInt("Top9"));
        records.Add(PlayerPrefs.GetInt("Top10"));
    }


    public void SaveChanges()
    {
        PlayerPrefs.SetInt("CurrLevel", gameManager.CurrLevel);
        PlayerPrefs.SetFloat("CurrRep", gameManager.CurrRep);

        UpdateRecords(gameManager.CurrLevel);
    }


    private void UpdateRecords(int currLevel)
    {
        for (int i = 0; i < records.Count - 1; i++)
        {
            if (currLevel > records[i])
            {
                records.Insert(i, currLevel);
                records.RemoveAt(records.Count - 1);
                break;
            }
        }
    }
}
