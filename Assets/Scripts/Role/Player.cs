using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RoleBase
{

    private enum State
    {
        a=2,
        b=3
    }

    // 当前状态
    private State currentState = State.a;
    /// <summary>
    /// 记录是第一人称还是第三人称
    /// </summary>
    private bool IsFirst=true;
    public ColliderTransition[] colliderTransition;
    public CharacterController characterController;
    /// <summary>
    /// 得到摄像机
    /// </summary>
    private GameObject C;
    public  Camera myCam;
    public GameObject lineShooter;

    

    void Awake()
    {
        AddController();  //开启监控器

         characterController=GetComponent<CharacterController>();

        Movespeed = 5;  //移动速度

        Rotatespeed = 30;  //转动速度
    }
    void Start()
    {
        
    }
    void OnDestroy()
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
        // 获取基于屏幕的方向移动输入
        float horizontal = Input.GetAxis("Horizontal"); // A和D键
        float vertical = Input.GetAxis("Vertical"); // W和S键

        // 如果同时按下A和D或者W和S，或者都没有按，则不移动
        if ((Mathf.Approximately(horizontal, 0f) && Mathf.Approximately(vertical, 0f)) ||
            (horizontal > 0 && horizontal < 0) ||
            (vertical > 0 && vertical < 0))
        {
            return; // 不执行任何移动
        }

        // 计算移动方向，不改变角色朝向
        Vector3 forwardMovement = myCam.transform.forward * vertical;
        Vector3 rightMovement = myCam.transform.right * horizontal;

        // 确保不改变Y轴上的移动量（即，不根据相机的倾斜角度向上或向下移动）
        forwardMovement.y = 0;
        rightMovement.y = 0;

        // 应用移动
        Vector3 movement = forwardMovement + rightMovement;

        // 应用移动
        characterController.Move(movement.normalized * Movespeed * Time.deltaTime);

    }
    #endregion

    #region 转动
    /// <summary>
    /// 转动
    /// </summary>
    protected void Rotate()
    {
        // 根据当前状态执行相应的方法
        switch (currentState)
        {
            case State.a:
                TowD();
                break;
            case State.b:
                ThreeD();
                break;
        }
    }

    #endregion

void OnTwo(){
        currentState = State.b;
        //print("qiehuan 111");
    }

    // 当摄像机优先级改变时执行的方法
    void Onthree()
    {
        currentState = State.a;
        //print("qiehuan 222");
    }


        void ThreeD(){
        // this.transform.Rotate(Input.GetAxis("Mouse X") * Time.deltaTime * Rotatespeed * this.transform.up);
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // 假设地面在y=0的平面
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 direction = point - transform.position;
            direction.y = 0; // 确保旋转只围绕Y轴
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        }
    }

        void TowD(){
        // 获取鼠标在屏幕上的位置
        Vector3 mousePosition = Input.mousePosition;

        // 判断鼠标在屏幕的左半边还是右半边
        if (mousePosition.x <= Screen.width / 2)
        {
            // 如果鼠标在屏幕的左半边，角色朝向固定左
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            // 如果鼠标在屏幕的右半边，角色朝向固定右
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
    
    #region 监控器

    /// <summary>
    /// 添加监控器
    /// </summary>
    public void AddController()
    {
        InputMgr.GetInstance().StartOrEndCheck(true);
        // 订阅事件
        EventCenter.GetInstance().AddEventListener("Three", Onthree);
        EventCenter.GetInstance().AddEventListener("Two", OnTwo);
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyDown", CheckKeyDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("SomeKeyUp", CheckKeyUp);
    }

    /// <summary>
    /// 移除监控器
    /// </summary>
    public void RemoveController()
    {
        InputMgr.GetInstance().StartOrEndCheck(false);
        // 取消订阅事件，以避免内存泄漏
        EventCenter.GetInstance().RemoveEventListener("three", Onthree);
        EventCenter.GetInstance().RemoveEventListener("Two", OnTwo);
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
                    for(int i=0;i<colliderTransition.Length;i++)
                    {
                        colliderTransition[i].EnableTargetCollider();
                    }
                   
                }
                else
                {
                    for(int i=0;i<colliderTransition.Length;i++)
                    {
                        colliderTransition[i].UnenableTargetCollider();
                    }
                   
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
