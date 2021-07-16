using UnityEngine;
using Random = UnityEngine.Random;

public class FlySwatFly : MonoBehaviour
{

    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] float speed = 20.0f;
    [SerializeField] float rotateSpeed = 15.0f;
    
    bool _dead;
    static Bounds _bounds;
    Vector3 newPosition;
    void Start ()
    {
        _dead = false;
        PositionChange();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Die()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.simulated = true;
        _rigidbody.gravityScale = 2;
        _dead = true;
    }

    void PositionChange()
    {
        newPosition = new Vector2(
            Random.Range(_bounds.size.x / 2 - _bounds.size.x, _bounds.size.x / 2), 
            Random.Range(_bounds.size.y / 2 - _bounds.size.y, _bounds.size.y / 2)
            );

    }
   
    void Update ()
    {
        if (!_dead)
        {
            if(Vector2.Distance(transform.localPosition, newPosition) < 1)
                PositionChange();
 
            transform.localPosition = Vector3.Lerp(transform.localPosition,newPosition,Time.deltaTime/speed);
        }

        LookAt2D(newPosition);
    }
 
    void LookAt2D(Vector3 lookAtPosition)
    {
        float distanceX = lookAtPosition.x - transform.localPosition.x;
        float distanceY = lookAtPosition.y - transform.localPosition.y;
        float angle = Mathf.Atan2(distanceX, distanceY) * Mathf.Rad2Deg;
       
        Quaternion endRotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * rotateSpeed);
    }

    public static void SetBounds(Bounds bounds)
    {
        _bounds = bounds;
    }
}
