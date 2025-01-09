using UnityEditor;
using UnityEngine;

public class DeerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _directionChangeTimeInterval = 2f;
    [SerializeField] private Animator _animator;

    private Vector3 _direction;
    private float _timer = 0f;
    private bool _beenFound = false;

    public void BeenFound()
    {
        _beenFound = true;
        _animator.SetBool("found", true);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetARandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (_beenFound) return;
        
        transform.position += _direction * (_speed * Time.deltaTime);
        
        _timer += Time.deltaTime;
        if (_timer >= _directionChangeTimeInterval)
        {
            SetARandomDirection();
            _timer = 0;
        }
    }

    private void SetARandomDirection()
    {
        _direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f)).normalized;
        if (_direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_direction);
        }
    }
}
