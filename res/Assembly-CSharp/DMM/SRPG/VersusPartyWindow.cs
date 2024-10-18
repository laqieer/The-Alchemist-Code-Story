// Decompiled with JetBrains decompiler
// Type: SRPG.VersusPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10010, "ユニット配置ボタン押下", FlowNode.PinTypes.Input, 10010)]
  [FlowNode.Pin(10100, "ユニット配置 成功", FlowNode.PinTypes.Output, 10100)]
  [FlowNode.Pin(10200, "ユニット配置 失敗", FlowNode.PinTypes.Output, 10200)]
  public class VersusPartyWindow : PartyWindow2
  {
    public override void Activated(int pinID)
    {
      base.Activated(pinID);
      if (pinID != 10010)
        return;
      this.OnClickEdit();
    }

    protected override int AvailableMainMemberSlots => this.CurrentParty.PartyData.MAX_UNIT;

    protected override void OnItemSlotsChange()
    {
    }

    protected override void SetItemSlot(int slotIndex, ItemData item)
    {
      this.mCurrentItems[slotIndex] = item;
    }

    private void Update()
    {
    }

    protected override void PostForwardPressed()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PartyData partyData = this.CurrentParty.PartyData;
      List<int> intList = new List<int>();
      for (int index = 0; index < partyData.MAX_UNIT; ++index)
      {
        if ((this.CurrentParty.Units[index] == null ? 0L : this.CurrentParty.Units[index].UniqueID) != 0L)
        {
          string str = partyData.PartyType != PlayerPartyTypes.RankMatch ? PlayerPrefsUtility.VERSUS_ID_KEY : PlayerPrefsUtility.RANKMATCH_ID_KEY;
          int idx = player.GetVersusPlacement(str + (object) index);
          if (intList.FindIndex((Predicate<int>) (d => d == idx)) != -1)
          {
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.MULTI_VERSUS_SAME_POS"), (UIUtility.DialogResultEvent) (dialog => { }));
            return;
          }
          intList.Add(idx);
        }
      }
      base.PostForwardPressed();
    }

    private void OnClickEdit()
    {
      if (FlowNode_MultiPlayRoomIsDraft.IsDraftMultiPlayRoom())
        this.SaveAndActivatePin(10100, 10200);
      else if (AssetManager.UseDLC && AssetDownloader.IsEnableShowSizeBeforeDownloading())
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        AssetDownloader.StartConfirmDownloadQuestContentYesNo(this.GetBattleEntryUnits(), (List<ItemData>) null, quest, (UIUtility.DialogResultEvent) (ok => this.SaveAndActivatePin(10100, 10200)), (UIUtility.DialogResultEvent) (no => FlowNode_GameObject.ActivateOutputLinks((Component) this, 10200)));
      }
      else
        this.SaveAndActivatePin(10100, 10200);
    }
  }
}
