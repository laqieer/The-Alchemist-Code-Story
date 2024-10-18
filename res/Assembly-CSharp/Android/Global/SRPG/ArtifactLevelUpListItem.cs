// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactLevelUpListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ArtifactLevelUpListItem : MonoBehaviour
  {
    private string mCurrentItemID = string.Empty;
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
    private int mMaxValue;
    private bool IsLock;
    private float mPrevValue;

    private void Start()
    {
      if ((UnityEngine.Object) this.PlusBtn != (UnityEngine.Object) null)
        this.PlusBtn.onClick.AddListener(new UnityAction(this.OnAddNum));
      if ((UnityEngine.Object) this.MinusBtn != (UnityEngine.Object) null)
        this.MinusBtn.onClick.AddListener(new UnityAction(this.OnRemoveNum));
      this.Init();
    }

    private void Init()
    {
      if ((UnityEngine.Object) this.UseExpItemSlider == (UnityEngine.Object) null || (UnityEngine.Object) this.SelectUseNum == (UnityEngine.Object) null)
        return;
      ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(this.gameObject, (ArtifactData) null);
      if (dataOfClass1 == null)
        return;
      ItemData dataOfClass2 = DataSource.FindDataOfClass<ItemData>(this.gameObject, (ItemData) null);
      if (dataOfClass2 == null)
        return;
      int num1 = dataOfClass1.GetGainExpCap() - dataOfClass1.Exp;
      int num2 = Mathf.Max(1, Mathf.Min(dataOfClass2.Num, Mathf.CeilToInt((float) num1 / (float) (int) dataOfClass2.Param.value)));
      this.mCurrentItemID = dataOfClass2.Param.iname;
      this.UseExpItemSlider.onValueChanged.RemoveAllListeners();
      this.UseExpItemSlider.minValue = 0.0f;
      this.UseExpItemSlider.maxValue = (float) num2;
      this.UseExpItemSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnSelectUseNumChanged));
      this.UseExpItemSlider.value = this.UseExpItemSlider.minValue;
      this.SelectUseNum.text = this.UseExpItemSlider.value.ToString();
      if ((UnityEngine.Object) this.UseItemNum != (UnityEngine.Object) null)
        this.UseItemNum.text = this.UseExpItemSlider.value.ToString();
      if ((UnityEngine.Object) this.CheckUseMax != (UnityEngine.Object) null)
        this.CheckUseMax.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeUseMax));
      this.mMaxValue = num2;
      if ((UnityEngine.Object) this.PlusBtn != (UnityEngine.Object) null)
        this.PlusBtn.interactable = (UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null && (double) this.UseExpItemSlider.value < (double) this.UseExpItemSlider.maxValue;
      if (!((UnityEngine.Object) this.MinusBtn != (UnityEngine.Object) null))
        return;
      this.MinusBtn.interactable = (UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null && (double) this.UseExpItemSlider.value > (double) this.UseExpItemSlider.minValue;
    }

    private void Refresh(float value)
    {
      if ((UnityEngine.Object) this.UseExpItemSlider == (UnityEngine.Object) null)
        return;
      this.UseExpItemSlider.value = (float) Mathf.Min(this.mMaxValue, (int) value);
      if ((UnityEngine.Object) this.SelectUseNum != (UnityEngine.Object) null)
        this.SelectUseNum.text = ((int) this.UseExpItemSlider.value).ToString();
      if ((UnityEngine.Object) this.UseItemNum != (UnityEngine.Object) null)
        this.UseItemNum.text = ((int) this.UseExpItemSlider.value).ToString();
      if ((UnityEngine.Object) this.PlusBtn != (UnityEngine.Object) null)
        this.PlusBtn.interactable = !this.IsLock && ((UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null && (double) this.UseExpItemSlider.value < (double) this.UseExpItemSlider.maxValue);
      if ((UnityEngine.Object) this.MinusBtn != (UnityEngine.Object) null)
        this.MinusBtn.interactable = (UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null && (double) this.UseExpItemSlider.value > (double) this.UseExpItemSlider.minValue;
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
      if (!((UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null))
        return;
      ++this.UseExpItemSlider.value;
    }

    private void OnRemoveNum()
    {
      if (!((UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null))
        return;
      --this.UseExpItemSlider.value;
      this.IsLock = false;
      this.PlusBtn.interactable = true;
    }

    public void OnChangeUseMax(bool value)
    {
      if (this.ChangeUseMax == null)
        return;
      this.ChangeUseMax(this.mCurrentItemID, value);
    }

    public bool IsUseMax()
    {
      if ((UnityEngine.Object) this.CheckUseMax == (UnityEngine.Object) null)
        return false;
      return this.CheckUseMax.isOn;
    }

    public void SetUseExpItemSliderValue(int value)
    {
      if (value < 0)
        return;
      this.UseExpItemSlider.onValueChanged.RemoveAllListeners();
      this.Refresh((float) value);
      this.UseExpItemSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnSelectUseNumChanged));
    }

    public void Reset()
    {
      if ((UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null)
        this.UseExpItemSlider.value = this.UseExpItemSlider.minValue;
      if (!((UnityEngine.Object) this.SelectUseNum != (UnityEngine.Object) null))
        return;
      this.SelectUseNum.text = "0";
    }

    public void SetInputLock(bool islock)
    {
      if ((UnityEngine.Object) this.PlusBtn != (UnityEngine.Object) null)
        this.PlusBtn.interactable = islock;
      if ((UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null)
        this.UseExpItemSlider.interactable = islock ? islock : (double) this.UseExpItemSlider.value != 0.0;
      this.IsLock = !islock;
    }

    public void SetUseMax(bool is_on)
    {
      this.CheckUseMax.isOn = is_on;
    }

    public delegate void SelectExpItem(string iname, int value);

    public delegate void ChangeToggleEvent(string iname, bool is_on);

    public delegate int CheckSliderValue(string iname, int value);
  }
}
