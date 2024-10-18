// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EnhanceEquip
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
  [FlowNode.NodeType("System/Unit/EnhanceEquip", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "費用が足りない", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_EnhanceEquip : FlowNode_Network
  {
    private int mRankupCount;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      UnitData unitDataByUniqueId = instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      JobData job = unitDataByUniqueId.Jobs[(int) GlobalVars.SelectedUnitJobIndex];
      int selectedEquipmentSlot = (int) GlobalVars.SelectedEquipmentSlot;
      EquipData selectedEquipData = GlobalVars.SelectedEquipData;
      int num1 = 0;
      int exp = 0;
      List<EnhanceMaterial> enhanceMaterials = GlobalVars.SelectedEnhanceMaterials;
      if (enhanceMaterials == null || enhanceMaterials.Count < 1)
        return;
      for (int index = 0; index < enhanceMaterials.Count; ++index)
      {
        EnhanceMaterial enhanceMaterial = enhanceMaterials[index];
        ItemData itemData = enhanceMaterial.item;
        num1 += itemData.Param.enhace_cost * selectedEquipData.GetEnhanceCostScale() / 100 * enhanceMaterial.num;
        exp += itemData.Param.enhace_point * enhanceMaterial.num;
      }
      if (num1 > player.Gold)
      {
        this.ActivateOutputLinks(2);
      }
      else
      {
        this.mRankupCount = 0;
        int current = selectedEquipData.Exp + exp;
        int num2 = selectedEquipData.CalcRankFromExp(current) - selectedEquipData.CalcRankFromExp(selectedEquipData.Exp);
        if (num2 > 0)
          this.mRankupCount += num2;
        selectedEquipData.GainExp(exp);
        unitDataByUniqueId?.CalcStatus();
        for (int index = 0; index < enhanceMaterials.Count; ++index)
        {
          EnhanceMaterial enhanceMaterial = enhanceMaterials[index];
          enhanceMaterial.item.Used(enhanceMaterial.num);
        }
        player.GainGold(-num1);
        if (Network.Mode == Network.EConnectMode.Online)
        {
          Dictionary<string, int> usedItems = new Dictionary<string, int>();
          for (int index = 0; index < enhanceMaterials.Count; ++index)
          {
            EnhanceMaterial enhanceMaterial = enhanceMaterials[index];
            ItemData itemData = enhanceMaterial.item;
            if (enhanceMaterial.num >= 1)
            {
              if (!usedItems.ContainsKey(itemData.ItemID))
                usedItems[itemData.ItemID] = 0;
              Dictionary<string, int> dictionary;
              string itemId;
              (dictionary = usedItems)[itemId = itemData.ItemID] = dictionary[itemId] + enhanceMaterial.num;
            }
          }
          this.ExecRequest((WebAPI) new ReqEquipExpAdd(job.UniqueID, selectedEquipmentSlot, usedItems, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).enabled = true;
        }
        else
          this.Success();
      }
    }

    private void Success()
    {
      MonoSingleton<GameManager>.Instance.Player.OnSoubiPowerUp(this.mRankupCount);
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobEnforceEquip:
          case Network.EErrCode.NoEquipEnforce:
            this.OnFailed();
            break;
          case Network.EErrCode.ForceMax:
          case Network.EErrCode.MaterialShort:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
