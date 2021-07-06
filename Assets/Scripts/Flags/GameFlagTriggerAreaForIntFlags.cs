using UnityEngine;

public class GameFlagTriggerAreaForIntFlags : MonoBehaviour
{
    [SerializeField] private IntGameFlag _intGameFlag;
    [SerializeField] private int _amount;

    private void OnTriggerEnter(Collider other)
    {
        _intGameFlag.Modify(_amount);
    }
}