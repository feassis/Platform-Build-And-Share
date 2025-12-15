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
        stopButton.onClick.AddListener(() => PlayModeToggle(false));
    }
        
    private void Start()
    {
        
    }

    private void LevelCreatorManager_OnPlaymodeStart()
    {
        playButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);
    }

    private void LevelCreatorManager_OnPlaymodeEnd()
    {
        playButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(false);
    }

    private void PlayModeToggle(bool playMode)
    {
        levelCreatorManager.ToggleGameMode(playMode);
    }

    public void Init(LevelCreatorManager levelCreatorManager)
    {
        this.levelCreatorManager = levelCreatorManager;

        levelCreatorManager.OnPlaymodeStart += LevelCreatorManager_OnPlaymodeStart;
        levelCreatorManager.OnPlaymodeEnd += LevelCreatorManager_OnPlaymodeEnd;
    }


}
