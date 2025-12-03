using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class ResizeGrid : MonoBehaviour {
    private Camera pcam;
    private Vector3 ratio;
    private MeshRenderer render;


    [SerializeField] private Material[] sizes;
    [SerializeField] private float[] maxOrtho;


    public UnityEvent<string> onChange = new();


    void Start() {
        pcam = transform.parent.GetComponent<Camera>();
        render = GetComponent<MeshRenderer>();

        ratio = transform.localScale / pcam.orthographicSize;
    }


    void Update() {
        transform.localScale = ratio * pcam.orthographicSize;

        int index = 0;
        while (maxOrtho.Length > index && maxOrtho[index] < pcam.orthographicSize) index++;

        if (render.material != sizes[index]) {
            render.material = sizes[index];

            onChange.Invoke(sizes[index].name);
        }
    }
}
