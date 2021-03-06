using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    [SerializeField]
    private GameData _gameData;
    private void Start() => LoadGame();
    private void OnDisable() => SaveGame();

    private void SaveGame()
    {
        var json = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString("GameData", json);
        Debug.Log(json);
        Debug.Log("Completed");
    }

    private void LoadGame()
    {
        var json = PlayerPrefs.GetString("GameData");
        _gameData = JsonUtility.FromJson<GameData>(json);
        
        if (_gameData == null)
            _gameData = new GameData();

        FlagManager.Instance.Bind(_gameData.GameFlagDatas);
        InspectionManager.Bind(_gameData.InspectableDatas);
        Inventory.Instance.Bind(_gameData.SlotDatas);
    }
}