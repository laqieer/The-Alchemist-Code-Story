// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResumeTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tutorial/ResumeTutorial")]
  [FlowNode.Pin(0, "Try", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Next Step", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "DebugEndMovieLoad", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(2, "Start Quest", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "Resume Quest", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "Tutorial Skipped", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "Resume Tower", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(6, "Resume Multi", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(7, "ClearResumeMulti", FlowNode.PinTypes.Input, 7)]
  [FlowNode.Pin(8, "ResumeTowerError", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(11, "DebugMovieLoad", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "ClearTutorial", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "Resume Multi Cancel", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "Resume Versus", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(15, "Resume Versus Cancel", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(16, "ClearResumeVersus", FlowNode.PinTypes.Input, 16)]
  [FlowNode.Pin(17, "GotoHome", FlowNode.PinTypes.Input, 17)]
  [FlowNode.Pin(18, "FgGChainWish", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(19, "Resume MultiTower", FlowNode.PinTypes.Output, 19)]
  [FlowNode.Pin(20, "ClearResumeMultiTower", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(21, "ResumeMultiTower Cancel", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "ResumeNormalQuest Cancel", FlowNode.PinTypes.Input, 22)]
  [FlowNode.Pin(1000, "Download Cancel", FlowNode.PinTypes.Output, 1000)]
  public class FlowNode_ResumeTutorial : FlowNode
  {
    private bool mSkipTutorial;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          GameManager instance = MonoSingleton<GameManager>.Instance;
          ((Behaviour) this).enabled = true;
          if ((long) GlobalVars.BtlID != 0L)
          {
            if ((instance.Player.TutorialFlags & 1L) == 0L)
            {
              this.ActivateOutputLinks(3);
              break;
            }
            QuestTypes questType = GlobalVars.QuestType;
            switch (questType)
            {
              case QuestTypes.Tower:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnTowerResumeAccept), new UIUtility.DialogResultEvent(this.OnTowerResumeCancel));
                return;
              case QuestTypes.VersusFree:
              case QuestTypes.VersusRank:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnVersusAccept), new UIUtility.DialogResultEvent(this.OnVersusResumeCancel));
                return;
              case QuestTypes.MultiTower:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_TOWER_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnMultiTowerAccept), new UIUtility.DialogResultEvent(this.OnMultiTowerResumeCancel));
                return;
              case QuestTypes.MultiGps:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnMultiResumeAccept), new UIUtility.DialogResultEvent(this.OnMultiResumeCancel));
                return;
              case QuestTypes.RankMatch:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnVersusAccept), new UIUtility.DialogResultEvent(this.OnVersusResumeCancel));
                return;
              case QuestTypes.Raid:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnRaidResumeCancel));
                return;
              case QuestTypes.GenesisBoss:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnGenesisBossResumeCancel));
                return;
              case QuestTypes.AdvanceBoss:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnAdvanceBossResumeCancel));
                return;
              case QuestTypes.GuildRaid:
                if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null || !MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
                {
                  this.OnGuildRaidResumeCancel((GameObject) null);
                  return;
                }
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnGuildRaidResumeCancel));
                return;
              case QuestTypes.WorldRaid:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnWorldRaidResumeCancel));
                return;
              default:
                if (questType != QuestTypes.Multi)
                {
                  UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnResumeCancel));
                  return;
                }
                goto case QuestTypes.MultiGps;
            }
          }
          else
          {
            BattleCore.RemoveSuspendData();
            if ((instance.Player.TutorialFlags & 1L) != 0L)
            {
              GlobalVars.IsTutorialEnd = true;
              if (MonoSingleton<GameManager>.Instance.AuthStatus == ReqFgGAuth.eAuthStatus.Synchronized)
              {
                this.LoadStartScene();
                break;
              }
              this.ActivateOutputLinks(18);
              break;
            }
            instance.UpdateTutorialStep();
            if (instance.TutorialStep == 0 && GameUtility.IsDebugBuild)
            {
              if (GlobalVars.DebugIsPlayTutorial || !AssetManager.UseDLC)
              {
                this.PlayTutorial();
                break;
              }
              this.mSkipTutorial = true;
              this.CompleteTutorial();
              break;
            }
            this.PlayTutorial();
            break;
          }
        case 1:
          MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
          this.PlayTutorial();
          break;
        case 7:
          this.ClearMultiResumeData();
          break;
        case 8:
          this.ClearTowerResumeData();
          break;
        case 10:
          this.PlayTutorial();
          break;
        case 16:
          this.ClearVersusResumeData();
          break;
        case 17:
          this.LoadStartScene();
          break;
        case 20:
          this.ClearMultiTowerResumeData();
          break;
        case 22:
          this.ClearResumeData(true);
          break;
      }
    }

    private void OnResumeAccept(GameObject go) => this.ActivateOutputLinks(3);

    private void OnTowerResumeAccept(GameObject go) => this.ActivateOutputLinks(5);

    private void OnMultiResumeAccept(GameObject go) => this.ActivateOutputLinks(6);

    private void OnVersusAccept(GameObject go) => this.ActivateOutputLinks(14);

    private void OnMultiTowerAccept(GameObject go) => this.ActivateOutputLinks(19);

    private void OnResumeCancel(GameObject go) => this.ClearResumeData();

    private void OnTowerResumeCancel(GameObject go) => this.ClearTowerResumeData();

    private void OnMultiResumeCancel(GameObject go) => this.ActivateOutputLinks(13);

    private void OnVersusResumeCancel(GameObject go) => this.ActivateOutputLinks(15);

    private void OnMultiTowerResumeCancel(GameObject go) => this.ActivateOutputLinks(21);

    private void OnRaidResumeCancel(GameObject go) => this.ClearRaidResumeData();

    private void OnGenesisBossResumeCancel(GameObject go) => this.ClearGenesisBossResumeData();

    private void OnAdvanceBossResumeCancel(GameObject go) => this.ClearAdvanceBossResumeData();

    private void OnGuildRaidResumeCancel(GameObject go) => this.ClearGuildRaidResumeData();

    private void OnWorldRaidResumeCancel(GameObject go) => this.ClearWorldRaidResumeData();

    private void OnPlayTutorial(GameObject go) => this.ActivateOutputLinks(11);

    private void OnSkipTutorial(GameObject go)
    {
      this.mSkipTutorial = true;
      this.CompleteTutorial();
    }

    private void PlayTutorial()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      string nextTutorialStep = instance.GetNextTutorialStep();
      if (string.IsNullOrEmpty(nextTutorialStep))
        this.CompleteTutorial();
      else if (instance.FindQuest(nextTutorialStep) == null)
      {
        this.StartCoroutine(this.DownloadContentAsync(nextTutorialStep));
      }
      else
      {
        GlobalVars.SelectedQuestID = nextTutorialStep;
        GlobalVars.SelectedFriendID = string.Empty;
        this.ActivateOutputLinks(2);
      }
    }

    private void CompleteTutorial()
    {
      MonoSingleton<GameManager>.Instance.UpdateTutorialFlags(1L);
      this.StartCoroutine(this.WaitCompleteTutorialAsync());
    }

    [DebuggerHidden]
    private IEnumerator WaitCompleteTutorialAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResumeTutorial.\u003CWaitCompleteTutorialAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void ClearResumeData(bool is_rehash = false)
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqBtlComEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Cancel, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd), BtlEndTypes.com, is_rehash: is_rehash));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearTowerResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqTowerBtlComEnd((long) GlobalVars.BtlID, (Unit[]) null, (Unit[]) null, 0, 0, (byte) 0, BtlResultTypes.Cancel, (RandDeckResult[]) null, new Network.ResponseCallback(this.OnBtlComEnd)));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearMultiResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqBtlComEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Cancel, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd), BtlEndTypes.multi));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearVersusResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqVersusEnd((long) GlobalVars.BtlID, BtlResultTypes.Cancel, (string) null, (string) null, 0U, (int[]) null, (int[]) null, 0, 0, 0, 0, (int[]) null, new Network.ResponseCallback(this.OnBtlComEnd), GlobalVars.SelectedMultiPlayVersusType));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearMultiTowerResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqBtlMultiTwEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Retire, (int[]) null, (string[]) null, (string[]) null, new Network.ResponseCallback(this.OnBtlComEnd)));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearRaidResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqRaidBtlEnd((long) GlobalVars.BtlID, BtlResultTypes.Retire, 0, (List<Unit>) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd), trophyProgs: string.Empty, bingoProgs: string.Empty));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearGenesisBossResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqGenesisBossBtlEnd((long) GlobalVars.BtlID, BtlResultTypes.Retire, (List<Unit>) null, 0, (int[]) null, (int[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd)));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearAdvanceBossResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqAdvanceBossBtlEnd((long) GlobalVars.BtlID, BtlResultTypes.Retire, (List<Unit>) null, 0, (int[]) null, (int[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd)));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearGuildRaidResumeData()
    {
      BattleCore.RemoveSuspendData();
      int gid = 0;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild != null && MonoSingleton<GameManager>.Instance.Player.PlayerGuild.IsJoined)
        gid = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid;
      Network.RequestAPI((WebAPI) new ReqGuildRaidBtlEnd(gid, (long) GlobalVars.BtlID, BtlResultTypes.Retire, 0, 0, (List<Unit>) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd)));
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearWorldRaidResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqWorldRaidBtlEnd((long) GlobalVars.BtlID, BtlResultTypes.Cancel, string.Empty, (int[]) null, (List<Unit>) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd)));
      GlobalVars.BtlID.Set(0L);
    }

    private void OnBtlComEnd(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www) || GlobalVars.QuestType == QuestTypes.Tower && TowerErrorHandle.Error())
        return;
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        switch (errCode)
        {
          case Network.EErrCode.GuildRaid_OutOfPeriod:
          case Network.EErrCode.GuildRaid_EnableTimeOutOfPeriod:
          case Network.EErrCode.GuildRaid_AlreadyBeat:
          case Network.EErrCode.GuildRaid_CanNotChallengeByThereIsNoBoss:
            Network.ResetError();
            Network.RemoveAPI();
            BattleCore.RemoveSuspendData();
            UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.LoadStartScene()), systemModal: true, systemModalPriority: 10);
            break;
          default:
            if (errCode != Network.EErrCode.QuestEnd && errCode != Network.EErrCode.ColoNoBattle)
            {
              if (errCode == Network.EErrCode.WorldRaid_OutOfPeriod)
              {
                Network.ResetError();
                Network.RemoveAPI();
                BattleCore.RemoveSuspendData();
                UIUtility.SystemMessage(Network.ErrMsg, (UIUtility.DialogResultEvent) (go => this.LoadStartScene()), systemModal: true, systemModalPriority: 10);
                break;
              }
              FlowNode_Network.Retry();
              break;
            }
            FlowNode_Network.Failed();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            if (jsonObject.body.mails != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.mails);
            if (jsonObject.body.trophyprogs != null)
              MonoSingleton<GameManager>.Instance.Player.TrophyData.RegistTrophyStateDictByProgExtra(jsonObject.body.trophyprogs);
            if (jsonObject.body.bingoprogs != null)
              MonoSingleton<GameManager>.Instance.Player.TrophyData.RegistTrophyStateDictByProgExtra(jsonObject.body.bingoprogs);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          Network.RemoveAPI();
          this.LoadStartScene();
        }
      }
    }

    private void LoadStartScene(bool fromTutorial = false)
    {
      Debug.Log((object) ("LoadStartScene : fromTutorial = " + (object) fromTutorial));
      string empty = string.Empty;
      this.StartCoroutine(this.LoadSceneAsync(!fromTutorial || !AssetDownloader.IsEnableShowSizeBeforeDownloading(false) ? (!AssetManager.UseDLC ? "TownDummy" : "Home") : "TownDummy"));
    }

    [DebuggerHidden]
    private IEnumerator DownloadContentAsync(string sceneName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResumeTutorial.\u003CDownloadContentAsync\u003Ec__Iterator1()
      {
        sceneName = sceneName,
        \u0024this = this
      };
    }

    private void DownloadApproved(string sceneName)
    {
      this.StartCoroutine(this.LoadSceneAsync(sceneName));
    }

    private void DownloadNotApproved() => this.ActivateOutputLinks(1000);

    [DebuggerHidden]
    private IEnumerator LoadSceneAsync(string sceneName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResumeTutorial.\u003CLoadSceneAsync\u003Ec__Iterator2()
      {
        sceneName = sceneName,
        \u0024this = this
      };
    }
  }
}
