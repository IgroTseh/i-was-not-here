using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;

    private void Awake()
    {
        VisualElement root = uiDoc.rootVisualElement;

        Button playButton = root.Q<Button>("PlayButton");
        playButton.clicked += OnPlayClicked;

        Button settingsButton = root.Q<Button>("SettingsButton");
        settingsButton.clicked += OnSettingsClicked;

        Button howToPlayButton = root.Q<Button>("HowToPlayButton");
        howToPlayButton.clicked += OnHowToPlayClicked;

        Button exitButton = root.Q<Button>("ExitButton");
        exitButton.clicked += OnExitClicked;
    }

    private void OnPlayClicked()
    {
        Debug.Log("Play!");
        SceneManager.LoadScene("GameLevel");
    }

    private void OnSettingsClicked()
    {
        Debug.Log("Settings");
        // call settings screen
    }

    private void OnHowToPlayClicked()
    {
        Debug.Log("HowToPlay");
        // call how to play screen
    }

    private void OnExitClicked()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
