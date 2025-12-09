using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonMode : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button stopButton;

    private LevelCreatorManager levelCreatorManager;

    private void Awake()
    {
        playButton.onClick.AddListener(() => PlayModeToggle(true));
        stopButton.onClick.AddListener( () => PlayModeToggle(false));
    }

    private void PlayModeToggle(bool playMode)
    {
        if (playMode)
        {
            playButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);
        }
        else
        {
            playButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
        }

        levelCreatorManager.ToggleGameMode(playMode);
    }

    public void Init(LevelCreatorManager levelCreatorManager)
    {
        this.levelCreatorManager = levelCreatorManager;
    }


}
