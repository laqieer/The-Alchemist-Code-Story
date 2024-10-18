// Decompiled with JetBrains decompiler
// Type: SRPG.FriendFoundWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class FriendFoundWindow : MonoBehaviour
  {
    private void Awake()
    {
      FriendData foundFriend = GlobalVars.FoundFriend;
      if (foundFriend != null)
        DataSource.Bind<FriendData>(((Component) this).gameObject, foundFriend);
      UnitData unit = foundFriend.Unit;
      if (unit == null)
        return;
      DataSource.Bind<UnitData>(((Component) this).gameObject, unit);
    }
  }
}
