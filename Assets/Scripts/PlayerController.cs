using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DeerController _deer;
    [SerializeField] private SkyController _sky;
    [SerializeField] private GameObject _skyPatchPrefab;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _deerLayerMask;

    private float _sphereCastRadius;
    private bool _hasFoundAnimal;

    private void Start()
    {
        var skyPatch = _skyPatchPrefab.transform.Find("SkyPatch");
        _sphereCastRadius = skyPatch.GetComponent<CapsuleCollider>().radius;
    }

    private void Update()
    {
        if (_sky.skyPatches.Count <= 0) return;
        
        SeekTheAnimal(_sky.skyPatches[_sky.skyPatches.Count - 1]);
    }

    private void SeekTheAnimal(GameObject skyPatch)
    {
        Vector3 rayOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Vector3 rayDirection = (skyPatch.transform.position - rayOrigin).normalized;
        Ray ray = new Ray(rayOrigin, rayDirection);
        
        if (Physics.SphereCast(ray, _sphereCastRadius, out RaycastHit hitInfo, Mathf.Infinity, _deerLayerMask))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out DeerController deer))
            {
                deer.BeenFound();
            }
           
        }
    }
}
