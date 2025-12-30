using System.Collections;
using UnityEngine.SceneManagement;

public class PlayLevelManager : LevelManager
{
    private const string LEVEL_PLAYER_NAME = "PlayLevel";

    public static void Open(bool newMap, string fileName = null)
    {
        SceneManager.LoadScene(LEVEL_PLAYER_NAME);

        isNewMap = newMap;
        levelName = fileName;
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }


    private IEnumerator StartGame()
    {
        yield return null;
        ToggleGameMode(true);
    }
}