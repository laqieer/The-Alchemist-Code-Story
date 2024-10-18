// Decompiled with JetBrains decompiler
// Type: SRPG.VersusSeasonRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class VersusSeasonRewardInfo : MonoBehaviour
  {
    public GameObject template;
    public GameObject parent;
    private List<GameObject> mItems = new List<GameObject>();

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.template, (Object) null))
        return;
      VersusTowerParam dataOfClass = DataSource.FindDataOfClass<VersusTowerParam>(((Component) this).gameObject, (VersusTowerParam) null);
      if (dataOfClass != null)
      {
        while (this.mItems.Count < dataOfClass.SeasonIteminame.Length)
        {
          GameObject gameObject = Object.Instantiate<GameObject>(this.template);
          if (Object.op_Inequality((Object) gameObject, (Object) null))
          {
            if (Object.op_Inequality((Object) this.parent, (Object) null))
              gameObject.transform.SetParent(this.parent.transform, false);
            this.mItems.Add(gameObject);
          }
        }
        for (int index = 0; index < dataOfClass.SeasonIteminame.Length; ++index)
        {
          GameObject mItem = this.mItems[index];
          if (Object.op_Inequality((Object) mItem, (Object) null))
          {
            DataSource.Bind<VersusTowerParam>(mItem, dataOfClass);
            mItem.SetActive(true);
            VersusTowerRewardItem component = mItem.GetComponent<VersusTowerRewardItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.Refresh(VersusTowerRewardItem.REWARD_TYPE.Season, index);
          }
        }
      }
      this.template.SetActive(false);
    }
  }
}
