using UnityEngine;
using DG.Tweening;

public class ALing : MonoBehaviour
{
    public Transform followPoint; // 玩家子物体的Transform
    public float floatAmplitude = 0.5f; // 上下浮动的幅度
    public float floatFrequency = 1f; // 上下浮动的频率
    public float followSpeed = 2f; // 跟随速度

    private void Start()
    {
        // 使球体上下浮动
        transform.DOMoveY(floatAmplitude, floatFrequency)
                 .SetRelative(true)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        // 平滑更新位置以实时跟随玩家
        transform.position = Vector3.Lerp(transform.position, followPoint.position, followSpeed * Time.deltaTime);
    }
}
