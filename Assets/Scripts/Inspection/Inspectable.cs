using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inspectable : MonoBehaviour
{
    [SerializeField] private float _timeToInspect = 3f;
    [SerializeField] private float _completedTextTime = 5f;
    [SerializeField] private UnityEvent OnInspectionCompleted;
    [SerializeField, TextArea] private string _completedInspectionText;
    [SerializeField] private bool _requireMinigame = false;
    [SerializeField] private MingameSettings _minigameSettings;
    
    private static HashSet<Inspectable> _inspectablesInRange = new HashSet<Inspectable>();
    
    public static IReadOnlyCollection<Inspectable> InspectablesInRange => _inspectablesInRange;
    public static event Action<bool> InspectablesInRangeChanged;
    public static event Action<Inspectable, string> OnAnyInspectionComplete;

    private InspectableData _data;
    private IMet[] _allConditions;
    

    public bool WasFullyInspected => InspectionProgress >= 1f;
    public float InspectionProgress => _data?.TimeInspected ?? 0f / _timeToInspect;

    private void Awake()
    {
        _allConditions = GetComponents<IMet>();
    }
    
    public void Bind(InspectableData inspectableData)
    {
        _data = inspectableData;
        if(WasFullyInspected)
            RestoreInspectionState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !WasFullyInspected && MeetsConditions())
        {
            if(_inspectablesInRange.Add(this))
                InspectablesInRangeChanged?.Invoke(true);
        }
    }

    public bool MeetsConditions()
    {
        foreach (var condition in _allConditions)
        {
            if (condition.Met() == false)
                return false;
        }

        return true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inspectablesInRange.Remove(this);
            InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        }
    }

    public void Inspect()
    {
        if(WasFullyInspected)
            return;
        
        _data.TimeInspected += Time.deltaTime;
        if (WasFullyInspected)
        {
            if (_requireMinigame)
            {
                _inspectablesInRange.Remove(this);
                InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
                MinigameManager.Instance.StartMinigame(_minigameSettings, HandleMinigameCompleted);
            }
            else
                CompleteInspection();
        }
    }

    private void HandleMinigameCompleted(MinigameResult result)
    {
        if (result == MinigameResult.Won)
            CompleteInspection();
        else if (result == MinigameResult.Lost)
        {
            _inspectablesInRange.Add(this);
            InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
            _data.TimeInspected = 0f;
        }
            
    }

    private void CompleteInspection()
    {
        _inspectablesInRange.Remove(this);
        InspectablesInRangeChanged?.Invoke(_inspectablesInRange.Any());
        OnInspectionCompleted?.Invoke();
        OnAnyInspectionComplete?.Invoke(this, _completedInspectionText);
    }

    void RestoreInspectionState()
    {
        OnInspectionCompleted?.Invoke();
    }
}