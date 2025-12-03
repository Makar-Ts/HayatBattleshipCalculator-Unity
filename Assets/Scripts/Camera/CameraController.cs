using System;
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

        transform.Translate(inp * moveSpeed * (camera.orthographicSize * scrollToMoveSpeed) * Time.deltaTime);


        targetZoom = Mathf.Clamp(targetZoom + Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, minZoom, maxZoom);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, scrollSmoothness);
    }
}
