using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitRoomController : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetFloat("CurrRep", 100f);
        PlayerPrefs.SetInt("CurrLevel", 0);

        SceneManager.LoadScene("GameLevel");
    }
}
