using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreatorManager : LevelManager
{
    private const string LEVEL_EDITOR_NAME = "MapCreator";

    [SerializeField] private PlayButtonMode playButtonMode;
    

    public static void Open(bool newMap, string fileName = null)
    {
        SceneManager.LoadScene(LEVEL_EDITOR_NAME);

        isNewMap = newMap;
        levelName = fileName;
    }

    protected override void InitilizaServices()
    {
        base.InitilizaServices();
        playButtonMode.Init(this);
    }
}
