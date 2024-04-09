using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RoleBase
{
    /// <summary>
    /// 记录是第一人称还是第三人称
    /// </summary>
    private bool IsFirst=true;

    /// <summary>
    /// 得到摄像机
    /// </summary>
    private GameObject C;
    void Awake()
    {
        AddController();  //开启监控器

        C = Camera.main.gameObject;  //得到主摄像机

        Movespeed = 5;  //移动速度

        Rotatespeed = 30;  //转动速度
    }
    void Start()
    {

    }

    void Update()
    {
        Move();  //移动

        Rotate();  //转动
    }

    #region 攻击
    public override void Atk()
    {
        
    }
    #endregion

    #region 受伤
    public override void Hurt()
    {
        
    }
    #endregion

    #region 移动
    protected override void Move()
    {
        this.transform.Translate(this.transform.forward * Time.deltaTime * Movespeed*Input.GetAxisRaw("Vertical"));
        this.transform.Translate(this.transform.right * Time.deltaTime * Movespeed * Input.GetAxisRaw("Horizontal"));
    }
    #endregion

    #region 转动
    /// <summary>
    /// 转动
    /// </summary>
    protected void Rotate()
    {
        this.transform.Rotate(Input.GetAxis("Mouse X") * Time.deltaTime * Rotatespeed * this.transform.up);
    }

    #endregion

    #region 监控器

    /// <summary>
    /// 添加监控器
    /// </summary>
    public void AddController()
    {
        InputMgr.GetInstance().StartOrEndCheck(true);
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyUp", CheckKeyUp);
    }

    /// <summary>
    /// 移除监控器
    /// </summary>
    public void RemoveController()
    {
        InputMgr.GetInstance().StartOrEndCheck(false);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("SomeKeyUp", CheckKeyUp);
    }
    #endregion

    #region 检测按键

    /// <summary>
    /// 检测按键按下
    /// </summary>
    /// <param name="keyCode"></param>
    private void CheckKeyDown(KeyCode keyCode)
    {
        switch(keyCode)
        {     
            case KeyCode.J:
                print("sesa");
                break;
            case KeyCode.K:
                break;
            case KeyCode.L:
                break;
            case KeyCode.Space:
                IsFirst = !IsFirst;
                if (IsFirst)
                {
                    C.GetComponent<First>().enabled=true;
                    C.GetComponent<Third>().enabled = false;
                }
                else
                {
                    C.GetComponent<First>().enabled = false;
                    C.GetComponent<Third>().enabled = true;
                }
                break;
        }
    }

    /// <summary>
    /// 检测按键抬起
    /// </summary>
    /// <param name="keyCode"></param>
    private void CheckKeyUp(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.J:
                break;
            case KeyCode.K:
                break;
            case KeyCode.L:
                break;
            case KeyCode.Space:
                break;
        }
    }
    #endregion
}
