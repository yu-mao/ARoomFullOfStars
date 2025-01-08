using System;
using System.Collections.Generic;
using UnityEngine;

public class SkySpawner : MonoBehaviour
{
    public List<GameObject> skyPatches = new List<GameObject>();
    
    [SerializeField] private GameObject skyPrefab;
    [SerializeField] private GameObject pointerVisual;
    [SerializeField] private float skyPlacementElevation = 0.01f;
    [SerializeField] private AudioController audioController;
    
    private bool isInitialized = false;
    private int wallLayerMask;
    // private AudioSource audioSource;

    public void Initialize()
    {
        isInitialized = true;
        wallLayerMask = LayerMask.GetMask("Wall");
        pointerVisual = Instantiate(pointerVisual);
        // audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isInitialized) return;

        Vector3 rayOrigin = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Vector3 rayDirection = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
        Ray ray = new Ray(rayOrigin, rayDirection);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, wallLayerMask))
        {
            pointerVisual.transform.position = hit.point;

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Quaternion rotation = Quaternion.LookRotation(-hit.normal);
                Vector3 placementPosition = hit.point + hit.normal * skyPlacementElevation;
                
                var skyPatch = Instantiate(skyPrefab, placementPosition, rotation);
                skyPatches.Add(skyPatch);
                // audioSource.Play();
            }
        }
    }
}
