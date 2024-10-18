// Decompiled with JetBrains decompiler
// Type: SRPG.InnWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "退店", FlowNode.PinTypes.Output, 10)]
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

    public void Activated(int pinID) => this.Refresh();

    public void Refresh()
    {
      if (Object.op_Inequality((Object) this.NotificationBadge, (Object) null))
      {
        this.NotificationBadge.SetActive(MonoSingleton<GameManager>.Instance.Player.FollowerNum > 0);
        GameParameter.UpdateAll(this.NotificationBadge);
      }
      if (!Object.op_Inequality((Object) this.FriendPresentButton, (Object) null))
        return;
      if (MonoSingleton<GameManager>.Instance.MasterParam.IsFriendPresentItemParamValid())
        this.FriendPresentButton.SetActive(true);
      else
        this.FriendPresentButton.SetActive(false);
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
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
