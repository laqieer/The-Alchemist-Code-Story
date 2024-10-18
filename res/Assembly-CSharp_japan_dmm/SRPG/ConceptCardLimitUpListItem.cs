// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardLimitUpListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardLimitUpListItem : MonoBehaviour
  {
    [SerializeField]
    private Slider UseLimitUpItemSlider;
    [SerializeField]
    private Button PlusBtn;
    [SerializeField]
    private Button MinusBtn;
    [SerializeField]
    private Text TextUseItemNum;
    [SerializeField]
    private GameObject ItemStatus;
    private bool IsLock;
    private int mValueMax;
    private ItemData mItemData;
    private int mUseNum;
    private int mNeedNum;
    private Action OnRefresh;
    private ConceptCardLimitUpListItem.CheckSliderValue OnCheck;
    private float mPreValue;

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusBtn, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.PlusBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnAddNum)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MinusBtn, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.MinusBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnRemoveNum)));
    }

    public void Init(
      int current_awake,
      int awake_cap,
      int need_num,
      ItemData item_data,
      ConceptCardLimitUpListItem.CheckSliderValue onCheck,
      Action onRefresh)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.TextUseItemNum, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemStatus, (UnityEngine.Object) null) || need_num <= 0)
        return;
      this.mItemData = item_data;
      this.mNeedNum = need_num;
      this.OnCheck = onCheck;
      this.OnRefresh = onRefresh;
      ((UnityEventBase) this.UseLimitUpItemSlider.onValueChanged).RemoveAllListeners();
      this.UseLimitUpItemSlider.minValue = 0.0f;
      this.mValueMax = awake_cap - current_awake;
      int num = item_data.Num / this.mNeedNum;
      if (num < this.mValueMax)
        this.mValueMax = num;
      this.UseLimitUpItemSlider.maxValue = (float) this.mValueMax;
      this.UseLimitUpItemSlider.value = this.UseLimitUpItemSlider.minValue;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextUseItemNum, (UnityEngine.Object) null))
        this.TextUseItemNum.text = this.mUseNum.ToString();
      DataSource.Bind<ItemParam>(this.ItemStatus, this.mItemData.Param);
      DataSource.Bind<ItemData>(this.ItemStatus, this.mItemData);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusBtn, (UnityEngine.Object) null))
        ((Selectable) this.PlusBtn).interactable = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null) && (double) this.UseLimitUpItemSlider.value < (double) this.UseLimitUpItemSlider.maxValue;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MinusBtn, (UnityEngine.Object) null))
        ((Selectable) this.MinusBtn).interactable = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null) && (double) this.UseLimitUpItemSlider.value > (double) this.UseLimitUpItemSlider.minValue;
      // ISSUE: method pointer
      ((UnityEvent<float>) this.UseLimitUpItemSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnValueChanged)));
    }

    private void Refresh(float value)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null))
        return;
      this.UseLimitUpItemSlider.value = (float) Mathf.Min(this.mValueMax, (int) value);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextUseItemNum, (UnityEngine.Object) null))
        this.TextUseItemNum.text = this.mUseNum.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusBtn, (UnityEngine.Object) null))
        ((Selectable) this.PlusBtn).interactable = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null) && (double) this.UseLimitUpItemSlider.value < (double) this.UseLimitUpItemSlider.maxValue && !this.IsLock;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MinusBtn, (UnityEngine.Object) null))
        return;
      ((Selectable) this.MinusBtn).interactable = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null) && (double) this.UseLimitUpItemSlider.value > (double) this.UseLimitUpItemSlider.minValue;
    }

    private void OnValueChanged(float value)
    {
      if ((double) this.mPreValue == (double) value)
        return;
      if (this.OnCheck != null && (double) value > (double) this.mPreValue)
        value = (float) this.OnCheck(this.mItemData.ItemID, (int) value);
      if (this.IsLock && (double) value > (double) this.mPreValue)
        value = (float) (int) this.mPreValue;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null))
      {
        ((UnityEventBase) this.UseLimitUpItemSlider.onValueChanged).RemoveAllListeners();
        this.UseLimitUpItemSlider.value = value;
        // ISSUE: method pointer
        ((UnityEvent<float>) this.UseLimitUpItemSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnValueChanged)));
      }
      if ((double) value == (double) this.mPreValue)
        return;
      this.mUseNum = (int) value * this.mNeedNum;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextUseItemNum, (UnityEngine.Object) null))
        this.TextUseItemNum.text = this.mUseNum.ToString();
      if (this.OnRefresh != null)
        this.OnRefresh();
      this.UpdateButtonInterctable();
      this.mPreValue = value;
    }

    private void UpdateButtonInterctable()
    {
      ((Selectable) this.PlusBtn).interactable = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null) && (double) this.UseLimitUpItemSlider.value < (double) this.UseLimitUpItemSlider.maxValue && !this.IsLock;
      ((Selectable) this.MinusBtn).interactable = UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null) && (double) this.UseLimitUpItemSlider.value > (double) this.UseLimitUpItemSlider.minValue;
    }

    private void OnAddNum()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null))
        return;
      ++this.UseLimitUpItemSlider.value;
    }

    private void OnRemoveNum()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null))
        return;
      --this.UseLimitUpItemSlider.value;
    }

    public void OnReset()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null))
        this.UseLimitUpItemSlider.value = this.UseLimitUpItemSlider.minValue;
      this.mUseNum = 0;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextUseItemNum, (UnityEngine.Object) null))
        return;
      this.TextUseItemNum.text = this.mUseNum.ToString();
    }

    public void SetInputLock(bool islock)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PlusBtn, (UnityEngine.Object) null))
        ((Selectable) this.PlusBtn).interactable = !islock;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLimitUpItemSlider, (UnityEngine.Object) null))
      {
        if (islock)
          ((Selectable) this.UseLimitUpItemSlider).interactable = (double) this.UseLimitUpItemSlider.value > (double) this.UseLimitUpItemSlider.minValue;
        else
          ((Selectable) this.UseLimitUpItemSlider).interactable = true;
      }
      this.IsLock = islock;
    }

    public ItemData GetItemData() => this.mItemData;

    public int GetUpCount() => this.mUseNum / this.mNeedNum;

    public int GetUseNum() => this.mUseNum;

    public delegate int CheckSliderValue(string iname, int value);
  }
}
