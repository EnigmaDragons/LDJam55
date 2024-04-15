using UnityEngine;

public class TimedRoom : MonoBehaviour
{
    [SerializeField] private float seconds;

    private bool _turnedOff;
    private bool _started;
    private float _t;

    private void Start()
    {
        _turnedOff = false;
        _started = false;
        _t = 0;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _started = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _turnedOff = true;
    }
    
    private void Update()
    {
        if (!_started || _turnedOff)
            return;
        _t += Time.deltaTime;
        if (_t >= seconds)
        {
            _turnedOff = true;
            Message.Publish(new PlayQuip());
        }
    }
}