using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;
    private Label youSurvived;
    private Label cntDays;
    private Label ofInvestigation;

    private void Awake()
    {
        VisualElement root = uiDoc.rootVisualElement;

        Button playAgainButton = root.Q<Button>("PlayAgain");
        playAgainButton.clicked += OnPlayAgainClicked;

        Button toMenuButton = root.Q<Button>("ToMenu");
        toMenuButton.clicked += OnToMenuClicked;

        Button exitButton = root.Q<Button>("Exit");
        exitButton.clicked += OnExitClicked;

        youSurvived = root.Q<Label>("YouSurvived");
        cntDays = root.Q<Label>("CntDays");
        ofInvestigation = root.Q<Label>("OfInvestigation");

        UpdateText();
    }

    private void OnPlayAgainClicked()
    {
        Debug.Log("PlayAgain");
        SceneManager.LoadScene("InitRoom");
    }

    private void OnToMenuClicked()
    {
        Debug.Log("ToMenu");
        SceneManager.LoadScene("MainMenu");
    }

    private void OnExitClicked()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    private void UpdateText()
    {
        int days = PlayerPrefs.GetInt("CurrLevel");

        youSurvived.text = "You survived through";
        cntDays.text = $"{days} days";
        ofInvestigation.text = "of investigation";
    }
}
