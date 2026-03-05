using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int currRep;
    [SerializeField] private int maxRep;
    public int CurrLevel;
    public float CoeffLevel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        CurrLevel = 1;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("MainMenu");
        CurrLevel++;
    }

    public void ChangeRep(int repDamage)
    {
        currRep -= repDamage;
    }
}
