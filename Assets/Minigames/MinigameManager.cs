using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    private Action _completeInspection;
    public static MinigameManager Instance { get; private set; }

    private void Awake() => Instance = this;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _completeInspection?.Invoke();
            _completeInspection = null;
        }
            
    }

    public void StartMinigame(MingameSettings settings, Action<MinigameResult> completeInspection)
    {
        if(settings is FlippyBoxMingameSettings flippyBoxMingameSettings) 
            FlippyBoxMinigame.Instance.StartMinigame(flippyBoxMingameSettings,completeInspection);
        else if(settings is WinLoseMinigameSettings winLoseMingameSettings) 
            WinLoseMinigamePanel.Instance.StartMinigame(winLoseMingameSettings, completeInspection);
    }
}