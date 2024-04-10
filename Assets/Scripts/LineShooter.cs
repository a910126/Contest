using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineShooter : MonoBehaviour
{
    private LineRenderer lineRenderer;

    

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f; // 设置线段起点的宽度
        lineRenderer.endWidth = 0.1f; // 设置线段终点的宽度
    }

    void Update()
    {
        // 设置线段的起点为物体的位置
        lineRenderer.SetPosition(0, transform.position);

        // 设置线段的终点为物体位置加上z轴方向的一个向量
        Vector3 endPosition = transform.position + transform.forward * 10;
        lineRenderer.SetPosition(1, endPosition);

        // 在0.05和0.15之间改变线段的宽度
        float width = Mathf.PingPong(Time.time, 0.1f) + 0.05f;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}