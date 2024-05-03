using System.Collections;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Runtime.InteropServices;
using UnityEngine.PlayerLoop;
using System;
using DG.Tweening;

public class EndPanel : BasePanel
{
    
    
    protected override void Awake()
    {
        base.Awake(); // 调用基类的Awake方法以确保所有的UI控件都被找到并存储
      

        // 添加按钮点击事件
        GetControl<Button>("OnceMoreButton").onClick.AddListener(OnceMore);
        GetControl<Button>("ExitGameButton").onClick.AddListener(ExitGame);
    }

    // 在ShowMe方法中，我们将显示主菜单面板
    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }

    // 在HideMe方法中，我们将隐藏主菜单面板
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }

    // 处理开始游戏按钮点击事件
    private void StartGame()
    {
      
    }

   private void OnceMore()
    {
        // 定义一个回调函数，这个函数将在场景加载完成后执行
        UnityAction onSceneLoaded = () =>
        {
            
        };

        // 获取ScenesMgr实例并调用LoadSceneAsyn方法来异步加载场景
        ScenesMgr.GetInstance().LoadSceneAsyn("Game_1", onSceneLoaded);
    }



    private void ExitGame()
    {
        UnityAction onSceneLoaded = () =>
       {

       };

        // 获取ScenesMgr实例并调用LoadSceneAsyn方法来异步加载场景
        ScenesMgr.GetInstance().LoadSceneAsyn("Start", onSceneLoaded);

    }
}