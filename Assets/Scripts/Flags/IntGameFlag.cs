using UnityEngine;

[CreateAssetMenu(menuName = "GameFlag/Counted Int game flag")]
public class IntGameFlag : GameFlag<int>
{
    public void Modify(int value) => Set(value + Value);

    protected override void SetFromData(string value)
    {
        if (int.TryParse(value, out var intValue))
            Set(intValue);
    }
}