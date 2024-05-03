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

public class MainMenuPanel : BasePanel
{
    string sceneName = "Game_1";
    private VideoPlayer video;
    public GameObject SkipButton;
    protected override void Awake()
    {
        base.Awake(); // 调用基类的Awake方法以确保所有的UI控件都被找到并存储
        video = this.GetComponent<VideoPlayer>();

        // 添加按钮点击事件
        GetControl<Button>("StartGameButton").onClick.AddListener(StartGame);
        GetControl<Button>("ContinueGameButton").onClick.AddListener(ContinueGame);
        GetControl<Button>("ExitGameButton").onClick.AddListener(ExitGame);
        GetControl<Button>("Skip").onClick.AddListener(Sikp);
        SkipButton.SetActive(false);
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
       
        video.Play();
        GameObject.Find("Image").SetActive(false);
        GameObject.Find("StartGameButton").SetActive(false);
        GameObject.Find("ContinueGameButton").SetActive(false);
        GameObject.Find("ExitGameButton").SetActive(false);
        StartCoroutine(EnableSkipButtonWithDelay(2f));

    }

    private IEnumerator EnableSkipButtonWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的延迟时间

        // 启用 SkipButton
        SkipButton.SetActive(true);

        // 添加淡入效果
        CanvasGroup canvasGroup = SkipButton.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // 如果 SkipButton 上没有 CanvasGroup 组件，则添加一个
            canvasGroup = SkipButton.AddComponent<CanvasGroup>();
        }

        // 设置初始透明度为 0
        canvasGroup.alpha = 0f;

        // 执行淡入效果
        canvasGroup.DOFade(1f, 1f);
    }

    // 处理继续游戏按钮点击事件
    private void ContinueGame()
    {
        // 在这里添加继续游戏的代码
    }


   private void Sikp()
    {
        
        // 定义一个回调函数，这个函数将在场景加载完成后执行
        UnityAction onSceneLoaded = () =>
        {
            
        };

        // 获取ScenesMgr实例并调用LoadSceneAsyn方法来异步加载场景
        ScenesMgr.GetInstance().LoadSceneAsyn(sceneName, onSceneLoaded);

    }

    // 处理退出游戏按钮点击事件
    private void ExitGame()
    {
        Application.Quit();
        // 在编辑器中输出信息，因为编辑器不会实际退出
        Debug.Log("退出游戏");
    }

    void Update()
    {
        if (video.time >= video.length)
        {
            // 视频已经播放完毕
            Sikp();
        }
    }
}