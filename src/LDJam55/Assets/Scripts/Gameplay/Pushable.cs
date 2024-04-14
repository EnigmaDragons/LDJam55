using UnityEngine;
using DG.Tweening;

public class Pushable : MonoBehaviour
{
    [SerializeField] private float moveDurationSeconds = 1.5f;

    private bool isPlayerAdjacent = false;
    private float playerContactTime = 0f;
    private const float requiredContactTime = 0.3f;
    private Vector3 pushDirection;

    private bool _isMoving = false;

    private void Update()
    {
        if (_isMoving) return;

        if (isPlayerAdjacent && playerContactTime >= requiredContactTime)
        {
            MoveObject();
            ResetPushMechanism();
        }
        else if (isPlayerAdjacent)
        {
            playerContactTime += Time.deltaTime;
        }

        // NOTE: Hacky fix to ensure the block always snaps to the tile grid.
        if (!_isMoving)
        {
            float snappedX = Mathf.Round(transform.localPosition.x / 2) * 2;
            float snappedZ = Mathf.Round(transform.localPosition.z / 2) * 2;
            if (transform.localPosition.x != snappedX || transform.localPosition.z != snappedZ)
            {
                transform.localPosition = new Vector3(snappedX, transform.localPosition.y, snappedZ);
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
            isPlayerAdjacent = true;
            Log.Info("Player Adjacent", this);
            Vector3 direction = (transform.position - o.transform.position).normalized;
            var isMoreX = Mathf.Abs(direction.x) > Mathf.Abs(direction.z);
            pushDirection = isMoreX 
              ? new Vector3(Mathf.Round(direction.x), 0, 0) 
              : new Vector3(0, 0, Mathf.Round(direction.z));
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

    private void MoveObject()
    {
        _isMoving = true;
        transform.DOMove(transform.position + pushDirection * 2, moveDurationSeconds);
        this.ExecuteAfterDelay(() =>
        {
            _isMoving = false;
            playerContactTime = 0f;
        }, moveDurationSeconds);
    }

    private void ResetPushMechanism()
    {
      isPlayerAdjacent = false;
      playerContactTime = 0f;
    }
}
