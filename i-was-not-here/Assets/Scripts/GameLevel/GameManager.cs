using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float currRep;
    [SerializeField] private float maxRep = 100f;
    public int CurrLevel;
    public float CoeffLevel;

    [SerializeField] BoardManager boardManager;
    [SerializeField] UIDocument uiDoc;
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

        CurrLevel = 1;
        currRep = maxRep;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("MainMenu");
        CurrLevel++;
    }

    public void ChangeRep(float repDamage)
    {
        currRep += repDamage;
        repLabel.text = $"{currRep}";

        if (currRep >= maxRep * 0.75)
            repLabel.style.color = Color.green;
        else if (currRep >= maxRep * 0.4 && currRep < maxRep * 0.75)
            repLabel.style.color = Color.yellow;
        else if (currRep > maxRep * 0 && currRep < maxRep * 0.4)
            repLabel.style.color = Color.red;
        else
            repLabel.style.color = Color.black;
    }
}
