using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Minigame/Fly Swat Settings", fileName = "FlySwatSettings- ")]
public class FlySwatMinigameSettings : MingameSettings
{
    public float FlySpeed = 1.5f;
    public int Flies = 10;
    public int ClicksToLose = 20;
}
