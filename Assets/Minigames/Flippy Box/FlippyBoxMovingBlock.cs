using UnityEngine;

public class FlippyBoxMovingBlock : MonoBehaviour, IRestart
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float MoveSpeed => FlippyBoxMinigame.Instance.CurrentSettings.MovingBlockSpeed;
    private Vector3 _startingPosition;

    private void Awake()
    {
        _startingPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 movement = Vector2.left * MoveSpeed * Time.deltaTime;
        _rigidbody.position += movement;
        if (transform.localPosition.x < -15f)
            _rigidbody.position += new Vector2(30f, 0);
    }
    
    public void Restart()
    {
        transform.position = _startingPosition;
    }
}