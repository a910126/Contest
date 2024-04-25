using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BasePanel
{
    

    protected override void Awake()
    {
        base.Awake();
        GetControl<Button>("Quit").onClick.AddListener(QuitGame);
        GetControl<Button>("Return").onClick.AddListener(TogglePauseMenu);
        this.HideMe();
    }


    public override void ShowMe()
    {
        this.gameObject.SetActive(true);
    }

    // 在HideMe方法中，我们将隐藏主菜单面板
    public override void HideMe()
    {
        this.gameObject.SetActive(false);
    }

    

    public void TogglePauseMenu()
    {
        // 切换暂停面板的状态
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        
        EventCenter.GetInstance().EventTrigger("暂停游戏");
        
        // 暂停或恢复游戏
        Time.timeScale = this.gameObject.activeSelf ? 0 : 1;
       

    }

  
    public void QuitGame()
    {
        // 退出游戏
        Application.Quit();
        print("Quit Game");
    }
}
