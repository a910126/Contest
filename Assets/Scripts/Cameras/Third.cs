using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Third : MonoBehaviour  //第三人称
{
    /// <summary>
    /// 目标
    /// </summary>
    private GameObject Target;

    /// <summary>
    /// 距离Player身后
    /// </summary>
    public float DisLater = 5;

    /// <summary>
    /// 距离Player上方
    /// </summary>
    public float DisUp = 1;

    /// <summary>
    /// 距离Player的Vec3
    /// </summary>
    private UnityEngine.Vector3 offset;

    void Awake()
    {
        offset = new UnityEngine.Vector3(0, DisLater, DisUp);
    }

    void LateUpdate()
    {
        GetTarget();
    }
    private void GetTarget()
    {
        Target = GameObject.Find("Player");
        this.transform.position= Target.transform.position+offset;
        this.transform.LookAt(Target.transform);
    }
}
