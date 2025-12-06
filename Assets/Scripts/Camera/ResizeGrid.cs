using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct GridLevel
{
    public float maxOrtho;
    public float repeat;
    public float split;
    public Vector3 offset;
    public Color baseColor;
}

[RequireComponent(typeof(MeshRenderer))]
public class ResizeGrid : MonoBehaviour
{
    private Camera pcam;
    private Vector3 ratio;
    private MeshRenderer render;
    private MaterialPropertyBlock mpb;

    [SerializeField] private GridLevel[] levels;
    [SerializeField] private Material gridMaterial; // Ссылка на материал

    public UnityEvent<string> onChange = new();


    private int currentIndex = -1;

    private void Start()
    {
        pcam = transform.parent.GetComponent<Camera>();
        render = GetComponent<MeshRenderer>();
        
        // Создаем копию материала, чтобы не менять оригинал
        if (gridMaterial != null)
        {
            render.material = new Material(gridMaterial);
        }
        
        mpb = new MaterialPropertyBlock();
        ratio = transform.localScale / pcam.orthographicSize;
        
        // Применяем начальный уровень
        UpdateGridLevel();
    }

    private void LateUpdate()
    {
        if (pcam == null) return;
        
        transform.localScale = ratio * pcam.orthographicSize;
        
        int index = GetLevelIndex(pcam.orthographicSize);
        if (index != currentIndex)
        {
            currentIndex = index;
            UpdateGridLevel();
            var size = Math.Round(1 / (levels[currentIndex].repeat / 2));
            onChange.Invoke("Grid " + size + "x" + size);
        }
    }

    private void UpdateGridLevel()
    {
        if (currentIndex >= 0 && currentIndex < levels.Length)
        {
            ApplyLevel(levels[currentIndex]);
        }
    }

    private void ApplyLevel(GridLevel level)
    {
        if (render == null) return;
        
        // Используем прямой доступ к материалу
        Material mat = render.material;
        if (mat != null)
        {
            mat.SetFloat("_repeat", level.repeat);
            mat.SetFloat("_split", level.split);
            mat.SetVector("_offset", level.offset);
            mat.SetColor("_base", level.baseColor);
        }
    }

    private int GetLevelIndex(float ortho)
    {
        if (levels == null || levels.Length == 0) return -1;
        
        for (int i = 0; i < levels.Length; i++)
        {
            if (ortho <= levels[i].maxOrtho)
                return i;
        }

        return levels.Length - 1;
    }

    private void OnValidate()
    {
        // В редакторе обновляем материал при изменении
        if (Application.isPlaying) return;
        
        if (render != null && levels != null && levels.Length > 0)
        {
            ApplyLevel(levels[0]);
        }
    }
}