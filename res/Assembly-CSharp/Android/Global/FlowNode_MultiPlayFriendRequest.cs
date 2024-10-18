// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayFriendRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[FlowNode.NodeType("Multi/MultiPlayFriendRequest", 32741)]
[FlowNode.Pin(0, "開始", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "次の人", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(2, "一括申請用プレハブ作成", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(3, "フレンド申請が可能か", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(101, "終了", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(102, "一括申請用プレハブ作成完了", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(103, "申請可能", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(104, "申請不可能", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(100, "実行", FlowNode.PinTypes.Output, 0)]
public class FlowNode_MultiPlayFriendRequest : FlowNode
{
  private List<FriendWindowItem> FriendItemList = new List<FriendWindowItem>();
  private int mCurrentIndex;
  [SerializeField]
  private GameObject Template;
  [SerializeField]
  private Button OkButton;

  private int SearchTarget(int startIndex)
  {
    MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
    List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
    if (myPlayersStarted == null || MonoSingleton<GameManager>.Instance.Player == null)
      return -1;
    List<MultiFuid> multiFuids = MonoSingleton<GameManager>.Instance.Player.MultiFuids;
    for (int index = startIndex; index < myPlayersStarted.Count; ++index)
    {
      JSON_MyPhotonPlayerParam startedPlayer = myPlayersStarted[index];
      if (startedPlayer != null && startedPlayer.playerIndex != instance.MyPlayerIndex && !string.IsNullOrEmpty(startedPlayer.FUID))
      {
        MultiFuid multiFuid = multiFuids?.Find((Predicate<MultiFuid>) (f =>
        {
          if (f.fuid != null)
            return f.fuid.Equals(startedPlayer.FUID);
          return false;
        }));
        if (multiFuid != null && multiFuid.status.Equals("none"))
          return index;
      }
    }
    return -1;
  }

  private void Output()
  {
    if (!MonoSingleton<GameManager>.Instance.Player.IsRequestFriend())
    {
      this.mCurrentIndex = -1;
      GlobalVars.SelectedSupport.Set((SupportData) null);
      GlobalVars.SelectedFriendID = (string) null;
      this.ActivateOutputLinks(101);
    }
    else if (this.mCurrentIndex < 0)
    {
      GlobalVars.SelectedSupport.Set((SupportData) null);
      GlobalVars.SelectedFriendID = (string) null;
      this.ActivateOutputLinks(101);
    }
    else
    {
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
      GlobalVars.SelectedSupport.Set(myPlayersStarted?[this.mCurrentIndex].CreateSupportData());
      GlobalVars.SelectedFriendID = GlobalVars.SelectedSupport.Get() != null ? myPlayersStarted[this.mCurrentIndex].FUID : (string) null;
      this.ActivateOutputLinks(100);
    }
  }

  private void CheckFriend()
  {
    if (!MonoSingleton<GameManager>.Instance.Player.IsRequestFriend() || this.SearchTarget(0) < 0)
    {
      this.mCurrentIndex = -1;
      GlobalVars.SelectedSupport.Set((SupportData) null);
      GlobalVars.SelectedFriendID = (string) null;
      this.ActivateOutputLinks(104);
    }
    else
      this.ActivateOutputLinks(103);
  }

  public void CreateFriendItem()
  {
    List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
    this.mCurrentIndex = -1;
    for (int index = 0; index < myPlayersStarted.Count; ++index)
    {
      this.mCurrentIndex = this.SearchTarget(this.mCurrentIndex + 1);
      if (this.mCurrentIndex >= 0)
      {
        SupportData supportData = myPlayersStarted?[this.mCurrentIndex].CreateSupportData();
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Template);
        gameObject.transform.SetParent(this.Template.transform.parent, false);
        DataSource.Bind<SupportData>(gameObject, supportData);
        FriendWindowItem component = gameObject.GetComponent<FriendWindowItem>();
        component.FriendRequest = this;
        component.PlayerParam = myPlayersStarted[this.mCurrentIndex];
        this.FriendItemList.Add(component);
      }
      else
        break;
    }
    GameParameter.UpdateAll(this.Template.transform.parent.gameObject);
    this.Template.SetActive(false);
  }

  public void SetInteractable()
  {
    if (!((UnityEngine.Object) this.OkButton != (UnityEngine.Object) null))
      return;
    this.OkButton.interactable = false;
    for (int index = 0; index < this.FriendItemList.Count; ++index)
    {
      if (this.FriendItemList[index].IsOn)
      {
        this.OkButton.interactable = true;
        break;
      }
    }
  }

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 0:
        this.mCurrentIndex = this.SearchTarget(0);
        this.Output();
        break;
      case 1:
        this.mCurrentIndex = this.SearchTarget(this.mCurrentIndex + 1);
        this.Output();
        break;
      case 2:
        if ((UnityEngine.Object) this.OkButton != (UnityEngine.Object) null)
          this.OkButton.interactable = false;
        this.CreateFriendItem();
        break;
      case 3:
        this.CheckFriend();
        break;
    }
  }
}
