// Decompiled with JetBrains decompiler
// Type: SRPG.RaidRescueListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class RaidRescueListItem : MonoBehaviour
  {
    private int mIndex;

    public int Index
    {
      get
      {
        return this.mIndex;
      }
    }

    public void Setup(int index, RaidRescueMember member)
    {
      this.mIndex = index;
      DataSource.Bind<RaidRescueMember>(this.gameObject, member, false);
    }
  }
}
