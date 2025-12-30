using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadlLeveEditorMenu : LevelLoader
{  

    protected override void SetupLevelSaveOptions(List<string> saves)
    {
        foreach(var lsoption in levelSaveOptions)
        {
            Destroy(lsoption.gameObject);
        }

        foreach(var save in saves)
        {
            var saveOption = Instantiate<LevelSaveOption>(levelSaveOption, scrollViewContent);

            saveOption.Init(save);
            saveOption.onPlayButtonClicked += SaveOption_onPlayButtonClicked;
            saveOption.onDeleteButtonClicked += SaveOption_onDeleteButtonClicked;
            saveOption.onSelectButtonClicked += SaveOption_onSelectButtonClicked;

            levelSaveOptions.Add(saveOption);
        }
    }

    

    private void SaveOption_onDeleteButtonClicked(string saveName)
    {
        DeleteSave(saveName);

        SetupLevelSaveOptions(GetAllSaves());
    }

    protected override void SaveOption_onPlayButtonClicked(string saveName)
    {
        LevelCreatorManager.Open(false, saveName);
    }

    public bool DeleteSave(string saveName)
    {
        string path = Application.dataPath + $"/{GameConstants.SAVE_FOLDER_PATH}/{saveName}.json";

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Save not found: {path}");
            return false;
        }

        try
        {
            File.Delete(path);
            Debug.Log($"Save deleted: {saveName}");
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to delete save {saveName}: {e.Message}");
            return false;
        }
    }


    



}
