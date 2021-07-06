using UnityEngine;

[CreateAssetMenu(menuName = "GameFlag/Bool game flag")]
public class BoolGameFlag : GameFlag<bool>
{
    protected override void SetFromData(string value)
    {
        if (bool.TryParse(value, out var boolValue))
            Set(boolValue);
    }
}