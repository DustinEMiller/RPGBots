using System;
using UnityEngine;

public class MetInspectedCondition : MonoBehaviour, IMet
{
    [SerializeField] private Inspectable _requiredInspectable;
    
    public bool Met()
    {
        return _requiredInspectable.WasFullyInspected;
    }

    private void OnDrawGizmos()
    {
        if (_requiredInspectable != null)
        {
            Gizmos.color = Met() ? Color.yellow : Color.red;
            Gizmos.DrawLine(transform.position, _requiredInspectable.transform.position); 
        }
        
    }
}