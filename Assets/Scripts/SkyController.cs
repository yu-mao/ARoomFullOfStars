using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    public List<GameObject> skyPatches = new List<GameObject>();
    
    [SerializeField] private GameObject _skyPrefab;
    [SerializeField] private GameObject _pointerVisual;
    [SerializeField] private float _skyPlacementElevation = 0.01f;
    [SerializeField] private AudioController _audio;

    private bool _isInitialized = false;
    private int _wallLayerMask;

    public void Initialize()
    {
        _isInitialized = true;
        _wallLayerMask = LayerMask.GetMask("Wall");
        _pointerVisual = Instantiate(_pointerVisual);
    }

    private void Update()
    {
        if (!_isInitialized) return;

        DetectSkySpawning();
    }

    private void DetectSkySpawning()
    {
        Vector3 rayOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Vector3 rayDirection = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
        Ray ray = new Ray(rayOrigin, rayDirection);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _wallLayerMask))
        {
            _pointerVisual.transform.position = hit.point;

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                SpawnSkyPatch(hit);
            }
        }
    }

    private void SpawnSkyPatch(RaycastHit hit)
    {
        Quaternion rotation = Quaternion.LookRotation(-hit.normal);
        Vector3 placementPosition = hit.point + hit.normal * _skyPlacementElevation;
                
        var skyPatch = Instantiate(_skyPrefab, placementPosition, rotation);
        skyPatches.Add(skyPatch);
                
        // // TODO: play SFX of sky spawning 
        // _audio.sfxChannel.clip = _audio.skySpawnSFX;
        // _audio.sfxChannel.Play();
    }
}
