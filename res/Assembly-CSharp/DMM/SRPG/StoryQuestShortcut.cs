﻿// Decompiled with JetBrains decompiler
// Type: SRPG.StoryQuestShortcut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "ノーマルクエスト", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "キークエスト", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "塔クエスト", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "イベントページへ", FlowNode.PinTypes.Output, 100)]
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
      if (Object.op_Inequality((Object) this.KeyQuestOpenEffect, (Object) null))
        this.KeyQuestOpenEffect.SetActive(flag1);
      if (Object.op_Inequality((Object) this.KeyQuestButton, (Object) null))
      {
        ((Component) this.KeyQuestButton).gameObject.SetActive(true);
        ((Selectable) this.KeyQuestButton).interactable = flag2;
      }
      if (!Object.op_Inequality((Object) this.TowerQuestButton, (Object) null))
        return;
      LevelLock component = ((Component) this.TowerQuestButton).GetComponent<LevelLock>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.UpdateLockState();
        if (!((Selectable) this.TowerQuestButton).interactable)
          return;
        ((Selectable) this.TowerQuestButton).interactable = this.IsOpendTower();
      }
      else
        ((Selectable) this.TowerQuestButton).interactable = this.IsOpendTower();
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
  }
}
