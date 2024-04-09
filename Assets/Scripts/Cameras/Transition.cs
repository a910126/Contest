using System.Collections;
using UnityEngine;
using Cinemachine;

public class Transition : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;

    public ColliderTransition colliderTransitionScript;
    public bool isOrthographic = true; // 初始模式设置为正交
    public float transitionDuration = 1f; // 过渡持续时间，秒为单位

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 切换模式
            isOrthographic = !isOrthographic;
            StartCoroutine(TransitionLens());
        }
    }

    IEnumerator TransitionLens()
    {
        float timeElapsed = 0;
        var lens = vCam.m_Lens;

        // 设置初始值
        float startFOV = lens.FieldOfView;
        float startOrthoSize = lens.OrthographicSize;

        // 根据目标模式更新目标值
        float targetFOV = isOrthographic ? 80f : 80f; // 透视模式下FOV为80度
        float targetOrthoSize = isOrthographic ? 5f : 10f; 

        // 在过渡开始时设置摄像机模式
        lens.Orthographic = isOrthographic;

        while (timeElapsed < transitionDuration)
        {
            // 根据当前模式决定是否需要调整FOV或OrthographicSize
            if (!isOrthographic)
            {
                lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, timeElapsed / transitionDuration);
                colliderTransitionScript.UnenableTargetCollider();
            }
            else
            {
                lens.OrthographicSize = Mathf.Lerp(startOrthoSize, targetOrthoSize, timeElapsed / transitionDuration);
                colliderTransitionScript.EnableTargetCollider();
            }
            vCam.m_Lens = lens;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 确保最终值精确设置
        lens.FieldOfView = targetFOV;
        lens.OrthographicSize = targetOrthoSize;
        vCam.m_Lens = lens;
    }
}
