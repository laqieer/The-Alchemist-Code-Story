// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardLevelUpListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardLevelUpListItem : MonoBehaviour
  {
    [SerializeField]
    private int MaxCardNum = 999;
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
    [SerializeField]
    private Text ConceptCardEnExp;
    [SerializeField]
    private ConceptCardIcon CardIcon;
    [SerializeField]
    private Text CardNum;
    [SerializeField]
    private GameObject ExpObject;
    public ConceptCardLevelUpListItem.SelectExpItem OnSelect;
    public ConceptCardLevelUpListItem.ChangeToggleEvent ChangeUseMax;
    public ConceptCardLevelUpListItem.CheckSliderValue OnCheck;
    private int mMaxValue;
    private bool IsLock;
    private float mPrevValue;
    private ConceptCardMaterialData mConceptCardMaterialData;
    private ConceptCardData mCardData;

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
      if ((UnityEngine.Object) this.UseExpItemSlider == (UnityEngine.Object) null || (UnityEngine.Object) this.SelectUseNum == (UnityEngine.Object) null || ((UnityEngine.Object) this.CardIcon == (UnityEngine.Object) null || this.mConceptCardMaterialData == null))
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
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
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
      this.UseExpItemSlider.onValueChanged.RemoveAllListeners();
      this.UseExpItemSlider.minValue = 0.0f;
      this.UseExpItemSlider.maxValue = (float) num1;
      this.UseExpItemSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnSelectUseNumChanged));
      this.UseExpItemSlider.value = this.UseExpItemSlider.minValue;
      this.SelectUseNum.text = this.UseExpItemSlider.value.ToString();
      if ((UnityEngine.Object) this.UseItemNum != (UnityEngine.Object) null)
        this.UseItemNum.text = this.UseExpItemSlider.value.ToString();
      if ((UnityEngine.Object) this.ConceptCardEnExp != (UnityEngine.Object) null)
        this.ConceptCardEnExp.text = this.mCardData.Param.en_exp.ToString();
      this.mMaxValue = num1;
      if ((UnityEngine.Object) this.CardNum != (UnityEngine.Object) null)
        this.CardNum.text = Mathf.Min((int) this.mConceptCardMaterialData.Num, this.MaxCardNum).ToString();
      if ((UnityEngine.Object) this.CheckUseMax != (UnityEngine.Object) null)
        this.CheckUseMax.onValueChanged.AddListener(new UnityAction<bool>(this.OnChangeUseMax));
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
        this.PlusBtn.interactable = (UnityEngine.Object) this.UseExpItemSlider != (UnityEngine.Object) null && (double) this.UseExpItemSlider.value < (double) this.UseExpItemSlider.maxValue && !this.IsLock;
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

    public void SetUseParamItemSliderValue(int value)
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

    public void AddConceptCardData(ConceptCardMaterialData material_data)
    {
      this.mConceptCardMaterialData = material_data;
    }

    public string GetConceptCardIName()
    {
      if (this.mConceptCardMaterialData == null)
        return (string) null;
      return (string) this.mConceptCardMaterialData.IName;
    }

    public ConceptCardData GetConceptCardData()
    {
      return this.mCardData;
    }

    public void SetExpObject(bool flag)
    {
      if ((UnityEngine.Object) this.ExpObject == (UnityEngine.Object) null)
        return;
      this.ExpObject.SetActive(flag);
    }

    public delegate void SelectExpItem(string iname, int value);

    public delegate void ChangeToggleEvent(string iname, bool is_on);

    public delegate int CheckSliderValue(string iname, int value);
  }
}
