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
    }

    private void OnCreateButtonClicked()
    {
        SceneManager.LoadScene("MapCreator");
    }
}
