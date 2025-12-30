using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button CreateLevelButton;
    [SerializeField] private Button LoadLevelButton;
    [SerializeField] private Button LevelsButton;

    private void Awake()
    {
        CreateLevelButton.onClick.AddListener(OnCreateButtonClicked);
        LoadLevelButton.onClick.AddListener(OnLoadButtonClicked);
        LevelsButton.onClick.AddListener(OnPlayLEvelButtonClicked);
    }

    private void OnPlayLEvelButtonClicked()
    {
        SceneManager.LoadScene("PlayMenu");
    }

    private void OnLoadButtonClicked()
    {
        SceneManager.LoadScene("LoadMenu");
    }

    private void OnCreateButtonClicked()
    {
        LevelCreatorManager.Open(true);
    }
}
