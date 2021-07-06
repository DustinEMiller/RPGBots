using UnityEngine;

[CreateAssetMenu(menuName = "GameFlag/String game flag")]
public class StringGameFlag : GameFlag<string>
{
    protected override void SetFromData(string value)
    {
        Set(value);
    }
}