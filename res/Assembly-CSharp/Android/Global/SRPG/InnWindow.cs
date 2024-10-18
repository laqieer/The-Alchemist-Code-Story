// Decompiled with JetBrains decompiler
// Type: SRPG.InnWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "退店", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class InnWindow : MonoBehaviour, IFlowInterface
  {
    [Description("フレンド申請通知バッジ")]
    public GameObject NotificationBadge;
    [Description("フレンドプレゼントボタン")]
    public GameObject FriendPresentButton;

    private void Awake()
    {
    }

    private void Start()
    {
      MonoSingleton<GameManager>.Instance.OnSceneChange += new GameManager.SceneChangeEvent(this.OnGoOutInn);
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    public void Refresh()
    {
      if ((UnityEngine.Object) this.NotificationBadge != (UnityEngine.Object) null)
      {
        this.NotificationBadge.SetActive(MonoSingleton<GameManager>.Instance.Player.FollowerNum > 0);
        GameParameter.UpdateAll(this.NotificationBadge);
      }
      if (!((UnityEngine.Object) this.FriendPresentButton != (UnityEngine.Object) null))
        return;
      if (MonoSingleton<GameManager>.Instance.MasterParam.IsFriendPresentItemParamValid())
        this.FriendPresentButton.SetActive(true);
      else
        this.FriendPresentButton.SetActive(false);
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() != (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnGoOutInn);
    }

    private bool OnGoOutInn()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      return true;
    }
  }
}
