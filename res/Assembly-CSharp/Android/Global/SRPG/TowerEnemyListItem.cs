// Decompiled with JetBrains decompiler
// Type: SRPG.TowerEnemyListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

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
      if (!((UnityEngine.Object) this.DisableIcon != (UnityEngine.Object) null))
        return;
      this.DisableIcon.SetActive(false);
    }

    private void Update()
    {
    }

    private void Refresh()
    {
      Unit dataOfClass = DataSource.FindDataOfClass<Unit>(this.gameObject, (Unit) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.IsDead)
        this.mCanvasGroup.alpha = 0.5f;
      else
        this.mCanvasGroup.alpha = 1f;
    }

    public void UpdateValue()
    {
      this.Refresh();
    }
  }
}
