// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TowerStartQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  [FlowNode.NodeType("System/Quest/TowerStart", 32741)]
  [FlowNode.Pin(7, "TowerError", FlowNode.PinTypes.Output, 7)]
  public class FlowNode_TowerStartQuest : FlowNode_StartQuest
  {
    private long btlID;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public override void OnActivate(int pinID)
    {
      MonoSingleton<GameManager>.Instance.AudienceMode = false;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      instance2.IsMultiPlay = false;
      instance2.IsMultiVersus = false;
      instance1.IsVSCpuBattle = false;
      if (pinID == 0 || pinID == 100 || (pinID == 200 || pinID == 500) || (pinID == 700 || pinID == 1000))
      {
        instance2.IsMultiPlay = pinID == 100 || pinID == 200 || pinID == 500;
        instance2.IsMultiVersus = pinID == 200;
        instance1.IsVSCpuBattle = pinID == 700;
      }
      if (pinID == 10)
      {
        this.mResume = true;
        pinID = 0;
      }
      if (pinID != 0 || this.enabled)
        return;
      this.enabled = true;
      CriticalSection.Enter(CriticalSections.SceneChange);
      PlayerPartyTypes type = PlayerPartyTypes.Tower;
      GlobalVars.SelectedPartyIndex.Set((int) type);
      MonoSingleton<GameManager>.Instance.Player.SetPartyCurrentIndex((int) type);
      PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay = false;
      PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus = false;
      PunMonoSingleton<MyPhoton>.Instance.IsRankMatch = false;
      if (this.mResume)
      {
        this.btlID = (long) GlobalVars.BtlID;
        GlobalVars.BtlID.Set(0L);
        GlobalVars.BtlIDStatus.Set(true);
        this.ExecRequest((WebAPI) new ReqTowerBtlComResume(this.btlID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
      {
        if (!string.IsNullOrEmpty(this.QuestID))
        {
          GlobalVars.SelectedQuestID = this.QuestID;
          GlobalVars.SelectedFriendID = string.Empty;
        }
        this.mStartingQuest = instance1.FindQuest(GlobalVars.SelectedQuestID);
        if (this.PlayOffline || Network.Mode != Network.EConnectMode.Online)
          return;
        PartyData partyOfType = instance1.Player.FindPartyOfType(type);
        if (this.mStartingQuest.type != QuestTypes.Tower)
          return;
        TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mStartingQuest.iname);
        this.ExecRequest((WebAPI) new ReqBtlTowerComReq(towerFloor.tower_id, towerFloor.iname, partyOfType, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (this.Error())
        return;
      FlowNode_TowerStartQuest.Json_TowerStartQuest jsonTowerStartQuest = (FlowNode_TowerStartQuest.Json_TowerStartQuest) null;
      WebAPI.JSON_BodyResponse<FlowNode_TowerStartQuest.Json_TowerResume> jsonBodyResponse = (WebAPI.JSON_BodyResponse<FlowNode_TowerStartQuest.Json_TowerResume>) null;
      if (this.mResume)
      {
        jsonBodyResponse = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_TowerStartQuest.Json_TowerResume>>(www.text);
        if (jsonBodyResponse.body.pdeck != null)
          MonoSingleton<GameManager>.Instance.TowerResuponse.Deserialize(jsonBodyResponse.body.pdeck);
        if (jsonBodyResponse.body.edeck != null)
          MonoSingleton<GameManager>.Instance.TowerResuponse.Deserialize(jsonBodyResponse.body.edeck);
        jsonTowerStartQuest = new FlowNode_TowerStartQuest.Json_TowerStartQuest();
        jsonTowerStartQuest.btlinfo = jsonBodyResponse.body.btlinfo;
        jsonTowerStartQuest.btlid = jsonBodyResponse.body.btlid;
        MonoSingleton<GameManager>.Instance.TowerResuponse.round = jsonBodyResponse.body.round;
      }
      WebAPI.JSON_BodyResponse<FlowNode_TowerStartQuest.Json_TowerStartQuest> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_TowerStartQuest.Json_TowerStartQuest>>(www.text);
      if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        if (jsonTowerStartQuest == null)
          jsonTowerStartQuest = jsonObject.body;
        if (this.mResume && jsonBodyResponse == null)
          jsonTowerStartQuest.btlinfo = jsonBodyResponse.body.btlinfo;
        Network.RemoveAPI();
        BattleCore.Json_Battle btl = new BattleCore.Json_Battle();
        btl.btlid = jsonTowerStartQuest.btlid;
        btl.btlinfo = (BattleCore.Json_BtlInfo) jsonTowerStartQuest.btlinfo;
        if (btl.btlinfo != null)
          btl.btlinfo.qid = jsonTowerStartQuest.btlinfo.floor_iname;
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(btl.btlinfo.qid);
        if (quest != null)
        {
          if (jsonObject.body.missions != null)
          {
            for (int index = 0; index < jsonObject.body.missions.Length; ++index)
            {
              bool isClear = jsonObject.body.missions[index] == 1;
              quest.SetMissionFlag(index, isClear);
            }
          }
          if (jsonObject.body.missions_val != null)
          {
            for (int index = 0; index < jsonObject.body.missions_val.Length; ++index)
            {
              int num = jsonObject.body.missions_val[index];
              quest.SetMissionValue(index, num);
            }
          }
        }
        if (this.mResume && AssetManager.UseDLC && AssetDownloader.IsEnableShowSizeBeforeDownloading)
        {
          QuestParam questParam = jsonObject.body.btlinfo.GetQuestParam();
          AssetDownloader.StartConfirmDownloadQuestContentYesNo(jsonObject.body.btlinfo.GetPlayerSideUnits(), (List<ItemData>) null, questParam, (UIUtility.DialogResultEvent) (ok => this.StartCoroutine(this.StartScene(btl))), (UIUtility.DialogResultEvent) (no =>
          {
            this.enabled = false;
            CriticalSection.Leave(CriticalSections.SceneChange);
            this.ActivateOutputLinks(5000);
          }));
        }
        else
          this.StartCoroutine(this.StartScene(btl));
      }
    }

    public bool Error()
    {
      if (!Network.IsError)
        return false;
      if (Network.ErrCode == Network.EErrCode.NotExist_tower)
      {
        if (this.mResume)
        {
          GlobalVars.BtlID.Set(this.btlID);
          CriticalSection.Leave(CriticalSections.SceneChange);
          Network.RequestResult = Network.RequestResults.Back;
          if (Network.IsImmediateMode)
            return true;
          Network.RemoveAPI();
          Network.ResetError();
          this.enabled = false;
          this.ActivateOutputLinks(7);
          return true;
        }
        this.OnFailed();
        return true;
      }
      Network.EErrCode errCode = Network.ErrCode;
      switch (errCode)
      {
        case Network.EErrCode.UnSelectable:
          this.OnBack();
          return true;
        case Network.EErrCode.OutOfDateQuest:
          this.OnBack();
          return true;
        case Network.EErrCode.ChallengeLimit:
          this.OnBack();
          return true;
        default:
          if (errCode != Network.EErrCode.QuestEnd)
          {
            if (errCode != Network.EErrCode.NoBtlInfo)
              return TowerErrorHandle.Error((FlowNode_Network) this);
            this.OnFailed();
            return true;
          }
          this.OnFailed();
          return true;
      }
    }

    private class Json_TowerStartQuest
    {
      public long btlid;
      public FlowNode_TowerStartQuest.Json_TowerBtlInfo btlinfo;
      public int[] missions;
      public int[] missions_val;
    }

    private class Json_TowerResume
    {
      public long btlid;
      public FlowNode_TowerStartQuest.Json_TowerBtlInfo btlinfo;
      public int status;
      public JSON_ReqTowerResuponse.Json_TowerPlayerUnit[] pdeck;
      public JSON_ReqTowerResuponse.Json_TowerEnemyUnit[] edeck;
      public byte round;
      public int[] missions;
      public int[] missions_val;
    }

    private class Json_TowerBtlInfo : BattleCore.Json_BtlInfo
    {
      public int manage_id;
      public string tower_iname;
      public string floor_iname;

      public override RandDeckResult[] GetDeck()
      {
        return this.lot_enemies;
      }

      public override QuestParam GetQuestParam()
      {
        return MonoSingleton<GameManager>.Instance.FindTowerFloor(this.floor_iname)?.GetQuestParam();
      }
    }
  }
}
