using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : RoleBase
{
    public Animator animator;
    public  enum  State//23d状态
    {
        a=2,
        b=3
    }
    // 子弹预制体路径
    
    // 当前状态
    public State currentState = State.a;
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

    bool pause = false;


    protected override void Awake()
    {
        AddController();  //开启监控器

        characterController=GetComponent<CharacterController>();
        laserController=GetComponent<LaserController>();

        Movespeed = 3;  //移动速度
        Rotatespeed = 30;  //转动速度
        IsDead = false;
        Hp = 100;      // 假设玩家的初始血量是100
        rb = GetComponent<Rigidbody>();
    }
    

    void OnDestroy()
    {
       
    }

    void FixedUpdate()
    {
        if (!pause)
        {
            Move();  //移动

            //Jump();  //跳跃

            Rotate();  //转动

            //Atk();  //攻击

            //ChangeCollider();
        }
    }
    protected override void Update()
    {
        if (!pause)
        {
            //Move();  //移动

            Jump();  //跳跃

           // Rotate();  //转动

            Atk();  //攻击

            ChangeCollider();
        }

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

   
    private float shootingInterval = 0.3f; // 射击间隔
    private bool isShooting = false; // 是否正在射击
    private float lastClickTime = 0f; // 上一次点击的时间


    public void BulletAtk()
    {
        laserController.enabled = false;
        if (Input.GetMouseButtonDown(0)) // 检测玩家是否点击了鼠标左键
        {
            float currentTime = Time.time;
            if (currentTime - lastClickTime > shootingInterval)
            {
                lastClickTime = currentTime; // 更新上次点击时间
                CreatePrefab("Player_Bullet", lineShooter.transform.position,this.transform.rotation);
                StartCoroutine(ShootBullets()); // 开始发射子弹的协程
            }
        }
    }

    private IEnumerator ShootBullets()
    {
        yield return new WaitForSeconds(shootingInterval); // 等待射击间隔
        while (Input.GetMouseButton(0)) // 检测玩家是否长按鼠标左键
        {
            CreatePrefab("Player_Bullet", lineShooter.transform.position,this.transform.rotation);
            //, Quaternion.LookRotation(lineShooter.transform.forward)
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
    private const string MoveParam = "IsWalk";
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


        if (rb.velocity.magnitude > 1.2f)
        {
            print("速度快");
            animator.SetBool(MoveParam, true);
        }
        else
        {
            // 如果没有移动输入，则暂停动画
            animator.SetBool(MoveParam, false);
        }
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
    /// 


    private bool hasExecutedA = false; // 用于检查是否已经执行过
    private bool hasExecutedB = false;
    public override void Rotate()
    {
        // 根据当前状态执行相应的方法
        switch (currentState)
        {
            case State.a:
                TowD();
                if (!hasExecutedA)
                {
                    hasExecutedA = true; 
                    hasExecutedB = false;
                    Invoke("DelayedMoveAndFade", 1.2f);
                }
                ui23dTransform.GetComponent<Text>().text = "2D";
                break;
            case State.b:
                ThreeD();
                if (!hasExecutedB)
                {
                    hasExecutedA = false;
                    hasExecutedB = true;
                    ChangePlayerPos(transform.position.x, transform.position.y);
                    transform.position = new Vector3(transform.position.x, transform.position.y, z);
                }
                ui23dTransform.GetComponent<Text>().text = "3D";
                break;
        }
    }

    private void DelayedMoveAndFade()
    {
        // 一秒后执行MoveAndFade
        MoveAndFade(new Vector3(transform.position.x, transform.position.y, 10), 2f);
    }
    public void MoveAndFade(Vector3 targetPosition, float duration)
    {
        // 立刻移动到指定位置
        transform.position = targetPosition;
        transform.DOPunchPosition(new Vector3(0.5f, 0, 0), 0.5f, 10, 1);

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            Material material = renderer.material;

            material.DOFade(0, duration);
        }
        
    }


    void OnTwo(){
        currentState = State.b;
    }

    // 当摄像机优先级改变时执行的方法
    void Onthree()
    {
        currentState = State.a;
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
        EventCenter.GetInstance().AddEventListener("暂停游戏",PauseGame);

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
        EventCenter.GetInstance().RemoveEventListener("暂停游戏",PauseGame);
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
            case KeyCode.LeftShift:
               
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

    public Vector3 prePos;
    private void ChangeCollider(){
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsFirst = !IsFirst;
            if (IsFirst)
            {
                prePos = transform.position;
                for (int i = 0; i < colliderTransition.Length; i++)
                {
                    colliderTransition[i].EnableTargetCollider();
                }
            }
            else
            {
                for (int i = 0; i < colliderTransition.Length; i++)
                {
                    colliderTransition[i].UnenableTargetCollider();
                }
            }
        }
    }
    #endregion

    public void PauseGame()
    {
        pause = !pause;
    }

    #region 针对第一场景的主角变换坐标的方法
    
    public float z;
    public void ChangePlayerPos(float x, float y)
    {
        z=transform.position.z;
        if (x > 0 && x < 5)
        {
            if (y < 2 && y > 0)
            {
               print("第一阶段");
            }
        }

        if (x > 5 && x < 10)
        {
            if (y < 5 && y > 0)
            {
                z = 15;
                print("第二阶段");
            }
        }

        if (x > 10 && x < 22)
        {
            if (y < 6.5 && y > 5)
            {
                z = 15;
                print("第三阶段");
            }
        }

        if (x > 22 && x < 28)
        {
            if (y < 7.5 && y > 5.7)
            {
                z = 28;
                print("第四阶段");
            }
        }

        if (x > 28 && x < 41)
        {
            if (y < 10 && y > 8)
            {
                z = 40;
                print("第五阶段");
            }
        }

        if (x > 41 && x < 49)
        {
            if (y < 12 && y > 8.5)
            {
                z = 45;
                print("第六阶段");
            }
        }

        if (x > 49 && x < 70)
        {
            if (y < 15 && y > 12)
            {
                z = 28;
                print("第七阶段");
            }
        }
    }
    #endregion

}


  