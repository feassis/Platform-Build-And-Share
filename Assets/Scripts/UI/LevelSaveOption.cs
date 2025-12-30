using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSaveOption : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI saveNameText;

    private string saveName;

    public event Action<string> onPlayButtonClicked;
    public event Action<string> onDeleteButtonClicked;
    public event Action<string> onSelectButtonClicked;

    public void Init(string saveName)
    {
        this.saveName = saveName;

        saveNameText.text = saveName;
    }

    private void Awake()
    {
        playButton.onClick.AddListener((() => onPlayButtonClicked?.Invoke(saveName)));
        deleteButton.onClick.AddListener(() =>  onDeleteButtonClicked?.Invoke(saveName));
        selectButton.onClick.AddListener(() => onSelectButtonClicked?.Invoke(saveName));
    }
}