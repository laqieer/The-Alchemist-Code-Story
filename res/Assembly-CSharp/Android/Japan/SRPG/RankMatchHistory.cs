// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class RankMatchHistory : SRPG_ListBase
  {
    [SerializeField]
    private GameObject PlayerGO;
    [SerializeField]
    private ListItemEvents ListItem;
    [Space(10f)]
    [SerializeField]
    private Text LastBattleDate;

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.PlayerGO != (UnityEngine.Object) null)
        DataSource.Bind<PlayerData>(this.PlayerGO, MonoSingleton<GameManager>.Instance.Player, false);
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
        return;
      this.ClearItems();
      this.ListItem.gameObject.SetActive(false);
      Network.RequestAPI((WebAPI) new ReqRankMatchHistory(new Network.ResponseCallback(this.ResponseCallback)), false);
    }

    private void ResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.MultiMaintenance:
          case Network.EErrCode.VsMaintenance:
          case Network.EErrCode.MultiVersionMaintenance:
          case Network.EErrCode.MultiTowerMaintenance:
            Network.RemoveAPI();
            this.enabled = false;
            break;
          default:
            if (errCode != Network.EErrCode.OutOfDateQuest)
            {
              if (errCode == Network.EErrCode.MultiVersionMismatch || errCode == Network.EErrCode.VS_Version)
              {
                Network.RemoveAPI();
                Network.ResetError();
                this.enabled = false;
                break;
              }
              FlowNode_Network.Retry();
              break;
            }
            Network.RemoveAPI();
            Network.ResetError();
            this.enabled = false;
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response> jsonBodyResponse = JsonUtility.FromJson<WebAPI.JSON_BodyResponse<ReqRankMatchHistory.Response>>(www.text);
        DebugUtility.Assert(jsonBodyResponse != null, "res == null");
        if (jsonBodyResponse.body == null)
        {
          Network.RemoveAPI();
        }
        else
        {
          if (jsonBodyResponse.body.histories == null || jsonBodyResponse.body.histories.list == null)
            return;
          long unixtime = 0;
          for (int index = 0; index < jsonBodyResponse.body.histories.list.Length; ++index)
          {
            ReqRankMatchHistory.ResponceHistoryList data1 = jsonBodyResponse.body.histories.list[index];
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ListItem);
            DataSource.Bind<ReqRankMatchHistory.ResponceHistoryList>(listItemEvents.gameObject, data1, false);
            FriendData data2 = new FriendData();
            data2.Deserialize(data1.enemy);
            DataSource.Bind<FriendData>(listItemEvents.gameObject, data2, false);
            DataSource.Bind<ViewGuildData>(listItemEvents.gameObject, data2.ViewGuild, false);
            DataSource.Bind<UnitData>(listItemEvents.gameObject, data2.Unit, false);
            SerializeValueBehaviour component = listItemEvents.GetComponent<SerializeValueBehaviour>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              long num = data2.ViewGuild == null ? 0L : (long) data2.ViewGuild.id;
              component.list.SetField(GuildSVB_Key.GUILD_ID, (int) num);
            }
            this.AddItem(listItemEvents);
            listItemEvents.transform.SetParent(this.transform, false);
            listItemEvents.gameObject.SetActive(true);
            if (unixtime < data1.time_end)
              unixtime = data1.time_end;
          }
          if ((UnityEngine.Object) this.LastBattleDate != (UnityEngine.Object) null && unixtime > 0L)
            this.LastBattleDate.text = TimeManager.FromUnixTime(unixtime).ToString("MM/dd HH:mm");
          Network.RemoveAPI();
        }
      }
    }
  }
}
