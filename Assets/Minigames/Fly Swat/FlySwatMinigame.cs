using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySwatMinigame : MonoBehaviour
{
    [SerializeField] private FlySwatMinigameSettings _defaultSettings;
    public FlySwatMinigameSettings CurrentSettings { get; private set; }
    public static FlySwatMinigame Instance { get; private set; }
    
    private Action<MinigameResult> _completeInspection;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
        
        if(transform.parent == null) {
            StartMinigame(_defaultSettings, (result) => Debug.Log(result));
        }
    }
    
    public void StartMinigame(FlySwatMinigameSettings settings, Action<MinigameResult> completeInspection)
    {
        CurrentSettings = settings ?? _defaultSettings;
        gameObject.SetActive(true);
        foreach (var restartable in GetComponentsInChildren<IRestart>())
        {
            restartable.Restart();
        }
        _completeInspection = completeInspection;
    }
    
    public void Win()
    {
        _completeInspection?.Invoke(MinigameResult.Won);
        _completeInspection = null;
        gameObject.SetActive(false);
        Debug.Log("won");
    }

    public void Lose()
    {
        Debug.Log("Lose");
        _completeInspection?.Invoke(MinigameResult.Lost);
        _completeInspection = null;
        gameObject.SetActive(false);
    }
}
