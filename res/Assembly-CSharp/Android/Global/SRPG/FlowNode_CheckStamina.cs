﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckStamina
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/CheckStamina", 32741)]
  [FlowNode.Pin(101, "Restore", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(102, "IAP", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(100, "Pass", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_CheckStamina : FlowNode
  {
    public const int PINID_IN = 0;
    public const int PINID_PASS = 100;
    public const int PINID_RESTORE = 101;
    public const int PINID_IAP = 102;
    public string DebugQuestID;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam selectedQuest = this.SelectedQuest;
      if (selectedQuest == null)
      {
        DebugUtility.LogError("QuestNotFound \"" + GlobalVars.SelectedQuestID + "\" ");
      }
      else
      {
        int num = selectedQuest.RequiredApWithPlayerLv(instance.Player.Lv, true);
        if (GlobalVars.RaidNum > 0)
          num *= GlobalVars.RaidNum;
        if (num <= instance.Player.Stamina)
          this.ActivateOutputLinks(100);
        else
          UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.CONFIRM_POINT"), (object) this.RestoreCost), new UIUtility.DialogResultEvent(this.OnRestoreStamina), new UIUtility.DialogResultEvent(this.OnCancel), (GameObject) null, false, -1, (string) null, (string) null);
      }
    }

    private int RestoreCost
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.GetStaminaRecoveryCost(false);
      }
    }

    private QuestParam SelectedQuest
    {
      get
      {
        if (!string.IsNullOrEmpty(this.DebugQuestID))
          return MonoSingleton<GameManager>.Instance.FindQuest(this.DebugQuestID);
        return MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      }
    }

    private void OnRestoreStamina(GameObject go)
    {
      if (this.RestoreCost <= MonoSingleton<GameManager>.Instance.Player.Coin)
        this.ActivateOutputLinks(101);
      else
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.OUT_OF_STAMINA_BUYCOIN")), new UIUtility.DialogResultEvent(this.OnBuyCoin), new UIUtility.DialogResultEvent(this.OnCancel), (GameObject) null, false, -1, (string) null, (string) null);
    }

    private void OnBuyCoin(GameObject go)
    {
      this.ActivateOutputLinks(102);
    }

    private void OnCancel(GameObject go)
    {
      GlobalVars.RaidNum = 0;
    }
  }
}
