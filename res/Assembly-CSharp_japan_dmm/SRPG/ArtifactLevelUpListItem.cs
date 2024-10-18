// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactLevelUpListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ArtifactLevelUpListItem : MonoBehaviour
  {
    [SerializeField]
    private Slider UseExpItemSlider;
    [SerializeField]
    private Button PlusBtn;
    [SerializeField]
    private Button MinusBtn;
    [SerializeField]
    private Text SelectUseNum;
    [SerializeField]
    private Toggle CheckUseMax;
    [SerializeField]
    private Text UseItemNum;
    public ArtifactLevelUpListItem.SelectExpItem OnSelect;
    public ArtifactLevelUpListItem.ChangeToggleEvent ChangeUseMax;
    public ArtifactLevelUpListItem.CheckSliderValue OnCheck;
    private string mCurrentItemID = string.Empty;
    private int mMaxValue;
    private bool IsLock;
    private float mPrevValue;

    private void Start()
    {
      if (Object.op_Inequality((Object) this.PlusBtn, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.PlusBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnAddNum)));
      }
      if (Object.op_Inequality((Object) this.MinusBtn, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MinusBtn.onClick).AddListener(new UnityAction((object) this, __methodptr(OnRemoveNum)));
      }
      this.Init();
    }

    private void Init()
    {
      if (Object.op_Equality((Object) this.UseExpItemSlider, (Object) null) || Object.op_Equality((Object) this.SelectUseNum, (Object) null))
        return;
      ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).gameObject, (ArtifactData) null);
      if (dataOfClass1 == null)
        return;
      ItemData dataOfClass2 = DataSource.FindDataOfClass<ItemData>(((Component) this).gameObject, (ItemData) null);
      if (dataOfClass2 == null)
        return;
      int num1 = dataOfClass1.GetGainExpCap() - dataOfClass1.Exp;
      int num2 = Mathf.Max(1, Mathf.Min(dataOfClass2.Num, Mathf.CeilToInt((float) num1 / (float) dataOfClass2.Param.value)));
      this.mCurrentItemID = dataOfClass2.Param.iname;
      ((UnityEventBase) this.UseExpItemSlider.onValueChanged).RemoveAllListeners();
      this.UseExpItemSlider.minValue = 0.0f;
      this.UseExpItemSlider.maxValue = (float) num2;
      // ISSUE: method pointer
      ((UnityEvent<float>) this.UseExpItemSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectUseNumChanged)));
      this.UseExpItemSlider.value = this.UseExpItemSlider.minValue;
      this.SelectUseNum.text = this.UseExpItemSlider.value.ToString();
      if (Object.op_Inequality((Object) this.UseItemNum, (Object) null))
        this.UseItemNum.text = this.UseExpItemSlider.value.ToString();
      if (Object.op_Inequality((Object) this.CheckUseMax, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.CheckUseMax.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeUseMax)));
      }
      this.mMaxValue = num2;
      if (Object.op_Inequality((Object) this.PlusBtn, (Object) null))
        ((Selectable) this.PlusBtn).interactable = Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.value < (double) this.UseExpItemSlider.maxValue;
      if (!Object.op_Inequality((Object) this.MinusBtn, (Object) null))
        return;
      ((Selectable) this.MinusBtn).interactable = Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.value > (double) this.UseExpItemSlider.minValue;
    }

    private void Refresh(float value)
    {
      if (Object.op_Equality((Object) this.UseExpItemSlider, (Object) null))
        return;
      this.UseExpItemSlider.value = (float) Mathf.Min(this.mMaxValue, (int) value);
      if (Object.op_Inequality((Object) this.SelectUseNum, (Object) null))
        this.SelectUseNum.text = ((int) this.UseExpItemSlider.value).ToString();
      if (Object.op_Inequality((Object) this.UseItemNum, (Object) null))
        this.UseItemNum.text = ((int) this.UseExpItemSlider.value).ToString();
      if (Object.op_Inequality((Object) this.PlusBtn, (Object) null))
        ((Selectable) this.PlusBtn).interactable = !this.IsLock && Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.value < (double) this.UseExpItemSlider.maxValue;
      if (Object.op_Inequality((Object) this.MinusBtn, (Object) null))
        ((Selectable) this.MinusBtn).interactable = Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.value > (double) this.UseExpItemSlider.minValue;
      this.mPrevValue = this.UseExpItemSlider.value;
    }

    private void OnSelectUseNumChanged(float value)
    {
      if (this.OnCheck != null && (double) value > (double) this.mPrevValue)
      {
        int num = this.OnCheck(this.mCurrentItemID, (int) value);
        if (num >= 0)
        {
          value = (float) num;
          this.UseExpItemSlider.value = value;
        }
      }
      if ((double) value > (double) this.mPrevValue && this.IsLock)
      {
        this.UseExpItemSlider.value = this.mPrevValue;
      }
      else
      {
        if (this.OnSelect != null)
          this.OnSelect(this.mCurrentItemID, (int) value);
        this.Refresh(value);
      }
    }

    private void OnAddNum()
    {
      if (!Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null))
        return;
      ++this.UseExpItemSlider.value;
    }

    private void OnRemoveNum()
    {
      if (!Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null))
        return;
      --this.UseExpItemSlider.value;
      this.IsLock = false;
      ((Selectable) this.PlusBtn).interactable = true;
    }

    public void OnChangeUseMax(bool value)
    {
      if (this.ChangeUseMax == null)
        return;
      this.ChangeUseMax(this.mCurrentItemID, value);
    }

    public bool IsUseMax()
    {
      return !Object.op_Equality((Object) this.CheckUseMax, (Object) null) && this.CheckUseMax.isOn;
    }

    public void SetUseExpItemSliderValue(int value)
    {
      if (value < 0)
        return;
      ((UnityEventBase) this.UseExpItemSlider.onValueChanged).RemoveAllListeners();
      this.Refresh((float) value);
      // ISSUE: method pointer
      ((UnityEvent<float>) this.UseExpItemSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectUseNumChanged)));
    }

    public void Reset()
    {
      if (Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null))
        this.UseExpItemSlider.value = this.UseExpItemSlider.minValue;
      if (!Object.op_Inequality((Object) this.SelectUseNum, (Object) null))
        return;
      this.SelectUseNum.text = "0";
    }

    public void SetInputLock(bool islock)
    {
      if (Object.op_Inequality((Object) this.PlusBtn, (Object) null))
        ((Selectable) this.PlusBtn).interactable = islock;
      if (Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null))
        ((Selectable) this.UseExpItemSlider).interactable = islock ? islock : (double) this.UseExpItemSlider.value != 0.0;
      this.IsLock = !islock;
    }

    public void SetUseMax(bool is_on) => this.CheckUseMax.isOn = is_on;

    public delegate void SelectExpItem(string iname, int value);

    public delegate void ChangeToggleEvent(string iname, bool is_on);

    public delegate int CheckSliderValue(string iname, int value);
  }
}
