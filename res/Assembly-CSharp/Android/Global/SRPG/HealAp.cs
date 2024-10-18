// Decompiled with JetBrains decompiler
// Type: SRPG.HealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(4, "Heal", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(3, "HealCoin", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "NotRequiredHeal", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(5, "HealOverFlow", FlowNode.PinTypes.Output, 5)]
  public class HealAp : MonoBehaviour
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

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void Refresh(bool is_quest, FlowNode_HealApWindow heal_ap_window)
    {
      this.mHealItemList = MonoSingleton<GameManager>.Instance.Player.Items.Where<ItemData>((Func<ItemData, bool>) (x => x.ItemType == EItemType.ApHeal)).ToList<ItemData>();
      for (int index = 0; index < this.mHealItemList.Count; ++index)
      {
        if (this.mHealItemList[index].Num > 0)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mItemBase);
          gameObject.GetComponent<ListItemEvents>().OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
          DataSource.Bind<ItemData>(gameObject, this.mHealItemList[index]);
          gameObject.transform.SetParent(this.mItemParent.transform, false);
        }
      }
      this.mItemBase.SetActive(false);
      if (is_quest && !string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
        this.mQuestParam = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.QuestInfo.SetActive(this.mQuestParam != null);
      this.silder.maxValue = (float) MonoSingleton<GameManager>.Instance.Player.StaminaStockCap;
      this.silder.minValue = 0.0f;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.silder.value = (float) player.Stamina;
      this.now_ap.text = player.Stamina.ToString();
      this.max_ap.text = player.StaminaStockCap.ToString();
      if (this.mQuestParam != null)
      {
        PartyWindow2 componentInChildren = heal_ap_window.gameObject.GetComponentInChildren<PartyWindow2>();
        int num = this.mQuestParam.RequiredApWithPlayerLv(player.Lv, true);
        if ((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null && (UnityEngine.Object) componentInChildren.RaidSettings != (UnityEngine.Object) null && componentInChildren.MultiRaidNum > 0)
          num *= componentInChildren.MultiRaidNum;
        this.LackAp.text = LocalizedText.Get("sys.TEXT_APHEAL_LACK_POINT", new object[1]
        {
          (object) (num - player.Stamina)
        });
      }
      this.heal_coin_text.text = LocalizedText.Get("sys.SKIPBATTLE_HEAL_NUM", new object[1]
      {
        (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaAdd.ToString()
      });
      this.heal_coin_num.text = player.GetStaminaRecoveryCost(false).ToString();
      if (player.StaminaStockCap <= player.Stamina)
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.STAMINAFULL"), (UIUtility.DialogResultEvent) (go => FlowNode_GameObject.ActivateOutputLinks((Component) this, 2)), (GameObject) null, false, -1);
      this.pre_ap.text = player.Stamina.ToString();
    }

    public void OnSelect(GameObject go)
    {
      DataSource.Bind<ItemData>(this.gameObject, DataSource.FindDataOfClass<ItemData>(go, (ItemData) null));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public void HealApCoin()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
    }

    public void OnClickHeal()
    {
      if (this.bar.IsOverFlow)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 5);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
    }
  }
}
