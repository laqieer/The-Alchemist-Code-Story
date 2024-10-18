// Decompiled with JetBrains decompiler
// Type: SRPG.SceneReplay
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
  [FlowNode.Pin(0, "Skip", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Skip生成完了", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "次のシナリオ再生", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(101, "次のシナリオ再生する？", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "シナリオ終了", FlowNode.PinTypes.Output, 111)]
  public class SceneReplay : Scene, IFlowInterface
  {
    public static readonly string QUEST_TEXTTABLE = "quest";
    private const string CREATE_SKIP_CANVAS = "CREATE_SKIP_CANVAS";
    private const string DESTROY_SKIP_CANVAS = "DESTROY_SKIP_CANVAS";
    private const string ENABLE_SKIP_BUTTON = "ENABLE_SKIP";
    private const string DISABLE_SKIP_BUTTON = "DISABLE_SKIP";
    private const string SCENE_EXIT = "EXIT";
    private const string SCENE_NEXT = "NEXT";
    private const int PIN_ID_SKIP = 0;
    private const int PIN_ID_SKIP_CANVAS_CREATED = 1;
    private const int PIN_ID_CONTINUE = 11;
    private const int PIN_ID_OUT_DIALOG_CONTINUE = 101;
    private const int PIN_ID_OUT_END = 111;
    private StateMachine<SceneReplay> mState;
    private bool mStartCalled;
    private QuestParam mCurrentQuest;
    private QuestParam mNextQuest;
    private BattleCore mBattle;
    private IEnumerator<string> mEventNames;
    public bool mAutoReplay;
    private bool skip;
    private bool canvasCreating;
    private bool mSceneExiting;
    private List<string> mPlayableScenario;
    private QuestParam[] mChapterQuests;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.skip = true;
          break;
        case 1:
          this.canvasCreating = false;
          break;
        case 11:
          if (this.mNextQuest != null)
          {
            GlobalVars.ReplaySelectedChapter.Set(this.mNextQuest.ChapterID);
            GlobalVars.ReplaySelectedSection.Set(this.mNextQuest.world);
            GlobalVars.ReplaySelectedQuestID.Set(this.mNextQuest.iname);
            ProgressWindow.OpenQuestLoadScreen(this.mNextQuest);
            this.StartQuest(this.mNextQuest.iname);
            this.mNextQuest = (QuestParam) null;
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
          break;
      }
    }

    private void Start()
    {
      LocalizedText.LoadTable(SceneReplay.QUEST_TEXTTABLE);
      FadeController.Instance.ResetSceneFade(0.0f);
      this.mState = new StateMachine<SceneReplay>(this);
      this.mStartCalled = true;
      this.mBattle = new BattleCore();
      this.mAutoReplay = false;
    }

    private void OnDestroy()
    {
      LocalizedText.UnloadTable(SceneReplay.QUEST_TEXTTABLE);
      if (this.mBattle == null)
        return;
      this.mBattle.Release();
      this.mBattle = (BattleCore) null;
    }

    public void RemoveLog()
    {
      DebugUtility.Log("RemoveLog(): " + (object) this.mBattle.Logs.Peek.GetType());
      this.mBattle.Logs.RemoveLog();
    }

    private void Update()
    {
      if (this.mState == null)
        return;
      this.mState.Update();
    }

    public void GotoState<StateType>() where StateType : State<SceneReplay>, new()
    {
      this.mState.GotoState<StateType>();
    }

    public bool IsInState<StateType>() where StateType : State<SceneReplay>
    {
      return this.mState.IsInState<StateType>();
    }

    public void StartQuest(string questID)
    {
      FadeController.Instance.ResetSceneFade(0.0f);
      this.StartCoroutine(this.StartQuestAsync(questID));
    }

    [DebuggerHidden]
    private IEnumerator StartQuestAsync(string questID)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneReplay.\u003CStartQuestAsync\u003Ec__Iterator0()
      {
        questID = questID,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator DownloadQuestAsync(QuestParam quest)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneReplay.\u003CDownloadQuestAsync\u003Ec__Iterator1()
      {
        quest = quest,
        \u0024this = this
      };
    }

    private void CreateSkipCanvas()
    {
      this.canvasCreating = true;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "CREATE_SKIP_CANVAS");
    }

    private void DestroySkipCanvas()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "DESTROY_SKIP_CANVAS");
    }

    private void EnableSkipButton()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "ENABLE_SKIP");
      EventScript.ActiveButtons(true);
    }

    private void DisableSkipButton()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "DISABLE_SKIP");
      EventScript.ActiveButtons(false);
    }

    private void ExitScene()
    {
      if (this.mSceneExiting)
        return;
      this.mSceneExiting = true;
      this.mNextQuest = this.GetNextSceneQuest(this.mCurrentQuest);
      if (this.mNextQuest != null)
      {
        GlobalVars.ReplaySelectedNextQuestID.Set(this.mNextQuest.iname);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }

    private QuestParam GetNextSceneQuest(QuestParam current)
    {
      QuestParam temp = current;
      if (this.mPlayableScenario == null)
        this.mPlayableScenario = ReplayQuestListManager.GetPlayableReplayScenario();
      if (this.mPlayableScenario == null || this.mPlayableScenario.Count <= 0)
        return (QuestParam) null;
      if (this.mChapterQuests == null)
        this.mChapterQuests = Array.FindAll<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, (Predicate<QuestParam>) (q => q.ChapterID == current.ChapterID));
      if (this.mChapterQuests == null || this.mChapterQuests.Length <= 0)
        return (QuestParam) null;
      do
      {
        temp = Array.Find<QuestParam>(this.mChapterQuests, (Predicate<QuestParam>) (quest => quest.cond_quests != null && Array.FindIndex<string>(quest.cond_quests, (Predicate<string>) (cond => cond == temp.iname)) >= 0));
        if (temp == null)
          goto label_11;
      }
      while ((string.IsNullOrEmpty(temp.event_start) || !this.mPlayableScenario.Contains(temp.event_start)) && (string.IsNullOrEmpty(temp.event_clear) || !this.mPlayableScenario.Contains(temp.event_clear)));
      return temp;
label_11:
      return (QuestParam) null;
    }

    private class State_PreReplay : State<SceneReplay>
    {
      public override void Begin(SceneReplay self)
      {
      }

      public override void Update(SceneReplay self)
      {
        if (self.mEventNames.MoveNext())
          self.GotoState<SceneReplay.State_Replay>();
        else
          self.GotoState<SceneReplay.State_Exit>();
      }
    }

    private class State_Replay : State<SceneReplay>
    {
      private string eventName;
      private string path;
      private bool is3DEvent;

      public override void Begin(SceneReplay self)
      {
        this.eventName = self.mEventNames.Current;
        this.path = "Events/" + this.eventName;
        if (!string.IsNullOrEmpty(this.eventName) && this.eventName.EndsWith("_3d"))
          this.is3DEvent = true;
        self.StartCoroutine(this.Exec());
      }

      [DebuggerHidden]
      private IEnumerator Exec()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneReplay.State_Replay.\u003CExec\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }
    }

    private class State_PostReplay : State<SceneReplay>
    {
      private AsyncOperation mAsyncOp;

      public override void Begin(SceneReplay self)
      {
        this.mAsyncOp = AssetManager.UnloadUnusedAssets();
      }

      public override void Update(SceneReplay self)
      {
        if (!this.mAsyncOp.isDone)
          return;
        if (self.mEventNames.MoveNext())
          self.GotoState<SceneReplay.State_Replay>();
        else
          self.GotoState<SceneReplay.State_Exit>();
      }
    }

    private class State_Exit : State<SceneReplay>
    {
      public override void Begin(SceneReplay self) => self.ExitScene();
    }
  }
}
