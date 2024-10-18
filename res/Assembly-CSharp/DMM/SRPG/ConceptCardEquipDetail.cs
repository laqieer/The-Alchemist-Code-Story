// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "更新終了", FlowNode.PinTypes.Output, 10)]
  public class ConceptCardEquipDetail : MonoBehaviour
  {
    public const int PIN_REFRESH = 0;
    public const int PIN_REFRESH_END = 10;
    [HeaderBar("▼ConceptCardDescriptionの参照方式")]
    [SerializeField]
    private ConceptCardEquipDetail.DescriptionInstanceType m_DescriptionInstanceType;
    [SerializeField]
    private ConceptCardDescription mConceptCardDescription;
    [HeaderBar("▼複製したConceptCardDescriptionを入れる親")]
    [SerializeField]
    private RectTransform mConceptCardDescriptionRoot;
    [SerializeField]
    private GameObject mConceptCardIconRoot;
    [SerializeField]
    private Text mCardNameText;
    [SerializeField]
    private ConceptCardIcon mConceptCardIcon;
    [SerializeField]
    private Text mConceptCardNum;
    private ConceptCardData mConceptCardData;
    private UnitData mUnitData;
    private static UnitData s_UnitData;
    private static int s_SelectedSlotIndex;

    public static void SetSelectedUnitData(UnitData mUnitData, int selectedSlotIndex)
    {
      ConceptCardEquipDetail.s_UnitData = mUnitData;
      ConceptCardEquipDetail.s_SelectedSlotIndex = selectedSlotIndex;
    }

    private void Start()
    {
      if (this.m_DescriptionInstanceType == ConceptCardEquipDetail.DescriptionInstanceType.PrefabInstantiate)
      {
        this.mConceptCardDescription = Object.Instantiate<ConceptCardDescription>(this.mConceptCardDescription);
        ((Component) this.mConceptCardDescription).transform.SetParent((Transform) this.mConceptCardDescriptionRoot, false);
      }
      this.SetParam();
    }

    public void SetParam()
    {
      this.mConceptCardData = (ConceptCardData) GlobalVars.SelectedConceptCardData;
      this.mUnitData = ConceptCardEquipDetail.s_UnitData;
      if (this.mConceptCardData == null)
        return;
      this.Refresh();
      this.mConceptCardDescription.SetConceptCardData(this.mConceptCardData, ((Component) this).gameObject, false, this.CheckGetUnitFrame(), this.mUnitData, !ConceptCardData.IsMainSlot(ConceptCardEquipDetail.s_SelectedSlotIndex));
    }

    private bool CheckGetUnitFrame()
    {
      bool unitFrame = false;
      if (FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue)
        unitFrame = currentValue.GetBool("is_first_get_unit");
      return unitFrame;
    }

    private void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
      if (Object.op_Inequality((Object) this.mConceptCardIconRoot, (Object) null))
        this.mConceptCardIconRoot.SetActive(true);
      if (Object.op_Inequality((Object) this.mConceptCardIcon, (Object) null))
        this.mConceptCardIcon.Setup(this.mConceptCardData);
      this.SetText(this.mConceptCardNum, MonoSingleton<GameManager>.Instance.Player.GetConceptCardNum(this.mConceptCardData.Param.iname));
    }

    public void SetText(Text text, string str)
    {
      if (!Object.op_Inequality((Object) text, (Object) null))
        return;
      text.text = str;
    }

    public void SetText(Text text, int value) => this.SetText(text, value.ToString());

    private void OnDestroy() => ConceptCardEquipDetail.s_UnitData = (UnitData) null;

    private enum DescriptionInstanceType
    {
      DirectUse,
      PrefabInstantiate,
    }
  }
}
