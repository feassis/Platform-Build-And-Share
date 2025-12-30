using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SavePopupUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField saveNameInput;

    [SerializeField] private Button saveButton;

    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject overrideSavePopup;

    [SerializeField] private Button agreeButton;

    [SerializeField] private Button disagreeButton;


    private LevelLoaderService levelLoaderService;

    public event Action<string> OnSaveConfirm;
    public event Action OnSavePopupClose;

    private void Awake()
    {
        saveButton.onClick.AddListener(OnSaveButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        agreeButton.onClick.AddListener(OnAgreeButtonClicked);
        disagreeButton.onClick.AddListener(OnDisgreeButtonClicked);
    }

    private void OnDisgreeButtonClicked()
    {
        CloseOverridePopup();
    }

    private void OnAgreeButtonClicked()
    {
        OnSaveConfirm?.Invoke(saveNameInput.text);

    }

    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
        OnSavePopupClose?.Invoke();
    }

    private void OnSaveButtonClicked()
    {
        if (levelLoaderService.SaveExists(saveNameInput.text))
        {
            OpenOverridePopup();
        }
        else
        {
            OnSaveConfirm?.Invoke(saveNameInput.text);
        }
    }

    private void OpenOverridePopup()
    {
        overrideSavePopup.SetActive(true);
    }

    private void CloseOverridePopup()
    {
        overrideSavePopup.SetActive(false);
    }

    public void Init(LevelLoaderService levelLoaderService)
    {
        this.levelLoaderService = levelLoaderService;
    }
}
