using Cinemachine;
using UnityEngine;

public class CamControll : MonoBehaviour
{
    public CinemachineVirtualCamera topDownVCam;
    public CinemachineVirtualCamera followVCam;
    private CinemachineBrain cinemachineBrain;
    private bool spacePressed = false; // 用于记录空格键是否已经按下

    void Start()
    {
        // 获取CinemachineBrain组件
        cinemachineBrain = this.GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        // 按下空格键切换摄像机，并确保只在按下时触发一次事件
        if (Input.GetKeyDown(KeyCode.Space) && !spacePressed)
        {
            spacePressed = true; // 设置空格键按下标志为true

            if (topDownVCam.Priority > followVCam.Priority)
            {
                topDownVCam.Priority = 9; // 降低当前激活的摄像机优先级
                followVCam.Priority = 10; // 提升另一个摄像机优先级
                EventCenter.GetInstance().EventTrigger("Three");
            }
            else
            {
                topDownVCam.Priority = 10;
                followVCam.Priority = 9;
                EventCenter.GetInstance().EventTrigger("Two");
            }
        }
        // 松开空格键时重置按键状态
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            spacePressed = false; // 设置空格键按下标志为false
        }
    }
}

