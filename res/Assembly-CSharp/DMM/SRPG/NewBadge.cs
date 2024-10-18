// Decompiled with JetBrains decompiler
// Type: SRPG.NewBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      if (Object.op_Equality((Object) this.BadgeObject, (Object) null))
        this.BadgeObject = ((Component) this).gameObject;
      NewBadgeParam dataOfClass = DataSource.FindDataOfClass<NewBadgeParam>(this.BadgeObject, (NewBadgeParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.use_newflag)
      {
        ((Component) this).gameObject.SetActive(dataOfClass.is_new);
      }
      else
      {
        bool active = ((Component) this).gameObject.GetActive();
        switch (dataOfClass.type)
        {
          default:
            ((Component) this).gameObject.SetActive(active);
            break;
        }
      }
    }

    private void Update()
    {
    }
  }
}
