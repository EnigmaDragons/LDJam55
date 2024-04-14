using System;
using System.Linq;
using UnityEngine;
using FMODUnity;

public class Pushable : MonoBehaviour
{
    [SerializeField] private float moveDurationSeconds = 1.5f;
    [SerializeField] private float requiredContactSeconds = 0.3f;

    private bool _isMoving;
    private Vector3 _start;
    private Vector3 _destination;
    private Vector3 _pushDirection;
    private bool _canMoveThatDirection;
    private float _movingT;
    private float _contactT; 
    private bool _isPlayerAdjacent;
    public EventReference pushBlockSFX;

    private void Start()
    {
        _isMoving = true;
        _start = transform.localPosition;
        _destination = new Vector3((float)(Math.Round((transform.localPosition.x) / 2f) * 2f), 0, (float)(Math.Round(transform.localPosition.z / 2f) * 2f));
        _movingT = 0;
        _contactT = 0;
        _isPlayerAdjacent = false;
    }
    
    private void Update()
    {
        if (_isMoving)
        {
            _movingT += Time.deltaTime;
            if (_movingT >= moveDurationSeconds)
            {
                _movingT = 0;
                _contactT = 0;
                _isMoving = false;
                transform.localPosition = _destination;
                _isPlayerAdjacent = false;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(_start, _destination, _movingT / moveDurationSeconds);
            }
        }
        else if (_canMoveThatDirection)
        {
            if (_isPlayerAdjacent)
                _contactT += Time.deltaTime;
            if (_contactT >= requiredContactSeconds)
            {
                _start = transform.localPosition;
                _destination = new Vector3((float)(Math.Round((transform.localPosition.x + _pushDirection.x * 2f) / 2f)  * 2f), 0, (float)(Math.Round((transform.localPosition.z + _pushDirection.z * 2f) / 2f) * 2f));
                _movingT = 0;
                _contactT = 0;
                _isMoving = true;
                RuntimeManager.PlayOneShotAttached(pushBlockSFX, gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision c) => ObjEnter(c.gameObject);
    private void OnTriggerEnter(Collider c) => ObjEnter(c.gameObject);
    private void OnCollisionExit(Collision c) => ObjExit(c.gameObject);
    private void OnTriggerExit(Collider c) => ObjExit(c.gameObject);
    
    private void ObjEnter(GameObject o)
    {
        if (o.CompareTag("Player"))
        {
            _isPlayerAdjacent = true;
            Log.Info("Player Adjacent", this);
            Vector3 direction = (transform.position - o.transform.position).normalized;
            var isMoreX = Mathf.Abs(direction.x) > Mathf.Abs(direction.z);
            _pushDirection = isMoreX 
              ? new Vector3(Mathf.Round(direction.x), 0, 0) 
              : new Vector3(0, 0, Mathf.Round(direction.z));
            _canMoveThatDirection = !Physics.OverlapSphere( transform.TransformPoint(_pushDirection + new Vector3(0, 0.5f, 0)), 0.1f).Any(x => !x.isTrigger);
        }
    }

    private void ObjExit(GameObject o)
    {
        if (o.CompareTag("Player"))
        {
            Log.Info("Player Not Adjacent", this);
            ResetPushMechanism();
        }
    }
    
    private void ResetPushMechanism()
    {
      _isPlayerAdjacent = false;
      _contactT = 0f;
    }
}
