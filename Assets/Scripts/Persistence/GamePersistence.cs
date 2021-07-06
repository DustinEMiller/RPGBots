using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    [SerializeField]
    private GameData _gameData;
    private void Start() => LoadGameFlags();
    private void OnDisable() => SaveGameFlags();

    private void SaveGameFlags()
    {
        var json = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString("GameData", json);
        Debug.Log(json);
        Debug.Log("Completed");
    }

    private void LoadGameFlags()
    {
        var json = PlayerPrefs.GetString("GameData");
        _gameData = JsonUtility.FromJson<GameData>(json);
        
        if (_gameData == null)
            _gameData = new GameData();

        FlagManager.Instance.Bind(_gameData.GameFlagDatas);
        InspectionManager.Bind(_gameData.InspectableDatas);
    }
}