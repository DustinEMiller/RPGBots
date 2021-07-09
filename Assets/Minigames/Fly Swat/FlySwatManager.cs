using System.Collections.Generic;
using UnityEngine;

public class FlySwatManager : MonoBehaviour, IRestart
{
    [SerializeField] Sprite[] _brokenPanes;
    [SerializeField] int _clicksToLose = 20;
    [SerializeField] int _maxFlies = 10;
    [SerializeField] GameObject _glassCrack;
    [SerializeField] GameObject _fly;
    [SerializeField] Camera mainCamera;

    int currentClicks = 0;
    int _currentFlies = 0;
    int _fliesKilled = 0;
    Bounds _bounds;
    float _timer = 0f;
    float _waitingTime = 0f;

    private List<GameObject> _objectsToRemove = new List<GameObject>();
    void Awake()
    {
        _waitingTime = Random.Range(1f, 1.35f);
        
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = (mainCamera.orthographicSize * 2);

        _bounds = new Bounds(
            mainCamera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        Debug.Log(cameraHeight * screenAspect);
        FlySwatFly.SetBounds(_bounds);
    }

    void Start() => SpawnFly();

    void Update()
    {
        if(_fliesKilled == _maxFlies)
        {
            GetComponentInParent<FlySwatMinigame>().Win();
        }
        
        if(currentClicks == _clicksToLose)
        {
            GetComponentInParent<FlySwatMinigame>().Lose();
        }
        
        _timer += Time.deltaTime;
        
        if (_timer > _waitingTime && _currentFlies < _maxFlies)
        {
            SpawnFly();
            _timer = 0f;
            _waitingTime = Random.Range(2.0f, 4.0f);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 screenPosDepth = Input.mousePosition;
            HitFly(screenPosDepth);
            CreateCrack(screenPosDepth);
        }
    }

    void HitFly(Vector3 screenPosDepth)
    {
        Vector2 rayPos = new Vector2(mainCamera.ScreenToWorldPoint(screenPosDepth).x, mainCamera.ScreenToWorldPoint(screenPosDepth).y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        if (hit)
        {
            var fly = hit.collider.GetComponent<FlySwatFly>();
            _fliesKilled++;
            fly.Die();
        }
    }

    void CreateCrack(Vector3 screenPosDepth)
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(screenPosDepth);
        mousePosition.z = 0;
        
        int randomCrack = Random.Range(0, _brokenPanes.Length);
        float scaleNumber = Random.Range(1.0f, 2.2f);
 
        GameObject newCrack = Instantiate(_glassCrack, mousePosition, Quaternion.identity);
        newCrack.transform.SetParent(gameObject.transform, true);

        newCrack.GetComponent<SpriteRenderer>().sprite = _brokenPanes[randomCrack];
        newCrack.transform.localScale = new Vector3(scaleNumber, scaleNumber, 1);
        newCrack.SetActive(true);
        _objectsToRemove.Add(newCrack);
        
        currentClicks++;
    }

    void SpawnFly()
    {
        var randomx = Random.Range(_bounds.size.x / 2 - _bounds.size.x, _bounds.size.x / 2);
        var randomy = Random.Range(_bounds.size.y / 2 - _bounds.size.y, _bounds.size.y / 2);
        Vector3 position = new Vector3(randomx, randomy, 0);

        GameObject newFly = Instantiate(_fly, position, Quaternion.identity);
        newFly.transform.SetParent(gameObject.transform, false);
        newFly.SetActive(true);
        _objectsToRemove.Add(newFly);
        
        _currentFlies++;
    }

    public void Restart()
    {
        currentClicks = 0;
        _currentFlies = 0;
        _fliesKilled = 0;
        _timer = 0f; 
        _waitingTime = 0f;
        
        foreach(GameObject go in _objectsToRemove)
        {
            Destroy(go);
        }

        _objectsToRemove = new List<GameObject>();
    }

}