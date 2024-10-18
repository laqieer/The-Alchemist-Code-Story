// Decompiled with JetBrains decompiler
// Type: SRPG.TowerEnemyListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TowerEnemyListItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject DisableIcon;
    [SerializeField]
    private CanvasGroup mCanvasGroup;

    private void Start()
    {
      this.UpdateValue();
      this.Refresh();
      if (!Object.op_Inequality((Object) this.DisableIcon, (Object) null))
        return;
      this.DisableIcon.SetActive(false);
    }

    private void Update()
    {
    }

    private void Refresh()
    {
      Unit dataOfClass = DataSource.FindDataOfClass<Unit>(((Component) this).gameObject, (Unit) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.IsDead)
        this.mCanvasGroup.alpha = 0.5f;
      else
        this.mCanvasGroup.alpha = 1f;
    }

    public void UpdateValue() => this.Refresh();
  }
}
