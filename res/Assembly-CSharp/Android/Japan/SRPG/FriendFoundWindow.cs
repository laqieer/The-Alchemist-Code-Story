// Decompiled with JetBrains decompiler
// Type: SRPG.FriendFoundWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class FriendFoundWindow : MonoBehaviour
  {
    private void Awake()
    {
      FriendData foundFriend = GlobalVars.FoundFriend;
      if (foundFriend != null)
        DataSource.Bind<FriendData>(this.gameObject, foundFriend, false);
      UnitData unit = foundFriend.Unit;
      if (unit == null)
        return;
      DataSource.Bind<UnitData>(this.gameObject, unit, false);
    }
  }
}
