using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    public event Action Changed;
    
    [SerializeField] private string _displayName;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _description;
    
    [Tooltip("Designer/programmer notes, not visible to player.")]
    [SerializeField] private string _notes;
    
    private int _currentStepIndex;
    
    public List<Step> Steps;
    public string Description => _description;
    public string DisplayName => _displayName;
    public Sprite Sprite => _sprite;
    public Step CurrentStep => Steps[_currentStepIndex];

    private void OnEnable()
    {
        foreach (var step in Steps)
        {
            foreach (var objective in step.Objectives)
            {
                if (objective.GameFlag != null)
                    objective.GameFlag.Changed += HandleFlagChanged;
            }
        }
        _currentStepIndex = 0;
    }

    private void HandleFlagChanged()
    {
        TryProgress();
        Changed?.Invoke();
    }

    public void TryProgress()
    {
        var currentStep = GetCurrentStep();
        if (currentStep.HasAllObjectivesCompleted())
        {
            _currentStepIndex++;
            Changed?.Invoke();
            // do whatever we do when a quest progresses like update the ui
        }
    }
    
    private Step GetCurrentStep() => Steps[_currentStepIndex];
}

[Serializable]
public class Step
{
    [SerializeField] private string _instructions;
    public List<Objective> Objectives;
    public string Instructions => _instructions;

    public bool HasAllObjectivesCompleted()
    {
        return Objectives.TrueForAll(t => t.IsCompleted);
    }
}