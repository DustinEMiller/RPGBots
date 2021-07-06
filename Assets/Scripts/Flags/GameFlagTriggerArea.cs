using UnityEngine;
using UnityEngine.Serialization;

public class GameFlagTriggerArea : MonoBehaviour
{
    [FormerlySerializedAs("_gameFlag")] [SerializeField] private BoolGameFlag _boolGameFlag;

    private void OnTriggerEnter(Collider other)
    {
        _boolGameFlag.Set(true);
    }
}