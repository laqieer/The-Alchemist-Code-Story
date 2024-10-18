// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardLevelUpListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardLevelUpListItem : MonoBehaviour
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
    [SerializeField]
    private Text ConceptCardEnExp;
    [SerializeField]
    private ConceptCardIcon CardIcon;
    [SerializeField]
    private Text CardNum;
    [SerializeField]
    private GameObject ExpObject;
    [SerializeField]
    private int MaxCardNum = 999;
    public ConceptCardLevelUpListItem.SelectExpItem OnSelect;
    public ConceptCardLevelUpListItem.ChangeToggleEvent ChangeUseMax;
    public ConceptCardLevelUpListItem.CheckSliderValue OnCheck;
    private string mCurrentItemID = string.Empty;
    private int mMaxValue;
    private bool IsLock;
    private float mPrevValue;
    private ConceptCardMaterialData mConceptCardMaterialData;
    private ConceptCardData mCardData;

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
      if (Object.op_Equality((Object) this.UseExpItemSlider, (Object) null) || Object.op_Equality((Object) this.SelectUseNum, (Object) null) || Object.op_Equality((Object) this.CardIcon, (Object) null) || this.mConceptCardMaterialData == null)
        return;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam((string) this.mConceptCardMaterialData.IName);
      if (conceptCardParam == null)
        return;
      JSON_ConceptCard json = new JSON_ConceptCard();
      json.iid = (long) this.mConceptCardMaterialData.UniqueID;
      json.iname = (string) this.mConceptCardMaterialData.IName;
      json.exp = conceptCardParam.en_exp;
      json.trust = conceptCardParam.en_trust;
      this.mCardData = new ConceptCardData();
      this.mCardData.Deserialize(json);
      this.CardIcon.Setup(this.mCardData);
      this.mCurrentItemID = (string) this.mConceptCardMaterialData.IName;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      int num1;
      if (this.mConceptCardMaterialData.Param.type == eCardType.Enhance_exp)
      {
        num1 = Mathf.Min(instance.SelectedConceptCardData.GetExpToLevelMax() / this.mConceptCardMaterialData.Param.en_exp, (int) this.mConceptCardMaterialData.Num);
        if (this.mConceptCardMaterialData.Param.en_exp * num1 < instance.SelectedConceptCardData.GetExpToLevelMax() && num1 < (int) this.mConceptCardMaterialData.Num)
          ++num1;
      }
      else
      {
        if (this.mConceptCardMaterialData.Param.type != eCardType.Enhance_trust)
          return;
        int num2 = (int) instance.SelectedConceptCardData.Trust / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax + (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
        if ((int) instance.SelectedConceptCardData.Trust >= ((int) instance.SelectedConceptCardData.AwakeCount + 1) * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax)
          num2 = ((int) instance.SelectedConceptCardData.AwakeCount + 1) * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
        int num3 = num2 - (int) instance.SelectedConceptCardData.Trust;
        num1 = Mathf.Min(num3 / this.mConceptCardMaterialData.Param.en_trust, (int) this.mConceptCardMaterialData.Num);
        if (this.mConceptCardMaterialData.Param.en_trust * num1 < num3 && num1 < (int) this.mConceptCardMaterialData.Num)
          ++num1;
      }
      ((UnityEventBase) this.UseExpItemSlider.onValueChanged).RemoveAllListeners();
      this.UseExpItemSlider.minValue = 0.0f;
      this.UseExpItemSlider.maxValue = (float) num1;
      // ISSUE: method pointer
      ((UnityEvent<float>) this.UseExpItemSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnSelectUseNumChanged)));
      this.UseExpItemSlider.value = this.UseExpItemSlider.minValue;
      this.SelectUseNum.text = this.UseExpItemSlider.value.ToString();
      if (Object.op_Inequality((Object) this.UseItemNum, (Object) null))
        this.UseItemNum.text = this.UseExpItemSlider.value.ToString();
      if (Object.op_Inequality((Object) this.ConceptCardEnExp, (Object) null))
        this.ConceptCardEnExp.text = this.mCardData.Param.en_exp.ToString();
      this.mMaxValue = num1;
      if (Object.op_Inequality((Object) this.CardNum, (Object) null))
        this.CardNum.text = Mathf.Min((int) this.mConceptCardMaterialData.Num, this.MaxCardNum).ToString();
      if (Object.op_Inequality((Object) this.CheckUseMax, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.CheckUseMax.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChangeUseMax)));
      }
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
        ((Selectable) this.PlusBtn).interactable = Object.op_Inequality((Object) this.UseExpItemSlider, (Object) null) && (double) this.UseExpItemSlider.value < (double) this.UseExpItemSlider.maxValue && !this.IsLock;
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

    public void SetUseParamItemSliderValue(int value)
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

    public void AddConceptCardData(ConceptCardMaterialData material_data)
    {
      this.mConceptCardMaterialData = material_data;
    }

    public string GetConceptCardIName()
    {
      return this.mConceptCardMaterialData == null ? (string) null : (string) this.mConceptCardMaterialData.IName;
    }

    public ConceptCardData GetConceptCardData() => this.mCardData;

    public void SetExpObject(bool flag)
    {
      if (Object.op_Equality((Object) this.ExpObject, (Object) null))
        return;
      this.ExpObject.SetActive(flag);
    }

    public delegate void SelectExpItem(string iname, int value);

    public delegate void ChangeToggleEvent(string iname, bool is_on);

    public delegate int CheckSliderValue(string iname, int value);
  }
}
