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
    }

    private void OnPlayClicked()
    {
        Debug.Log("Play!");
        SceneManager.LoadScene("GameLevel");
    }
}
