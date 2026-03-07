using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsGameLevelManager : MonoBehaviour
{
    private GameManager gameManager;
    private List<int> records;

    public void Satrt()
    {
        gameManager = GameManager.Instance;
        records = new List<int>();

        records[0] = PlayerPrefs.GetInt("Top1");
        records[1] = PlayerPrefs.GetInt("Top2");
        records[2] = PlayerPrefs.GetInt("Top3");
        records[3] = PlayerPrefs.GetInt("Top4");
        records[4] = PlayerPrefs.GetInt("Top5");
        records[5] = PlayerPrefs.GetInt("Top6");
        records[6] = PlayerPrefs.GetInt("Top7");
        records[7] = PlayerPrefs.GetInt("Top8");
        records[8] = PlayerPrefs.GetInt("Top9");
        records[9] = PlayerPrefs.GetInt("Top10");
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
