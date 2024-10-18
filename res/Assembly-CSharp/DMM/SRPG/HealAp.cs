﻿// Decompiled with JetBrains decompiler
// Type: SRPG.HealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "NotRequiredHeal", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "HealCoin", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(4, "Heal", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "HealOverFlow", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(10, "Click Use", FlowNode.PinTypes.Input, 10)]
  public class HealAp : MonoBehaviour, IFlowInterface
  {
    private List<ItemData> mHealItemList;
    public GameObject mItemParent;
    public GameObject mItemBase;
    public Text LackAp;
    public QuestParam mQuestParam;
    public Slider silder;
    public GameObject QuestInfo;
    public HealApBar bar;
    public Text now_ap;
    public Text max_ap;
    public Text heal_coin_text;
    public Text heal_coin_num;
    public Text pre_ap;
    public Text new_ap;

    public void Refresh(bool is_quest, FlowNode_HealApWindow heal_ap_window)
    {
      PartyWindow2 componentInChildren = ((Component) heal_ap_window).gameObject.GetComponentInChildren<PartyWindow2>();
      this.Refresh(is_quest, componentInChildren);
    }

    public void Refresh(bool is_quest, PartyWindow2 _party_window)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      List<ItemParam> list = MonoSingleton<GameManager>.Instance.MasterParam.Items.Where<ItemParam>((Func<ItemParam, bool>) (x => x.type == EItemType.ApHeal)).ToList<ItemParam>();
      if (list != null)
      {
        for (int index = 0; index < list.Count; ++index)
        {
          ItemParam iparam = list[index];
          ItemData data = player.Items.Find((Predicate<ItemData>) (x => x.ItemID == iparam.iname));
          if (data == null)
          {
            data = new ItemData();
            data.Setup(0L, iparam.iname, 0);
          }
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mItemBase);
          gameObject.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
          ((Selectable) gameObject.GetComponent<Button>()).interactable = data.Num > 0;
          DataSource.Bind<ItemData>(gameObject, data);
          gameObject.transform.SetParent(this.mItemParent.transform, false);
        }
      }
      this.mItemBase.SetActive(false);
      if (is_quest && !string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
        this.mQuestParam = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.QuestInfo.SetActive(this.mQuestParam != null);
      this.silder.maxValue = (float) player.StaminaStockCap;
      this.silder.minValue = 0.0f;
      this.silder.value = (float) player.Stamina;
      this.now_ap.text = player.Stamina.ToString();
      this.max_ap.text = player.StaminaStockCap.ToString();
      if (this.mQuestParam != null)
      {
        int num = this.mQuestParam.RequiredApWithPlayerLv(player.Lv);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) _party_window, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) _party_window.RaidSettings, (UnityEngine.Object) null) && _party_window.MultiRaidNum > 0)
          num *= _party_window.MultiRaidNum;
        this.LackAp.text = LocalizedText.Get("sys.TEXT_APHEAL_LACK_POINT", (object) (num - player.Stamina));
      }
      this.heal_coin_text.text = LocalizedText.Get("sys.SKIPBATTLE_HEAL_NUM", (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaAdd.ToString());
      this.heal_coin_num.text = player.GetStaminaRecoveryCost().ToString();
      if (player.StaminaStockCap <= player.Stamina)
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.STAMINAFULL"), (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 2)));
      this.pre_ap.text = player.Stamina.ToString();
    }

    public void OnSelect(GameObject go)
    {
      DataSource.Bind<ItemData>(((Component) this).gameObject, DataSource.FindDataOfClass<ItemData>(go, (ItemData) null));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public void HealApCoin() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);

    public void OnClickHeal()
    {
      if (this.bar.IsOverFlow)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 5);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.OnClickHeal();
    }
  }
}
