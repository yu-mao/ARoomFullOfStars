using System;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _windowPrefab;
    
    
    // [SerializeField] private TextMeshProUGUI debugText;

    // private int countCollision;

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(_windowPrefab, other.transform.position, Quaternion.identity);
    }
}
