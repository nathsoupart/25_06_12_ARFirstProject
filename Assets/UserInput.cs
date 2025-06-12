

   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TestUserInput : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _arRaycastManager;
    [SerializeField] private FurnitureDataSO _furnitureData;

    private Camera _camera;
    private List<GameObject> _spawnObjects = new List<GameObject>();
    [SerializeField] List<GameObject> objects = new List<GameObject>();
    private int objets;
    private Vector3 touchPosition;
    [SerializeField] private LayerMask _layerMask;

    void Start()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        //gestion input avec souris
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            SpawnObjects(ray);
        }

        //gestion input par gsm
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                var touchPosition = touch.screenPosition;
                Ray ray = _camera.ScreenPointToRay(touchPosition);
                SpawnObjects(ray);
            }
        }
    }

    private void SpawnObjects(Ray ray) // fonction pour toucher avec le rayon camera qqchose
    {
        if (CheckIfClickOnObject(ray, out var selectedObject))
        {
            Destroy(selectedObject);
        }
        else
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (_arRaycastManager.Raycast(ray, hits))
            {
                ARRaycastHit firstHit = hits[0];
                Vector3 spawnPosition = firstHit.pose.position;

                GameObject newObject = Instantiate (_furnitureData.m_prefab, spawnPosition, Quaternion.identity);
                    
                    
                    
            }    
        }
        
    }

    // private bool TryGetTouchedObject(Vector2 touchPosition, out GameObject touchedObject)
    // {
    //
    //     Ray ray = _camera.ScreenPointToRay(touchPosition);
    //     RaycastHit hit;
    //
    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         touchedObject = hit.collider.gameObject;
    //         return _spawnObjects.Contains(touchedObject);
    //     }
    //
    //     touchedObject = null;
    //     return false;
    // } 
    // deuxième façon de faire Checkifclickonobject()

    private bool CheckIfClickOnObject(Ray ray, out GameObject selectedObject)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _layerMask))
        {
            selectedObject = hit.collider.gameObject;
            return true;
        }

        selectedObject = null;
        return false;
    }
    
}
