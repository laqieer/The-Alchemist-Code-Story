// Decompiled with JetBrains decompiler
// Type: SRPG.ResultGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ResultGetUnit : MonoBehaviour
  {
    public GameObject GoGetUnitAnim;
    public GameObject GoGetUnitDetail;
    public RawImage ImgUnit;

    private void Start()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (Object.op_Equality((Object) instanceDirect, (Object) null))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance) || !instance.IsGetFirstClearItem || !Object.op_Implicit((Object) this.GoGetUnitAnim))
        return;
      string firstClearItemId = instance.FirstClearItemId;
      ItemParam itemParam = instanceDirect.GetItemParam(firstClearItemId);
      if (itemParam == null || itemParam.type != EItemType.Unit)
        return;
      UnitParam unitParam = instanceDirect.GetUnitParam(firstClearItemId);
      if (unitParam == null)
        return;
      DataSource.Bind<ItemParam>(((Component) this).gameObject, itemParam);
      DataSource.Bind<UnitParam>(((Component) this).gameObject, unitParam);
      if (Object.op_Implicit((Object) this.ImgUnit))
        instanceDirect.ApplyTextureAsync(this.ImgUnit, AssetPath.UnitImage(unitParam, unitParam.GetJobId(0)));
      GameParameter.UpdateAll(((Component) this).gameObject);
      Animator component = this.GoGetUnitAnim.GetComponent<Animator>();
      if (!Object.op_Implicit((Object) component))
        return;
      component.SetInteger("rariry", (int) unitParam.rare + 1);
    }
  }
}
