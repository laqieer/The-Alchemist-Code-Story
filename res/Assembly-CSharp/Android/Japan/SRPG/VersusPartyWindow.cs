// Decompiled with JetBrains decompiler
// Type: SRPG.VersusPartyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(7, "ユニット配置へ", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(8, "同一グリッド", FlowNode.PinTypes.Output, 8)]
  public class VersusPartyWindow : PartyWindow2
  {
    protected override int AvailableMainMemberSlots
    {
      get
      {
        return this.mCurrentParty.PartyData.MAX_UNIT;
      }
    }

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
      PartyData partyData = this.mCurrentParty.PartyData;
      List<int> intList = new List<int>();
      for (int index = 0; index < partyData.MAX_UNIT; ++index)
      {
        if ((this.mCurrentParty.Units[index] == null ? 0L : this.mCurrentParty.Units[index].UniqueID) != 0L)
        {
          string str = partyData.PartyType != PlayerPartyTypes.RankMatch ? PlayerPrefsUtility.VERSUS_ID_KEY : PlayerPrefsUtility.RANKMATCH_ID_KEY;
          int idx = player.GetVersusPlacement(str + (object) index);
          if (intList.FindIndex((Predicate<int>) (d => d == idx)) != -1)
          {
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.MULTI_VERSUS_SAME_POS"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
            return;
          }
          intList.Add(idx);
        }
      }
      base.PostForwardPressed();
    }

    public void OnClickEdit()
    {
      if (FlowNode_MultiPlayRoomIsDraft.IsDraftMultiPlayRoom())
        this.SaveAndActivatePin(7);
      else if (AssetManager.UseDLC && AssetDownloader.IsEnableShowSizeBeforeDownloading)
        AssetDownloader.StartConfirmDownloadQuestContentYesNo(this.GetBattleEntryUnits(), (List<ItemData>) null, MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID), (UIUtility.DialogResultEvent) (ok => this.SaveAndActivatePin(7)), (UIUtility.DialogResultEvent) (no => {}));
      else
        this.SaveAndActivatePin(7);
    }
  }
}
