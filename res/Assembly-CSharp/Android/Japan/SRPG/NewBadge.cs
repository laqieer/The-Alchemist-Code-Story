// Decompiled with JetBrains decompiler
// Type: SRPG.NewBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
