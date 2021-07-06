using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Objective
{
    [SerializeField] private ObjectiveType _objectiveType;
    [SerializeField] private GameFlag _gameFlag;

    [Tooltip("Required amount for for the counted integer game flag.")]
    [SerializeField] private int _required;

    public GameFlag GameFlag => _gameFlag;

    public enum ObjectiveType
    {
        GameFlag,
        Item,
        Kill
    }

    public bool IsCompleted
    {
        get
        {
            switch (_objectiveType)
            {
                case ObjectiveType.GameFlag:
                {
                    if(_gameFlag is BoolGameFlag boolGameFlag)
                        return boolGameFlag.Value;
                    if (_gameFlag is IntGameFlag intGameFlag)
                        return intGameFlag.Value >= _required;
                    return false;
                }
                default: return false;
            }
        }
    }

    public override string ToString()
    {
        switch (_objectiveType)
        {
            case ObjectiveType.GameFlag:
            {
                if(_gameFlag is BoolGameFlag)
                    return _gameFlag.name;
                if (_gameFlag is IntGameFlag intGameFlag)
                    return $"{intGameFlag.name} ({intGameFlag.Value}/{_required})";
                return "Invalid Objective Type";
            }
            default: return _objectiveType.ToString();
        }
    }
}