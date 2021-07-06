using UnityEngine;

public class MetBoolFlagCondition : MonoBehaviour, IMet
{
    [SerializeField] private BoolGameFlag _requiredFlag;
    
    public bool Met()
    {
        return _requiredFlag.Value;
    }
}