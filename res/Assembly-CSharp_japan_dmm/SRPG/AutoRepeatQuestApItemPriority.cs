// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestApItemPriority
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "設定反映チェック", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(100, "設定反映OK", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "設定反映NG", FlowNode.PinTypes.Output, 110)]
  public class AutoRepeatQuestApItemPriority : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_APPLY_CHECK = 20;
    private const int PIN_OUTPUT_APPLY_CHECK_OK = 100;
    private const int PIN_OUTPUT_APPLY_CHECK_NG = 110;
    [SerializeField]
    private GameObject mApItemTemplate;
    [SerializeField]
    private Button mDecideButton;
    private List<AutoRepeatQuestApItemContent> mCreatedApItemContents = new List<AutoRepeatQuestApItemContent>();
    private List<AutoRepeatQuestApItemContent> mSelectedApItemContents = new List<AutoRepeatQuestApItemContent>();
    private static AutoRepeatQuestApItemPriority mInstance;

    public static AutoRepeatQuestApItemPriority Instance => AutoRepeatQuestApItemPriority.mInstance;

    private void Awake()
    {
      AutoRepeatQuestApItemPriority.mInstance = this;
      if (!GameUtility.IsDebugBuild || this.IsEnough_PriorityImageArray())
        return;
      DebugUtility.LogError("優先度の数値画像の枚数が不足しています。マスターに存在する「AP回復アイテムの種類数」よりも多い必要があります。");
    }

    private void OnDestroy()
    {
      AutoRepeatQuestApItemPriority.mInstance = (AutoRepeatQuestApItemPriority) null;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Init();
          break;
        case 20:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, !this.IsEnableSettings() ? 110 : 100);
          break;
      }
    }

    private void Init()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDecideButton, (UnityEngine.Object) null))
        ((Selectable) this.mDecideButton).interactable = false;
      this.CreateApItemList();
      this.LoadPlayerApItemPriority();
    }

    private void LoadPlayerApItemPriority()
    {
      List<string> ap_items = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestApItemPriority;
      for (int i = 0; i < ap_items.Count; ++i)
        this.OnSelectToggle(this.mCreatedApItemContents.Find((Predicate<AutoRepeatQuestApItemContent>) (content => UnityEngine.Object.op_Inequality((UnityEngine.Object) content, (UnityEngine.Object) null) && content.ItemIname == ap_items[i])));
    }

    private void CreateApItemList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mApItemTemplate, (UnityEngine.Object) null) || this.mCreatedApItemContents.Count > 0)
        return;
      this.mApItemTemplate.SetActive(false);
      List<ItemParam> all = MonoSingleton<GameManager>.Instance.MasterParam.Items.FindAll((Predicate<ItemParam>) (item => item.type == EItemType.ApHeal));
      for (int index = 0; index < all.Count; ++index)
      {
        if (MonoSingleton<GameManager>.Instance.Player.HasItem(all[index].iname))
        {
          AutoRepeatQuestApItemContent component = UnityEngine.Object.Instantiate<GameObject>(this.mApItemTemplate, this.mApItemTemplate.transform.parent, false).GetComponent<AutoRepeatQuestApItemContent>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.Init(all[index]);
            ((Component) component).gameObject.SetActive(true);
            this.mCreatedApItemContents.Add(component);
          }
        }
      }
    }

    public string[] GetApItemPriority()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.mSelectedApItemContents.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedApItemContents[index], (UnityEngine.Object) null))
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.mSelectedApItemContents[index].ItemIname);
          if (itemParam != null && itemParam.type == EItemType.ApHeal)
            stringList.Add(this.mSelectedApItemContents[index].ItemIname);
        }
      }
      return stringList.ToArray();
    }

    public void OnSelectToggle(AutoRepeatQuestApItemContent self)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self, (UnityEngine.Object) null))
      {
        if (!this.mSelectedApItemContents.Contains(self))
          this.mSelectedApItemContents.Add(self);
        else
          this.mSelectedApItemContents.Remove(self);
        int num = this.mSelectedApItemContents.Count - 1;
        for (int index = 0; index < this.mCreatedApItemContents.Count; ++index)
        {
          int priority = this.mSelectedApItemContents.IndexOf(this.mCreatedApItemContents[index]);
          bool is_last_priority = false;
          if (priority >= 0)
            is_last_priority = priority == num;
          this.mCreatedApItemContents[index].Refresh(priority, is_last_priority);
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDecideButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.mDecideButton).interactable = this.IsEnableSettings();
    }

    private bool IsEnableSettings() => this.mSelectedApItemContents.Count > 0;

    private bool IsEnough_PriorityImageArray()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mApItemTemplate, (UnityEngine.Object) null))
        return false;
      AutoRepeatQuestApItemContent component = this.mApItemTemplate.GetComponent<AutoRepeatQuestApItemContent>();
      return !UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.MasterParam.Items.FindAll((Predicate<ItemParam>) (item => item.type == EItemType.ApHeal)).Count <= component.PriorityImageMax;
    }
  }
}
