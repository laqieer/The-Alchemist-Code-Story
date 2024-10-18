// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayFriendRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
[FlowNode.Pin(100, "実行", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(101, "終了", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(102, "一括申請用プレハブ作成完了", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(103, "申請可能", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(104, "申請不可能", FlowNode.PinTypes.Output, 0)]
public class FlowNode_MultiPlayFriendRequest : FlowNode
{
  private List<FriendWindowItem> FriendItemList = new List<FriendWindowItem>();
  private int mCurrentIndex;
  [SerializeField]
  private GameObject Template;
  [SerializeField]
  private Button OkButton;
  [SerializeField]
  private GameObject TitleText_Normal;
  [SerializeField]
  private GameObject TitleText_RaidRescue;

  private bool mIsOrdeal
  {
    get
    {
      SceneBattle instance = SceneBattle.Instance;
      if ((bool) ((UnityEngine.Object) instance))
        return instance.IsOrdealQuest;
      return false;
    }
  }

  private int SearchTarget(int startIndex)
  {
    if (this.mIsOrdeal)
    {
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      if (ordealSupports != null)
      {
        for (int index = startIndex; index < ordealSupports.Count; ++index)
        {
          SupportData supportData = ordealSupports[index];
          if (supportData != null && !string.IsNullOrEmpty(supportData.FUID) && supportData.mIsFriend == 0)
            return index;
        }
      }
    }
    else
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
          if (multiFuid == null || multiFuid.status.Equals("none"))
            return index;
        }
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
    else if (this.mIsOrdeal)
    {
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      if (ordealSupports != null && this.mCurrentIndex < ordealSupports.Count)
      {
        GlobalVars.SelectedSupport.Set(ordealSupports[this.mCurrentIndex]);
        GlobalVars.SelectedFriendID = ordealSupports[this.mCurrentIndex].FUID;
        this.ActivateOutputLinks(100);
      }
      else
      {
        GlobalVars.SelectedSupport.Set((SupportData) null);
        GlobalVars.SelectedFriendID = (string) null;
        this.ActivateOutputLinks(101);
      }
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
    SceneBattle instance = SceneBattle.Instance;
    if ((bool) ((UnityEngine.Object) instance))
    {
      if (instance.IsOrdealQuest || !instance.IsPlayingMultiQuest)
      {
        if (!MonoSingleton<GameManager>.Instance.Player.IsRequestFriend() || this.SearchTarget(0) < 0)
        {
          this.mCurrentIndex = -1;
          GlobalVars.SelectedSupport.Set((SupportData) null);
          GlobalVars.SelectedFriendID = (string) null;
          this.ActivateOutputLinks(104);
          return;
        }
      }
      else if (this.SearchTarget(0) < 0)
      {
        this.mCurrentIndex = -1;
        GlobalVars.SelectedSupport.Set((SupportData) null);
        GlobalVars.SelectedFriendID = (string) null;
        this.ActivateOutputLinks(104);
        return;
      }
    }
    this.ActivateOutputLinks(103);
  }

  private bool IsDupricateFriend(string checkFUID, List<string> addedFUIDList)
  {
    foreach (string addedFuid in addedFUIDList)
    {
      if (checkFUID == addedFuid)
        return true;
    }
    return false;
  }

  public void CreateFriendItem()
  {
    if ((UnityEngine.Object) RaidManager.Instance != (UnityEngine.Object) null)
    {
      this.TitleText_Normal.SetActive(false);
      this.TitleText_RaidRescue.SetActive(true);
      List<RaidSOSMember> sos_members = RaidManager.Instance.GetSOSMembers();
      for (int i = 0; i < sos_members.Count; ++i)
      {
        if (!(sos_members[i].FUID == MonoSingleton<GameManager>.Instance.Player.FUID) && MonoSingleton<GameManager>.Instance.Player.Friends.Find((Predicate<FriendData>) (friend => friend.FUID == sos_members[i].FUID)) == null)
        {
          SupportData data = new SupportData();
          data.Unit = sos_members[i].Unit;
          data.FUID = sos_members[i].FUID;
          data.PlayerName = sos_members[i].Name;
          data.PlayerLevel = sos_members[i].Lv;
          data.UnitID = sos_members[i].Unit.UnitID;
          data.UnitLevel = sos_members[i].Unit.Lv;
          data.UnitRarity = sos_members[i].Unit.Rarity;
          data.JobID = sos_members[i].Unit.CurrentJobId;
          data.mIsFriend = sos_members[i].MemberType == RaidRescueMemberType.Friend ? 1 : 0;
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Template);
          gameObject.transform.SetParent(this.Template.transform.parent, false);
          DataSource.Bind<SupportData>(gameObject, data, false);
          FriendWindowItem component = gameObject.GetComponent<FriendWindowItem>();
          component.FriendRequest = this;
          component.Support = data;
          component.Refresh(false);
          this.FriendItemList.Add(component);
        }
      }
    }
    else if (this.mIsOrdeal)
    {
      this.TitleText_Normal.SetActive(true);
      this.TitleText_RaidRescue.SetActive(false);
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      if (ordealSupports != null)
      {
        List<string> addedFUIDList = new List<string>();
        foreach (SupportData supportData in ordealSupports)
        {
          SupportData support = supportData;
          if (support != null)
          {
            FriendData friendData = MonoSingleton<GameManager>.Instance.Player.Friends.Find((Predicate<FriendData>) (f => f.FUID == support.FUID));
            if ((friendData == null || friendData.State != FriendStates.Friend) && !this.IsDupricateFriend(support.FUID, addedFUIDList))
            {
              addedFUIDList.Add(support.FUID);
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Template);
              gameObject.transform.SetParent(this.Template.transform.parent, false);
              DataSource.Bind<SupportData>(gameObject, support, false);
              FriendWindowItem component = gameObject.GetComponent<FriendWindowItem>();
              component.FriendRequest = this;
              component.Support = support;
              component.CanBlock = false;
              component.Refresh(false);
              this.FriendItemList.Add(component);
            }
          }
        }
      }
    }
    else
    {
      this.TitleText_Normal.SetActive(true);
      this.TitleText_RaidRescue.SetActive(false);
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
          DataSource.Bind<SupportData>(gameObject, supportData, false);
          FriendWindowItem component = gameObject.GetComponent<FriendWindowItem>();
          component.FriendRequest = this;
          component.PlayerParam = myPlayersStarted[this.mCurrentIndex];
          component.Refresh(false);
          this.FriendItemList.Add(component);
        }
        else
          break;
      }
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
