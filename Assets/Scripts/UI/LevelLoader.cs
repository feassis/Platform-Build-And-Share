using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] protected RectTransform scrollViewContent;
    [SerializeField] protected LevelSaveOption levelSaveOption;
    [SerializeField] protected Button mainMenuButton;
    [SerializeField] protected AddSaveMenu addSaveMenu;

    protected List<LevelSaveOption> levelSaveOptions = new List<LevelSaveOption>();

    protected virtual void Awake()
    {
        List<string> saves = GetAllSaves();

        SetupLevelSaveOptions(saves);

        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        addSaveMenu.OnSaveAdded += AddSaveMenu_OnSaveAdded;
    }

    private void AddSaveMenu_OnSaveAdded()
    {
        SetupLevelSaveOptions(GetAllSaves());
    }

    protected virtual void SaveOption_onSelectButtonClicked(string saveName)
    {
        Debug.Log($"Level {saveName} selected");
    }


    protected virtual void SaveOption_onPlayButtonClicked(string saveName)
    {
        PlayLevelManager.Open(false, saveName);
    }

    protected List<string> GetAllSaves()
    {
        string path = Application.dataPath + $"/{GameConstants.SAVE_FOLDER_PATH}";
        List<string> saves = new List<string>();

        if (!Directory.Exists(path))
        {
            Debug.LogWarning($"Pasta de saves não existe: {path}");
            return saves;
        }

        // gets only .json files
        string[] files = Directory.GetFiles(path, "*.json");

        foreach (string file in files)
        {
            // removes ".json"
            string fileName = Path.GetFileNameWithoutExtension(file);
            saves.Add(fileName);
        }

        return saves;
    }


    protected virtual void SetupLevelSaveOptions(List<string> saves)
    {
        foreach (var lsoption in levelSaveOptions)
        {
            Destroy(lsoption.gameObject);
        }

        foreach (var save in saves)
        {
            var saveOption = Instantiate<LevelSaveOption>(levelSaveOption, scrollViewContent);

            saveOption.Init(save);
            saveOption.onPlayButtonClicked += SaveOption_onPlayButtonClicked;
            saveOption.onSelectButtonClicked += SaveOption_onSelectButtonClicked;
            saveOption.onShareButtonClicked += SaveOption_onShareButtonClicked;

            levelSaveOptions.Add(saveOption);
        }
    }

    protected bool TryGetSaveJson(string saveName, out string json)
    {
        json = null;

        string path = Application.dataPath +
                      $"/{GameConstants.SAVE_FOLDER_PATH}/{saveName}.json";

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Save not found: {path}");
            return false;
        }

        json = File.ReadAllText(path);
        return true;
    }


    protected void SaveOption_onShareButtonClicked(string sharedLevel)
    {
        if(TryGetSaveJson(sharedLevel, out string saveJson))
        {
            var encodedJson = JsonEncoder.Encode(saveJson);
            GUIUtility.systemCopyBuffer = encodedJson;
        }
        else
        {
            Debug.LogError("Save Not Found");
        }
    }
}