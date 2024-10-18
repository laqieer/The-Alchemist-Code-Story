// Decompiled with JetBrains decompiler
// Type: SRPG.RaidGuildStatsWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  public class RaidGuildStatsWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 1;
    [SerializeField]
    private GameObject mListItem;
    [SerializeField]
    private Transform mListParent;
    private static RaidGuildStatsWindow mInstance;
    private RaidGuildInfo mRaidGuildInfo;
    private List<RaidGuildMemberData> mRaidGuildMemberList;

    public static RaidGuildStatsWindow Instance
    {
      get
      {
        return RaidGuildStatsWindow.mInstance;
      }
    }

    private void Awake()
    {
      RaidGuildStatsWindow.mInstance = this;
      this.mListItem.SetActive(false);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }

    public void Setup(ReqRaidGuildStats.Response json)
    {
      if (json == null)
        return;
      if (json.my_guild_info != null)
      {
        this.mRaidGuildInfo = new RaidGuildInfo();
        if (!this.mRaidGuildInfo.Deserialize(json.my_guild_info))
          return;
      }
      if (json.member == null)
        return;
      this.mRaidGuildMemberList = new List<RaidGuildMemberData>();
      for (int index = 0; index < json.member.Length; ++index)
      {
        RaidGuildMemberData raidGuildMemberData = new RaidGuildMemberData();
        if (!raidGuildMemberData.Deserialize(json.member[index]))
          break;
        this.mRaidGuildMemberList.Add(raidGuildMemberData);
      }
    }

    private void Init()
    {
      if ((UnityEngine.Object) this.mListItem == (UnityEngine.Object) null || (UnityEngine.Object) this.mListParent == (UnityEngine.Object) null)
        return;
      DataSource.Bind<RaidGuildInfo>(this.gameObject, this.mRaidGuildInfo, false);
      GameParameter.UpdateAll(this.gameObject);
      if (this.mRaidGuildMemberList == null)
        return;
      this.mRaidGuildMemberList.Sort((Comparison<RaidGuildMemberData>) ((a, b) =>
      {
        if (a.RoleId == GuildMemberData.eRole.MASTAER && b.RoleId == GuildMemberData.eRole.MASTAER)
          return b.BeatScore + b.RescueScore - (a.BeatScore + a.RescueScore);
        if (a.RoleId == GuildMemberData.eRole.MASTAER)
          return -1;
        if (b.RoleId == GuildMemberData.eRole.MASTAER)
          return 1;
        if (a.RoleId == GuildMemberData.eRole.SUB_MASTAER && b.RoleId == GuildMemberData.eRole.MEMBER)
          return -1;
        if (b.RoleId == GuildMemberData.eRole.SUB_MASTAER && a.RoleId == GuildMemberData.eRole.MEMBER)
          return 1;
        return b.BeatScore + b.RescueScore - (a.BeatScore + a.RescueScore);
      }));
      for (int index = 0; index < this.mRaidGuildMemberList.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mListItem, this.mListParent);
        DataSource.Bind<RaidGuildMemberData>(gameObject, this.mRaidGuildMemberList[index], false);
        DataSource.Bind<GuildMemberData>(gameObject, (GuildMemberData) this.mRaidGuildMemberList[index], false);
        DataSource.Bind<UnitData>(gameObject, this.mRaidGuildMemberList[index].Unit, false);
        gameObject.SetActive(true);
      }
    }
  }
}
