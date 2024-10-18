// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailGetUnit : ConceptCardDetailBase
  {
    [SerializeField]
    private RawImage UnitIcon;
    [SerializeField]
    private Text UnitName;
    [SerializeField]
    private ButtonEvent UnitDetailBtn;

    public override void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      string firstGetUnit = this.mConceptCardData.Param.first_get_unit;
      if (string.IsNullOrEmpty(firstGetUnit))
        return;
      UnitParam unitParam = this.GM.GetUnitParam(firstGetUnit);
      if (unitParam == null)
        return;
      if ((UnityEngine.Object) this.UnitIcon != (UnityEngine.Object) null)
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.UnitIcon, unitParam == null ? (string) null : AssetPath.UnitSkinIconSmall(unitParam, (ArtifactParam) null, (string) null));
      if ((UnityEngine.Object) this.UnitName != (UnityEngine.Object) null)
        this.UnitName.text = unitParam.name;
      if (!((UnityEngine.Object) this.UnitDetailBtn != (UnityEngine.Object) null))
        return;
      this.UnitDetailBtn.GetEvent("CONCEPT_CARD_DETAIL_BTN_UNIT_DETAIL")?.valueList.SetField("select_unit", unitParam.iname);
    }
  }
}
