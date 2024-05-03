using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 引入UI库
using DG.Tweening; // 引入DoTween库

public class LaserController : MonoBehaviour
{
    public GameObject laserPrefab;
    public Image chargeImage; // UI图片
    private bool isCharging = false;
    private float chargeTime = 0f;
    private float minChargeTime = 1f; // 激光最小蓄力时间
    private float maxChargeTime = 3f; // 激光最大蓄力时间
    private Color originalColor; // 图片的原始颜色
    private bool hasShaken = false; // 是否已经抖动过

    private void Start()
    {
        // 保存图片的原始颜色
        originalColor = chargeImage.color;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键按下
        {
            isCharging = true;
            chargeTime = 0f;
            hasShaken = false;
        }

        if (Input.GetMouseButtonUp(0)) // 鼠标左键释放
        {
            isCharging = false;
            if (chargeTime >= minChargeTime) // 蓄力时间超过最小蓄力时间
            {
                ReleaseLaser();
            }
        }

        if (isCharging) // 长按蓄力
        {
            chargeTime += Time.deltaTime;
            if (chargeTime >= maxChargeTime) // 达到最大蓄力时间
            {
                chargeTime = maxChargeTime;
            }
            // 更新UI图片的填充量
            chargeImage.fillAmount = chargeTime / maxChargeTime;

            if (chargeTime >= minChargeTime && !hasShaken) // 蓄力时间超过最小蓄力时间且还未抖动过
            {
                // 改变图片的颜色为红色
                chargeImage.DOColor(Color.red, 0.2f);
                // 让图片颤抖
                chargeImage.transform.DOShakeScale(0.2f, 0.1f);
                hasShaken = true;
            }
        }
        else
        {
            // 重置UI图片的填充量和颜色
            chargeImage.fillAmount = 0;
            chargeImage.color = originalColor;
        }
    }

    private void ReleaseLaser()
    {
        // 实例化激光预制体
        GameObject laser = Instantiate(laserPrefab, transform.position+new Vector3(0,3,0), transform.rotation);
        // 根据蓄力时间来调整激光的长度
        laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, chargeTime * 2f);
        // 销毁激光，或者让它自然消失
        Destroy(laser, chargeTime + 1f);
    }
}