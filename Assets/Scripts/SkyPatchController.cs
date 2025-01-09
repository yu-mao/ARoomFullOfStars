using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyPatchController : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    
    private MeshRenderer _meshRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(SkyPatchLifecycle());
    }

    private IEnumerator SkyPatchLifecycle()
    {
        // gently shake upon appearance
        yield return transform.DOShakePosition(0.3f, 0.1f, 15).WaitForCompletion();
        
        yield return new WaitForSeconds(lifeTime);

        yield return transform.DOScale(0f, 1f).SetEase(Ease.InOutQuad).WaitForCompletion();
        
        Destroy(gameObject);
    }
}
