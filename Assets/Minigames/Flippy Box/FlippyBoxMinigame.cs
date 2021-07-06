using System;
using UnityEngine;

public class FlippyBoxMinigame : MonoBehaviour
{
    [SerializeField] private FlippyBoxMingameSettings _defaultSettings;
    private Action<MinigameResult> _completeInspection;
    public static FlippyBoxMinigame Instance { get; private set; }
    public FlippyBoxMingameSettings CurrentSettings { get; private set; }

    void Awake() => Instance = this;
    void Start()
    {
        gameObject.SetActive(false);
        if(transform.parent == null) {
            StartMinigame(_defaultSettings, (result) => Debug.Log(result));
        }
    }

    public void StartMinigame(FlippyBoxMingameSettings settings, Action<MinigameResult> completeInspection)
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
    }

    public void Lose()
    {
        _completeInspection?.Invoke(MinigameResult.Lost);
        _completeInspection = null;
        gameObject.SetActive(false);
    }
    
}