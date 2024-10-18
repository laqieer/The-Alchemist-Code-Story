// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftSelectedUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      if ((UnityEngine.Object) this.mUnitIcon != (UnityEngine.Object) null && (UnityEngine.Object) this.mUnitIcon.Icon != (UnityEngine.Object) null)
        this.mUnitIcon.Icon.gameObject.SetActive(false);
      this.mDataSource = DataSource.Create(this.gameObject);
    }

    public void Selecting()
    {
      if (!((UnityEngine.Object) this.mSelecting != (UnityEngine.Object) null))
        return;
      this.mSelecting.SetActive(true);
    }

    public void SetUnit(UnitData unit)
    {
      if ((UnityEngine.Object) this.mSecretIcon != (UnityEngine.Object) null)
        this.mSecretIcon.SetActive(unit == null);
      if ((UnityEngine.Object) this.mUnitIcon != (UnityEngine.Object) null && (UnityEngine.Object) this.mUnitIcon.Icon != (UnityEngine.Object) null)
        this.mUnitIcon.Icon.gameObject.SetActive(unit != null);
      if (!((UnityEngine.Object) this.mUnitIcon != (UnityEngine.Object) null))
        return;
      this.mDataSource.Clear();
      this.mDataSource.Add(typeof (UnitData), (object) unit);
      this.mUnitIcon.UpdateValue();
    }

    public void Select(UnitData unit)
    {
      if ((UnityEngine.Object) this.mSelecting != (UnityEngine.Object) null)
        this.mSelecting.SetActive(false);
      this.SetUnit(unit);
    }
  }
}
