using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 锁定光标到游戏窗口的中心
        Cursor.lockState = CursorLockMode.Locked;

        // 隐藏光标
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;

            // 显示光标
            Cursor.visible = true;
        }
    }
}
