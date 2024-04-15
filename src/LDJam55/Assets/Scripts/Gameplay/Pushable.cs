using System;
using System.Linq;
using UnityEngine;
using FMODUnity;

public class Pushable : OnMessage<PushingObjectBegin, PushingObjectEnd>
{
    [SerializeField] private float moveDurationSeconds = 1.5f;
    [SerializeField] private float requiredContactSeconds = 0.3f;
    [SerializeField] private float y;

    private bool _canMove = true;
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
        _start = transform.position;
        _destination = new Vector3((float)(Math.Round(transform.position.x / 2f) * 2f), y, (float)(Math.Round(transform.position.z / 2f) * 2f));
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
                transform.position = _destination;
                _isPlayerAdjacent = false;
                Message.Publish(new PushingObjectEnd());
            }
            else
            {
                transform.position = Vector3.Lerp(_start, _destination, _movingT / moveDurationSeconds);
            }
        }
        else if (_canMove && _canMoveThatDirection)
        {
            if (_isPlayerAdjacent)
                _contactT += Time.deltaTime;
            if (_contactT >= requiredContactSeconds)
            {
                _start = transform.position;
                _destination = new Vector3((float)(Math.Round((transform.position.x + _pushDirection.x * 2f) / 2f) * 2f), y, (float)(Math.Round((transform.position.z + _pushDirection.z * 2f) / 2f) * 2f));
                _movingT = 0;
                _contactT = 0; 
                _isMoving = true;
                
                Message.Publish(new PushingObjectBegin(this));
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
            
            _canMoveThatDirection = !Physics.OverlapSphere(transform.position + _pushDirection * 2 / transform.localScale.x, 0.1f).Any(x => !x.isTrigger);
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

    protected override void Execute(PushingObjectBegin msg)
    {
        if (msg.BeingPushed != this)
        {
            _canMove = false;
        }
    }

    protected override void Execute(PushingObjectEnd msg)
    {
        _canMove = true;
    }
}
