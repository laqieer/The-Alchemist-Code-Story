// Decompiled with JetBrains decompiler
// Type: SRPG.FriendFoundWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class FriendFoundWindow : MonoBehaviour
  {
    private void Awake()
    {
      FriendData foundFriend = GlobalVars.FoundFriend;
      if (foundFriend != null)
        DataSource.Bind<FriendData>(this.gameObject, foundFriend);
      UnitData unit = foundFriend.Unit;
      if (unit == null)
        return;
      DataSource.Bind<UnitData>(this.gameObject, unit);
    }
  }
}
