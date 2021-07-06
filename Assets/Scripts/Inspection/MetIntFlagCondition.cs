using UnityEngine;

public class MetIntFlagCondition : MonoBehaviour, IMet
{
    [SerializeField] private IntGameFlag _requiredFlag;
    [SerializeField] private int _requiredValue;
    public bool Met()
    {
        return _requiredFlag.Value >= _requiredValue;
    }
}