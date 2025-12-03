using System;
using System.Security.Cryptography;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    public float moveSpeed = 1;

    public float scrollSpeed = 1;
    public float scrollSmoothness = 0.03f;
    [SerializeField]
    private float scrollToMoveSpeed = 1;


    [SerializeField]
    private float maxZoom = 40, minZoom = 2;
    private float targetZoom;
    private Camera camera;


    private Boolean isLocked = false;


    void Start() {
        camera = GetComponent<Camera>();
        targetZoom = camera.orthographicSize;
    }


    void Update() {
        if (Input.GetMouseButtonDown(2)) {
            isLocked = true;
            Cursor.lockState = CursorLockMode.Locked;
        } else if (Input.GetMouseButtonUp(2)) {
            isLocked = false;
            Cursor.lockState = CursorLockMode.None;
        }

        Vector2 inp = new Vector2(
            Input.GetAxis("Horizontal") + (isLocked ? Input.GetAxis("Mouse X") : 0), 
            Input.GetAxis("Vertical")   + (isLocked ? Input.GetAxis("Mouse Y") : 0)
        );

        Vector3 movement = camera.orthographicSize * scrollToMoveSpeed * moveSpeed * Time.deltaTime * inp;

        transform.Translate(movement);


        targetZoom = Mathf.Clamp(targetZoom + Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, minZoom, maxZoom);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, scrollSmoothness);


        if (targetPosition != null)
        {
            if (movement.magnitude > 0)
            {
                targetPosition = null;
                return;
            }

            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(
                    targetPosition?.x ?? 0, 
                    targetPosition?.y ?? 0,
                    transform.position.z
                ),
                0.05f
            );
        } 
    }


    public Vector3? targetPosition
    {
        get { return _targetPosition ?? _target?.position ?? null; }
        set
        {
            if (value == null)
            {
                _targetPosition = null;
                _target = null;
            }
            else
            {
                _targetPosition = value;
            }
        }
    }

    private Vector3? _targetPosition = null;
    private Transform? _target = null;
    public void Relocate(Vector3 pos)
    {
        _targetPosition = pos;
    }

    public void Relocate(Transform target)
    {
        _target = target;
    }
}
