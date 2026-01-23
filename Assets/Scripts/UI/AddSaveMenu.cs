using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AddSaveMenu : MonoBehaviour
{
    [SerializeField] protected TMP_InputField saveCodeInput;
    [SerializeField] protected Button addSaveButtom;

    public event Action OnSaveAdded;

    private void Awake()
    {
        addSaveButtom.onClick.AddListener(AddSave);
    }

    private void AddSave()
    {
        var json = JsonEncoder.Decode(saveCodeInput.text);
        var data = JsonUtility.FromJson<LevelData>(json);

        LevelSaverService.SaveLevelJson(data.Name, GameConstants.SAVE_FOLDER_PATH, json);

        OnSaveAdded?.Invoke();
    }
}
