using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTIme : MonoBehaviour
{
    [SerializeField] private Vector3 _magnitude = Vector3.down;
    [SerializeField] private AnimationCurve _curve = 
        AnimationCurve.Linear(0f, 0f, 1f, 1f);

    [SerializeField] private float _duration = 1f;
    
    private Vector3 _startingPosition;
    private Vector3 _endingPosition;
    private float _elapsed;

    void Awake() => _startingPosition = transform.position;

    private void OnEnable()
    {
        _elapsed = 0f;
        _endingPosition = _startingPosition + _magnitude;
    }

    private void OnDisable() => transform.position = _startingPosition;

    void Update()
    {
        _elapsed += Time.deltaTime;
        float pctElapsed = _elapsed / _duration;
        float pctOnCurve = _curve.Evaluate(pctElapsed);
        transform.position = Vector3.Lerp(_startingPosition, _endingPosition, pctOnCurve);
    }
}
