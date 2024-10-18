// Decompiled with JetBrains decompiler
// Type: SRPG.GuildAttendanceList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "プレイヤー詳細へ", FlowNode.PinTypes.Output, 100)]
  public class GuildAttendanceList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_OUTPUT_OPEN_PLAYER_INFO = 100;
    [SerializeField]
    [Header("現在の出席人数テキスト")]
    private Text CurrentAttendCountText;
    [SerializeField]
    [Header("前日の出席人数テキスト")]
    private Text PrevAttendCountText;
    [SerializeField]
    [Header("出席タイマーテキスト")]
    private Text AttendanceTimerText;
    [Space(10f)]
    [SerializeField]
    [Header("ポートメンバーリストの親")]
    private Transform MemberRoot;
    [SerializeField]
    [Header("ポートメンバーリストアイテムのテンプレート")]
    private GameObject MemberTemplate;
    private DateTime mEndTime;
    private DateTime mElapsedTime;

    private void Awake() => GameUtility.SetGameObjectActive(this.MemberTemplate, false);

    private void Start() => this.Refresh();

    private void Update() => this.RefreshAttendanceTime();

    private void Refresh()
    {
      if (MonoSingleton<GameManager>.Instance.Player.Guild == null)
        return;
      ReqGuildAttend.Response response = FlowNode_ReqGuildAttend.GetResponse();
      if (response == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentAttendCountText, (UnityEngine.Object) null))
        this.CurrentAttendCountText.text = (response.attend_guild_member_uids == null ? 0 : response.attend_guild_member_uids.Length).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevAttendCountText, (UnityEngine.Object) null))
        this.PrevAttendCountText.text = response.yesterday_attendance.ToString();
      this.mElapsedTime = TimeManager.ServerTime;
      this.mEndTime = new DateTime(this.mElapsedTime.Year, this.mElapsedTime.Month, this.mElapsedTime.Day).AddDays(1.0);
      Dictionary<string, BindViewGuildMemberData> dict = new Dictionary<string, BindViewGuildMemberData>();
      if (response.attend_guild_member_uids != null && response.attend_guild_member_uids.Length > 0)
      {
        for (int index = 0; index < response.attend_guild_member_uids.Length; ++index)
        {
          string attendGuildMemberUid = response.attend_guild_member_uids[index];
          BindViewGuildMemberData viewGuildMemberData = new BindViewGuildMemberData();
          viewGuildMemberData.SetAttendStatus(GuildManager.GuildAttendStatus.ATTENDED);
          dict.Add(attendGuildMemberUid, viewGuildMemberData);
        }
      }
      this.SetupMemberList(dict);
    }

    private void SetupMemberList(Dictionary<string, BindViewGuildMemberData> dict)
    {
      if (MonoSingleton<GameManager>.Instance.Player.Guild == null)
        return;
      List<GuildMemberData> guildMemberDataList = new List<GuildMemberData>((IEnumerable<GuildMemberData>) GuildCustomMenu.GetSortedMemberList(MonoSingleton<GameManager>.Instance.Player.Guild.Members, GuildCustomMenu.eSortType.MIN));
      int count = guildMemberDataList.Count;
      for (int index = 0; index < count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.MemberTemplate);
        gameObject.transform.SetParent(this.MemberRoot, false);
        DataSource.Bind<GuildMemberData>(gameObject, guildMemberDataList[index]);
        DataSource.Bind<UnitData>(gameObject, guildMemberDataList[index].Unit);
        if (dict != null && dict.ContainsKey(guildMemberDataList[index].Uid))
          DataSource.Bind<BindViewGuildMemberData>(gameObject, dict[guildMemberDataList[index].Uid]);
        GameUtility.SetGameObjectActive(gameObject, true);
      }
    }

    private void RefreshAttendanceTime()
    {
      this.mElapsedTime = this.mElapsedTime.AddSeconds((double) Time.deltaTime);
      TimeSpan timeSpan = this.mEndTime - this.mElapsedTime;
      if (timeSpan <= TimeSpan.Zero)
        timeSpan = TimeSpan.Zero;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AttendanceTimerText, (UnityEngine.Object) null))
        return;
      this.AttendanceTimerText.text = string.Format(LocalizedText.Get("sys.GUILDATTENDANCELIST_TIME_FORMAT", (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds));
    }

    public void OnClickMemberUnitIcon(GameObject obj)
    {
      GuildMemberData dataOfClass = DataSource.FindDataOfClass<GuildMemberData>(obj.gameObject, (GuildMemberData) null);
      if (dataOfClass == null)
        return;
      FlowNode_Variable.Set("SelectUserID", dataOfClass.Uid);
      FlowNode_Variable.Set("IsBlackList", "0");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public void Activated(int pinID)
    {
    }
  }
}
