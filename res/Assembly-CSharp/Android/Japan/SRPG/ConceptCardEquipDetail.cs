// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEquipDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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

    public static void SetSelectedUnitData(UnitData mUnitData)
    {
      ConceptCardEquipDetail.s_UnitData = mUnitData;
    }

    private void Start()
    {
      if (this.m_DescriptionInstanceType == ConceptCardEquipDetail.DescriptionInstanceType.PrefabInstantiate)
      {
        this.mConceptCardDescription = UnityEngine.Object.Instantiate<ConceptCardDescription>(this.mConceptCardDescription);
        this.mConceptCardDescription.transform.SetParent((Transform) this.mConceptCardDescriptionRoot, false);
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
      this.mConceptCardDescription.SetConceptCardData(this.mConceptCardData, this.gameObject, false, this.CheckGetUnitFrame(), this.mUnitData);
    }

    private bool CheckGetUnitFrame()
    {
      bool flag = false;
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
        flag = currentValue.GetBool("is_first_get_unit");
      return flag;
    }

    private void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
      if ((UnityEngine.Object) this.mConceptCardIconRoot != (UnityEngine.Object) null)
        this.mConceptCardIconRoot.SetActive(true);
      if ((UnityEngine.Object) this.mConceptCardIcon != (UnityEngine.Object) null)
        this.mConceptCardIcon.Setup(this.mConceptCardData);
      this.SetText(this.mConceptCardNum, MonoSingleton<GameManager>.Instance.Player.GetConceptCardNum(this.mConceptCardData.Param.iname));
    }

    public void SetText(Text text, string str)
    {
      if (!((UnityEngine.Object) text != (UnityEngine.Object) null))
        return;
      text.text = str;
    }

    public void SetText(Text text, int value)
    {
      this.SetText(text, value.ToString());
    }

    private void OnDestroy()
    {
      ConceptCardEquipDetail.s_UnitData = (UnitData) null;
    }

    private enum DescriptionInstanceType
    {
      DirectUse,
      PrefabInstantiate,
    }
  }
}
