﻿// Decompiled with JetBrains decompiler
// Type: SRPG.StoryQuestShortcut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "イベントページへ", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Go To Day Training Page", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(0, "ノーマルクエスト", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "キークエスト", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "塔クエスト", FlowNode.PinTypes.Input, 2)]
  public class StoryQuestShortcut : MonoBehaviour, IFlowInterface
  {
    public Button EventQuestButton;
    public Button KeyQuestButton;
    public Button TowerQuestButton;
    public GameObject KeyQuestOpenEffect;

    private void Start()
    {
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      bool flag1 = false;
      bool flag2 = false;
      if (chapters != null)
      {
        long serverTime = Network.GetServerTime();
        for (int index = 0; index < chapters.Length; ++index)
        {
          if (chapters[index].IsKeyQuest())
          {
            if (chapters[index].IsDateUnlock(serverTime))
              flag2 = true;
            if (chapters[index].IsKeyUnlock(serverTime))
              flag1 = true;
          }
        }
      }
      if ((UnityEngine.Object) this.KeyQuestOpenEffect != (UnityEngine.Object) null)
        this.KeyQuestOpenEffect.SetActive(flag1);
      if ((UnityEngine.Object) this.KeyQuestButton != (UnityEngine.Object) null)
      {
        this.KeyQuestButton.gameObject.SetActive(true);
        this.KeyQuestButton.interactable = flag2;
      }
      if (!((UnityEngine.Object) this.TowerQuestButton != (UnityEngine.Object) null))
        return;
      this.TowerQuestButton.interactable = this.IsOpendTower();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
          break;
        case 1:
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
          break;
        case 2:
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
          break;
      }
      if (this.IsBeginnerQuestIncomplete())
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    public bool IsOpendTower()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      for (int index1 = 0; index1 < instance.Towers.Length; ++index1)
      {
        TowerParam tower = instance.Towers[index1];
        for (int index2 = 0; index2 < availableQuests.Length; ++index2)
        {
          if (availableQuests[index2].type == QuestTypes.Tower && !(availableQuests[index2].iname != tower.iname) && availableQuests[index2].IsDateUnlock(serverTime))
            return true;
        }
      }
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].IsMultiTower && availableQuests[index].IsDateUnlock(serverTime))
          return true;
      }
      return false;
    }

    private bool IsBeginnerQuestIncomplete()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      FixParam fixParam = instance.MasterParam.FixParam;
      if (instance.Player.Lv < (int) fixParam.NonBeginnerLevel && instance.Player.IsBeginnerEventQuestLocked())
      {
        foreach (QuestParam quest in MonoSingleton<GameManager>.Instance.Quests)
        {
          if (quest.iname.Contains("QE_EV_BEGINNER") && quest.state != QuestStates.Cleared)
            return true;
        }
      }
      return false;
    }
  }
}