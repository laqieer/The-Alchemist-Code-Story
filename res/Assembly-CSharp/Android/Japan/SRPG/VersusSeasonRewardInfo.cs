// Decompiled with JetBrains decompiler
// Type: SRPG.VersusSeasonRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class VersusSeasonRewardInfo : MonoBehaviour
  {
    private List<GameObject> mItems = new List<GameObject>();
    public GameObject template;
    public GameObject parent;

    public void Refresh()
    {
      if ((UnityEngine.Object) this.template == (UnityEngine.Object) null)
        return;
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(this.gameObject, (VersusTowerParam) null);
      if (dataOfClass != null)
      {
        while (this.mItems.Count < dataOfClass.SeasonIteminame.Length)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.template);
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          {
            if ((UnityEngine.Object) this.parent != (UnityEngine.Object) null)
              gameObject.transform.SetParent(this.parent.transform, false);
            this.mItems.Add(gameObject);
          }
        }
        for (int idx = 0; idx < dataOfClass.SeasonIteminame.Length; ++idx)
        {
          GameObject mItem = this.mItems[idx];
          if ((UnityEngine.Object) mItem != (UnityEngine.Object) null)
          {
            DataSource.Bind<VersusTowerParam>(mItem, dataOfClass, false);
            mItem.SetActive(true);
            VersusTowerRewardItem component = mItem.GetComponent<VersusTowerRewardItem>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.Refresh(VersusTowerRewardItem.REWARD_TYPE.Season, idx);
          }
        }
      }
      this.template.SetActive(false);
    }
  }
}
