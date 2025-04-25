using UnityEngine;

public class UIObjectMover : MonoBehaviour
{
    public Vector3 moveDirection = new Vector3(1, 0, 0); // 移动方向 (默认向右)
    public float moveSpeed = 100f; // 每秒移动的像素量

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += (Vector2)(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
}
