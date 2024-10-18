// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementBridge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AchievementBridge : MonoBehaviour
  {
    public void OnClick()
    {
      if (GameCenterManager.IsAuth())
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        if (Object.op_Inequality((Object) instanceDirect, (Object) null))
          instanceDirect.Player.UpdateAchievementTrophyStates();
        GameCenterManager.ShowAchievement();
      }
      else
        GameCenterManager.ReAuth();
    }
  }
}
