// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftSelectedUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class VersusDraftSelectedUnit : MonoBehaviour
  {
    [SerializeField]
    private UnitIcon mUnitIcon;
    [SerializeField]
    private GameObject mSelecting;
    [SerializeField]
    private GameObject mSecretIcon;
    private DataSource mDataSource;

    public void Initialize()
    {
      if (Object.op_Inequality((Object) this.mUnitIcon, (Object) null) && Object.op_Inequality((Object) this.mUnitIcon.Icon, (Object) null))
        ((Component) this.mUnitIcon.Icon).gameObject.SetActive(false);
      this.mDataSource = DataSource.Create(((Component) this).gameObject);
    }

    public void Selecting()
    {
      if (!Object.op_Inequality((Object) this.mSelecting, (Object) null))
        return;
      this.mSelecting.SetActive(true);
    }

    public void SetUnit(UnitData unit)
    {
      if (Object.op_Inequality((Object) this.mSecretIcon, (Object) null))
        this.mSecretIcon.SetActive(unit == null);
      if (Object.op_Inequality((Object) this.mUnitIcon, (Object) null) && Object.op_Inequality((Object) this.mUnitIcon.Icon, (Object) null))
        ((Component) this.mUnitIcon.Icon).gameObject.SetActive(unit != null);
      if (!Object.op_Inequality((Object) this.mUnitIcon, (Object) null))
        return;
      this.mDataSource.Clear();
      this.mDataSource.Add(typeof (UnitData), (object) unit);
      this.mUnitIcon.UpdateValue();
    }

    public void Select(UnitData unit)
    {
      if (Object.op_Inequality((Object) this.mSelecting, (Object) null))
        this.mSelecting.SetActive(false);
      this.SetUnit(unit);
    }
  }
}
