// Decompiled with JetBrains decompiler
// Type: SRPG.GvGBattleResultUnitContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGBattleResultUnitContent : MonoBehaviour
  {
    [SerializeField]
    private Slider mGauge;
    [SerializeField]
    private GameObject mDead;

    private void Awake()
    {
      GvGBattleResultUnitContentData dataOfClass = DataSource.FindDataOfClass<GvGBattleResultUnitContentData>(((Component) this).gameObject, (GvGBattleResultUnitContentData) null);
      if (dataOfClass == null)
        return;
      DataSource.Bind<Unit>(((Component) this).gameObject, dataOfClass.mUnit);
      GameUtility.SetGameObjectActive(this.mDead, dataOfClass.mHp == 0);
      if (!Object.op_Inequality((Object) this.mGauge, (Object) null) || dataOfClass.mUnit.MaximumStatusHp <= 0)
        return;
      this.mGauge.value = (float) dataOfClass.mHp * 100f / (float) dataOfClass.mUnit.MaximumStatusHp;
    }
  }
}
