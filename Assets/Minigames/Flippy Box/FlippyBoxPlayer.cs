using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FlippyBoxPlayer : MonoBehaviour, IRestart
{
    Vector2 JumpVelocity => FlippyBoxMinigame.Instance.CurrentSettings.JumpVelocity;
    float GrowTime => FlippyBoxMinigame.Instance.CurrentSettings.GrowTime;
    [SerializeField] private float _spinSpeed = 50f;
    
    private Rigidbody2D _rigidBody;
    private Vector3 _startingPosition;
    private Quaternion _startingRotation;
    private float _elapsed;
    

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _startingPosition = transform.position;
        _startingRotation = transform.rotation;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            _rigidBody.velocity = JumpVelocity;
        
        transform.Rotate(0f, 0f, Time.deltaTime * _spinSpeed);

        _elapsed += Time.deltaTime;
        float size = Mathf.Lerp(1f, 2f, _elapsed / GrowTime);
        transform.localScale = new Vector3(size, size, 1f);
    }

    public void Restart()
    {
        transform.position = _startingPosition;
        transform.rotation = _startingRotation;
        _elapsed = 0f;
        transform.localScale = Vector3.one;
        GetComponent<SpriteRenderer>().color = FlippyBoxMinigame.Instance.CurrentSettings.PlayerColor;
    }
}
