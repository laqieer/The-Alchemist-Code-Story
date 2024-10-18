﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResumeTutorial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
          this.enabled = true;
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
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnTowerResumeAccept), new UIUtility.DialogResultEvent(this.OnTowerResumeCancel), (GameObject) null, false, -1, (string) null, (string) null);
                return;
              case QuestTypes.VersusFree:
              case QuestTypes.VersusRank:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnVersusAccept), new UIUtility.DialogResultEvent(this.OnVersusResumeCancel), (GameObject) null, false, -1, (string) null, (string) null);
                return;
              case QuestTypes.MultiTower:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_TOWER_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnMultiTowerAccept), new UIUtility.DialogResultEvent(this.OnMultiTowerResumeCancel), (GameObject) null, false, -1, (string) null, (string) null);
                return;
              case QuestTypes.MultiGps:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnMultiResumeAccept), new UIUtility.DialogResultEvent(this.OnMultiResumeCancel), (GameObject) null, false, -1, (string) null, (string) null);
                return;
              case QuestTypes.RankMatch:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnVersusAccept), new UIUtility.DialogResultEvent(this.OnVersusResumeCancel), (GameObject) null, false, -1, (string) null, (string) null);
                return;
              case QuestTypes.Raid:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnRaidResumeCancel), (GameObject) null, false, -1, (string) null, (string) null);
                return;
              case QuestTypes.GenesisBoss:
                UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnGenesisBossResumeCancel), (GameObject) null, false, -1, (string) null, (string) null);
                return;
              default:
                if (questType != QuestTypes.Multi)
                {
                  UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnResumeCancel), (GameObject) null, false, -1, (string) null, (string) null);
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

    private void OnResumeAccept(GameObject go)
    {
      this.ActivateOutputLinks(3);
    }

    private void OnTowerResumeAccept(GameObject go)
    {
      this.ActivateOutputLinks(5);
    }

    private void OnMultiResumeAccept(GameObject go)
    {
      this.ActivateOutputLinks(6);
    }

    private void OnVersusAccept(GameObject go)
    {
      this.ActivateOutputLinks(14);
    }

    private void OnMultiTowerAccept(GameObject go)
    {
      this.ActivateOutputLinks(19);
    }

    private void OnResumeCancel(GameObject go)
    {
      this.ClearResumeData(false);
    }

    private void OnTowerResumeCancel(GameObject go)
    {
      this.ClearTowerResumeData();
    }

    private void OnMultiResumeCancel(GameObject go)
    {
      this.ActivateOutputLinks(13);
    }

    private void OnVersusResumeCancel(GameObject go)
    {
      this.ActivateOutputLinks(15);
    }

    private void OnMultiTowerResumeCancel(GameObject go)
    {
      this.ActivateOutputLinks(21);
    }

    private void OnRaidResumeCancel(GameObject go)
    {
      this.ClearRaidResumeData();
    }

    private void OnGenesisBossResumeCancel(GameObject go)
    {
      this.ClearGenesisBossResumeData();
    }

    private void OnPlayTutorial(GameObject go)
    {
      this.ActivateOutputLinks(11);
    }

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
      return (IEnumerator) new FlowNode_ResumeTutorial.\u003CWaitCompleteTutorialAsync\u003Ec__Iterator0() { \u0024this = this };
    }

    private void ClearResumeData(bool is_rehash = false)
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqBtlComEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Cancel, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd), BtlEndTypes.com, (string) null, (string) null, 0, (string) null, is_rehash, new bool?(), (BattleCore.Record) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearTowerResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqTowerBtlComEnd((long) GlobalVars.BtlID, (Unit[]) null, (Unit[]) null, 0, 0, (byte) 0, BtlResultTypes.Cancel, (RandDeckResult[]) null, new Network.ResponseCallback(this.OnBtlComEnd), (string) null, (string) null, (int[]) null, (int[]) null, (int[]) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearMultiResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqBtlComEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Cancel, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd), BtlEndTypes.multi, (string) null, (string) null, 0, (string) null, false, new bool?(), (BattleCore.Record) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearVersusResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqVersusEnd((long) GlobalVars.BtlID, BtlResultTypes.Cancel, (string) null, (string) null, 0U, (int[]) null, (int[]) null, 0, 0, 0, 0, (int[]) null, new Network.ResponseCallback(this.OnBtlComEnd), GlobalVars.SelectedMultiPlayVersusType, (string) null, (string) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearMultiTowerResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqBtlMultiTwEnd((long) GlobalVars.BtlID, 0, BtlResultTypes.Retire, (int[]) null, (string[]) null, (string[]) null, new Network.ResponseCallback(this.OnBtlComEnd), (string) null, (string) null), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearRaidResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqRaidBtlEnd((long) GlobalVars.BtlID, BtlResultTypes.Retire, 0, (List<Unit>) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd)), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void ClearGenesisBossResumeData()
    {
      BattleCore.RemoveSuspendData();
      Network.RequestAPI((WebAPI) new ReqGenesisBossBtlEnd((long) GlobalVars.BtlID, BtlResultTypes.Retire, (List<Unit>) null, 0, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.OnBtlComEnd)), false);
      GlobalVars.BtlID.Set(0L);
    }

    private void OnBtlComEnd(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www) || GlobalVars.QuestType == QuestTypes.Tower && TowerErrorHandle.Error((FlowNode_Network) null))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestEnd:
          case Network.EErrCode.ColoNoBattle:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
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

    private void LoadStartScene()
    {
      this.StartCoroutine(this.LoadSceneAsync(!AssetManager.UseDLC ? "TownDummy" : "Home"));
    }

    [DebuggerHidden]
    private IEnumerator DownloadContentAsync(string sceneName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResumeTutorial.\u003CDownloadContentAsync\u003Ec__Iterator1() { sceneName = sceneName, \u0024this = this };
    }

    private void DownloadApproved(string sceneName)
    {
      this.StartCoroutine(this.LoadSceneAsync(sceneName));
    }

    private void DownloadNotApproved()
    {
      this.ActivateOutputLinks(1000);
    }

    [DebuggerHidden]
    private IEnumerator LoadSceneAsync(string sceneName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResumeTutorial.\u003CLoadSceneAsync\u003Ec__Iterator2() { sceneName = sceneName, \u0024this = this };
    }
  }
}
