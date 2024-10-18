// Decompiled with JetBrains decompiler
// Type: SRPG.ResultGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance) || !instance.IsGetFirstClearItem || !(bool) ((UnityEngine.Object) this.GoGetUnitAnim))
        return;
      string firstClearItemId = instance.FirstClearItemId;
      ItemParam itemParam = instanceDirect.GetItemParam(firstClearItemId);
      if (itemParam == null || itemParam.type != EItemType.Unit)
        return;
      UnitParam unitParam = instanceDirect.GetUnitParam(firstClearItemId);
      if (unitParam == null)
        return;
      DataSource.Bind<ItemParam>(this.gameObject, itemParam, false);
      DataSource.Bind<UnitParam>(this.gameObject, unitParam, false);
      if ((bool) ((UnityEngine.Object) this.ImgUnit))
        instanceDirect.ApplyTextureAsync(this.ImgUnit, AssetPath.UnitImage(unitParam, unitParam.GetJobId(0)));
      GameParameter.UpdateAll(this.gameObject);
      Animator component = this.GoGetUnitAnim.GetComponent<Animator>();
      if (!(bool) ((UnityEngine.Object) component))
        return;
      component.SetInteger("rariry", (int) unitParam.rare + 1);
    }
  }
}
