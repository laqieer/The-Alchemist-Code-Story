﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckStamina
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Check/CheckStamina", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Pass", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(101, "Restore", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(102, "IAP", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(103, "HEALAP", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_CheckStamina : FlowNode
  {
    public const int PINID_IN = 0;
    public const int PINID_PASS = 100;
    public const int PINID_RESTORE = 101;
    public const int PINID_IAP = 102;
    public const int PINID_HEALAP = 103;
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
        int num = selectedQuest.RequiredApWithPlayerLv(instance.Player.Lv);
        if (GlobalVars.RaidNum > 0)
          num *= GlobalVars.RaidNum;
        if (num <= instance.Player.Stamina)
          this.ActivateOutputLinks(100);
        else
          this.ActivateOutputLinks(103);
      }
    }

    private int RestoreCost => MonoSingleton<GameManager>.Instance.Player.GetStaminaRecoveryCost();

    private QuestParam SelectedQuest
    {
      get
      {
        return !string.IsNullOrEmpty(this.DebugQuestID) ? MonoSingleton<GameManager>.Instance.FindQuest(this.DebugQuestID) : MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      }
    }

    private void OnRestoreStamina(GameObject go)
    {
      if (this.RestoreCost <= MonoSingleton<GameManager>.Instance.Player.Coin)
        this.ActivateOutputLinks(101);
      else
        UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.OUT_OF_STAMINA_BUYCOIN")), new UIUtility.DialogResultEvent(this.OnBuyCoin), new UIUtility.DialogResultEvent(this.OnCancel));
    }

    private void OnBuyCoin(GameObject go) => this.ActivateOutputLinks(102);

    private void OnCancel(GameObject go) => GlobalVars.RaidNum = 0;
  }
}
