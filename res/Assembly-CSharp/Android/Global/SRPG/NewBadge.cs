// Decompiled with JetBrains decompiler
// Type: SRPG.NewBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class NewBadge : MonoBehaviour
  {
    [SerializeField]
    private GameObject BadgeObject;
    [SerializeField]
    public NewBadgeType SelectBadgeType;

    private void Start()
    {
      if ((UnityEngine.Object) this.BadgeObject == (UnityEngine.Object) null)
        this.BadgeObject = this.gameObject;
      NewBadgeParam dataOfClass = DataSource.FindDataOfClass<NewBadgeParam>(this.BadgeObject, (NewBadgeParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.use_newflag)
      {
        this.gameObject.SetActive(dataOfClass.is_new);
      }
      else
      {
        bool active = this.gameObject.GetActive();
        switch (dataOfClass.type)
        {
          default:
            this.gameObject.SetActive(active);
            break;
        }
      }
    }

    private void Update()
    {
    }
  }
}
