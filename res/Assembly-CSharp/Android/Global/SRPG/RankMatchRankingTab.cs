// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchRankingTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class RankMatchRankingTab : SRPG_ListBase
  {
    [SerializeField]
    private GameObject PlayerGO;
    [SerializeField]
    private GameObject PlayerUnit;
    [SerializeField]
    private ListItemEvents ListItem;

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.PlayerUnit != (UnityEngine.Object) null && (UnityEngine.Object) this.PlayerGO != (UnityEngine.Object) null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        DataSource.Bind<PlayerData>(this.PlayerGO, player);
        DataSource.Bind<UnitData>(this.PlayerUnit, player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID));
        GameParameter.UpdateAll(this.PlayerUnit);
      }
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
        return;
      this.ClearItems();
      this.ListItem.gameObject.SetActive(false);
      Network.RequestAPI((WebAPI) new ReqRankMatchRanking(new Network.ResponseCallback(this.ResponseCallback)), false);
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
        WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqRankMatchRanking.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null || jsonObject.body.rankings == null)
        {
          Network.RemoveAPI();
        }
        else
        {
          for (int index = 0; index < jsonObject.body.rankings.Length; ++index)
          {
            ReqRankMatchRanking.ResponceRanking ranking = jsonObject.body.rankings[index];
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ListItem);
            DataSource.Bind<ReqRankMatchRanking.ResponceRanking>(listItemEvents.gameObject, ranking);
            FriendData data = new FriendData();
            data.Deserialize(ranking.enemy);
            DataSource.Bind<FriendData>(listItemEvents.gameObject, data);
            DataSource.Bind<UnitData>(listItemEvents.gameObject, data.Unit);
            this.AddItem(listItemEvents);
            listItemEvents.transform.SetParent(this.transform, false);
            listItemEvents.gameObject.SetActive(true);
          }
          Network.RemoveAPI();
        }
      }
    }
  }
}
