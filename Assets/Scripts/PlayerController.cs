using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SkyController _sky;
    [SerializeField] private GameObject _skyPatchPrefab;
    [SerializeField] private LayerMask _deerLayerMask;

    private float _sphereCastRadius;
    private DeerController _deer;
    private Vector3 _playerPosWhenDiscoveredDeer;
    private AudioSource _audioSource;

    private void Start()
    {
        var skyPatch = _skyPatchPrefab.transform.Find("SkyPatch");
        _sphereCastRadius = skyPatch.GetComponent<CapsuleCollider>().radius;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
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
                _deer = deer;
                _playerPosWhenDiscoveredDeer = rayOrigin;
                
                _deer.BeenFound();
                StartCoroutine(PlayerWin());
            }
        }
    }

    private IEnumerator PlayerWin()
    {
        yield return _deer.transform.DOLookAt(_playerPosWhenDiscoveredDeer, 1f)
            .SetEase(Ease.InOutQuad).WaitForCompletion();
        yield return new WaitForSeconds(3f);
        
        yield return SceneManager.LoadSceneAsync("Win");
    }
}
