using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : RoleBase
{
    private enum State//23d状态
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

    public LaserController laserController;

    /// <summary>
    /// 得到摄像机
    /// </summary>
    public  Camera myCam;
    public GameObject lineShooter;

    public GameObject uiWeaponIndicator; // UI武器指示器

    public GameObject ui23dTransform; // 3D模式下的UI元素
    private bool isLaserMode = false; // 当前是否是激光模式

    public Image hpImage; // 血条图片

    private bool isGrounded;
    private Rigidbody rb;



    protected override void Awake()
    {
        AddController();  //开启监控器

         characterController=GetComponent<CharacterController>();

         laserController=GetComponent<LaserController>();
        Movespeed = 8;  //移动速度
        Rotatespeed = 30;  //转动速度
        IsDead = false;
        Hp = 100;      // 假设玩家的初始血量是100
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnDestroy()
    {
       
    }

    protected override void Update()
    {
        Move();  //移动

        Jump();  //跳跃
        
        Rotate();  //转动

        Atk();  //攻击

        hpImage.fillAmount=Hp/100f;  // 更新血条

    }

    #region 攻击
    public override void Atk()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&!isShooting) // 检测玩家是否按下了Q键
        {
            SwitchWeaponMode(); // 切换武器模式
        }

        // 根据当前模式调用相应的攻击方法
        if (isLaserMode)
        {
            LaserAtk();
        }
        else
        {
            BulletAtk();
        }
    }

    private void SwitchWeaponMode()
    {
        isLaserMode = !isLaserMode; // 切换模式
        UpdateUIWeaponIndicator(); // 更新UI指示器
    }

    private void UpdateUIWeaponIndicator()
    {
        if (uiWeaponIndicator != null)
        {
            // 根据当前模式设置UI指示器的文本或图标
            string indicatorText = isLaserMode ? "Laser" : "Bullet";
            uiWeaponIndicator.GetComponent<Text>().text = indicatorText;
        }
    }

    private bool isShooting = false; // 是否正在射击
    private float shootingInterval = 0.5f; // 射击间隔

    public void BulletAtk()
    {
        //print("武器换成子弹");
        laserController.enabled = false;

        if (Input.GetMouseButtonDown(0)) // 检测玩家是否点击了鼠标左键
        {
            isShooting = true;
            StartCoroutine(ShootBullets());
        }

        if (Input.GetMouseButtonUp(0)) // 检测玩家是否释放了鼠标左键
        {
            isShooting = false;
        }
    }

    private IEnumerator ShootBullets()
    {
        while (isShooting)
        {
            CreatePrefab("Player_Bullet", lineShooter.transform.position, Quaternion.LookRotation(lineShooter.transform.forward));
            yield return new WaitForSeconds(shootingInterval); // 等待射击间隔
        }
    }
    public void LaserAtk()
    {
        print("武器换成激光");
       laserController.enabled=true;
    }


    #endregion

    #region 受伤
    public override void Hurt()
    {
        int damage = 20;  // 假设伤害值为20
        Hp -= damage;  // 计算扣除防御力后的实际伤害
        if (Hp <= 0)
        {
            Dead();  // 调用死亡方法
        }
    }


    public void Hurt(int damage)
    {
        Hp -= damage;  // 计算扣除防御力后的实际伤害
        if (Hp <= 0)
        {
            Dead();  // 调用死亡方法
        }
    }
    #endregion

    #region 玩家死亡
    public override void Dead()
    {
        IsDead = true;
        Debug.Log("Player is dead.");
    }

#endregion

    #region 移动
    protected void Move()
    {
        // // 获取基于屏幕的方向移动输入
        // float horizontal = Input.GetAxis("Horizontal"); // A和D键
        // float vertical = Input.GetAxis("Vertical"); // W和S键

        // // 如果同时按下A和D或者W和S，或者都没有按，则不移动
        // if ((Mathf.Approximately(horizontal, 0f) && Mathf.Approximately(vertical, 0f)) ||
        //     (horizontal > 0 && horizontal < 0) ||
        //     (vertical > 0 && vertical < 0))
        // {
        //     return; // 不执行任何移动
        // }

        // // 计算移动方向，不改变角色朝向
        // Vector3 forwardMovement = myCam.transform.forward * vertical;
        // Vector3 rightMovement = myCam.transform.right * horizontal;

        // // 确保不改变Y轴上的移动量（即，不根据相机的倾斜角度向上或向下移动）
        // forwardMovement.y = 0;
        // rightMovement.y = 0;

        // // 应用移动
        // Vector3 movement = forwardMovement + rightMovement;

        // // 应用移动
        // characterController.Move(movement.normalized * Movespeed * Time.deltaTime);


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 获取摄像机的方向
        Vector3 cameraForward = myCam.transform.forward;
        Vector3 cameraRight = myCam.transform.right;

        // 使摄像机的方向与水平面平行
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // 计算移动方向
        Vector3 moveDirection = horizontal * cameraRight + vertical * cameraForward;

        // 应用移动
        rb.MovePosition(transform.position + moveDirection * Movespeed *2* Time.deltaTime);


    }
    #endregion

    #region 跳跃
    protected void Jump()
    {
        // 检测玩家是否接触地面
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1f);

        // 如果玩家接触地面并且按下跳跃键，则应用向上的力
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * 6, ForceMode.Impulse);
        }
    }
    #endregion

    #region 转动
    /// <summary>
    /// 转动
    /// </summary>
    public override void Rotate()
    {
        // 根据当前状态执行相应的方法
        switch (currentState)
        {
            case State.a:
                TowD();
                ui23dTransform.GetComponent<Text>().text = "2D";
                break;
            case State.b:
                ThreeD();
                ui23dTransform.GetComponent<Text>().text = "3D";
                break;
        }
    }

 

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


    #endregion

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
