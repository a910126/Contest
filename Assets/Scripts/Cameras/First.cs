using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First : MonoBehaviour  //第一人称
{
    /// <summary>
    /// 目标
    /// </summary>
    private GameObject Target;

    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    GetTarget();
    //}

    void LateUpdate()
    {
        GetTarget();
    }

    /// <summary>
    /// 得到目标位置角度信息
    /// </summary>
    /// <param name="TargetPos"></param>
    /// <param name="TargetRot"></param>
    private void GetTarget()
    {
        Target = GameObject.Find("Player");
        this.transform.position = Target.transform.position;
        this.transform.rotation = Target.transform.rotation;
    }

}
