using Cinemachine;
using UnityEngine;

public class CamControll : MonoBehaviour
{
    public CinemachineVirtualCamera topDownVCam;
    public CinemachineVirtualCamera followVCam;
    private CinemachineBrain cinemachineBrain;

    void Start()
    {
        // 获取CinemachineBrain组件
        cinemachineBrain = this.GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        // 按下空格键切换摄像机
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (topDownVCam.Priority > followVCam.Priority)
            {
                topDownVCam.Priority = 9; // 降低当前激活的摄像机优先级
                followVCam.Priority = 10; // 提升另一个摄像机优先级
            }
            else
            {
                topDownVCam.Priority = 10;
                followVCam.Priority = 9;
            }
        }
    }
}
