// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementBridge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class AchievementBridge : MonoBehaviour
  {
    public void OnClick()
    {
      if (GameCenterManager.IsAuth())
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        if ((UnityEngine.Object) instanceDirect != (UnityEngine.Object) null)
          instanceDirect.Player.UpdateAchievementTrophyStates();
        GameCenterManager.ShowAchievement();
      }
      else
        GameCenterManager.ReAuth();
    }
  }
}
