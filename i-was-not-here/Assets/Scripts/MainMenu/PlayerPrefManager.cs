using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    public int[] Records;
    public float GeneralSound;
    public float Volume;
    public float Music;


    void Awake()
    {
        Records = new int[10];

        if (PlayerPrefs.HasKey("NotFirstLaunch"))
        {
            // Not first launch, get existing PlayerPrefs
            Records[0] = PlayerPrefs.GetInt("Top1");
            Records[1] = PlayerPrefs.GetInt("Top2");
            Records[2] = PlayerPrefs.GetInt("Top3");
            Records[3] = PlayerPrefs.GetInt("Top4");
            Records[4] = PlayerPrefs.GetInt("Top5");
            Records[5] = PlayerPrefs.GetInt("Top6");
            Records[6] = PlayerPrefs.GetInt("Top7");
            Records[7] = PlayerPrefs.GetInt("Top8");
            Records[8] = PlayerPrefs.GetInt("Top9");
            Records[9] = PlayerPrefs.GetInt("Top10");

            GeneralSound = PlayerPrefs.GetFloat("GeneralSound");
            Volume = PlayerPrefs.GetFloat("Volume");
            Music = PlayerPrefs.GetFloat("Music");

            PlayerPrefs.SetInt("CurrLevel", 0);
            PlayerPrefs.SetFloat("CurrRep", 100f);

        }
        else
        {
            // First launch, create new PlayerPrefs
            PlayerPrefs.SetString("NotFirstLaunch", "yes");

            PlayerPrefs.SetInt("Top1", 0);
            PlayerPrefs.SetInt("Top2", 0);
            PlayerPrefs.SetInt("Top3", 0);
            PlayerPrefs.SetInt("Top4", 0);
            PlayerPrefs.SetInt("Top5", 0);
            PlayerPrefs.SetInt("Top6", 0);
            PlayerPrefs.SetInt("Top7", 0);
            PlayerPrefs.SetInt("Top8", 0);
            PlayerPrefs.SetInt("Top9", 0);
            PlayerPrefs.SetInt("Top10", 0);

            PlayerPrefs.SetInt("CurrLevel", 0);
            PlayerPrefs.SetFloat("CurrRep", 0);

            PlayerPrefs.SetFloat("GeneralVolume", 50f);
            PlayerPrefs.SetFloat("Sounds", 50f);
            PlayerPrefs.SetFloat("Music", 50f);

            PlayerPrefs.SetInt("CurrLevel", 0);
            PlayerPrefs.SetFloat("CurrRep", 100f);
        }
    }
}
