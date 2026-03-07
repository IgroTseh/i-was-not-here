using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public float CurrRep;
    [SerializeField] private float maxRep = 100f;
    public int CurrLevel;
    public float CoeffLevel;

    [SerializeField] BoardManager boardManager;
    [SerializeField] UIDocument uiDoc;
    [SerializeField] PlayerPrefsGameLevelManager prefsManager;
    private Label repLabel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
    }

    private void Start()
    {
        repLabel = uiDoc.rootVisualElement.Q<Label>("CurrRep");
        

        if (PlayerPrefs.HasKey("CurrLevel"))
        {
            CurrLevel = PlayerPrefs.GetInt("CurrLevel");
            CurrRep = PlayerPrefs.GetFloat("CurrRep");
        }
        else
        {
            CurrLevel = 1;
            CurrRep = maxRep;
        }
        
        boardManager.Init();
        ChangeRep(0f);
    }


    public void NextLevel()
    {
        CurrLevel++;
        prefsManager.SaveChanges();
        SceneManager.LoadScene("GameLevel");     
    }

    public void ChangeRep(float repDamage)
    {
        CurrRep += repDamage;
        repLabel.text = $"{CurrRep}";

        if (CurrRep >= maxRep * 0.75)
            repLabel.style.color = Color.green;
        else if (CurrRep >= maxRep * 0.4 && CurrRep < maxRep * 0.75)
            repLabel.style.color = Color.yellow;
        else if (CurrRep > maxRep * 0 && CurrRep < maxRep * 0.4)
            repLabel.style.color = Color.red;
        else
            repLabel.style.color = Color.black;

        if (CurrRep <= 0)
            GameOver();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
