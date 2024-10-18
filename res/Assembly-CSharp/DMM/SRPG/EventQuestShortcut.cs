// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestShortcut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class EventQuestShortcut : MonoBehaviour
  {
    public GameObject KeyQuestOpenEffect;

    private void Start() => this.RefreshSwitchButton();

    private void RefreshSwitchButton()
    {
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      bool flag = false;
      if (chapters != null)
      {
        long serverTime = Network.GetServerTime();
        for (int index = 0; index < chapters.Length; ++index)
        {
          if (chapters[index].IsKeyQuest() && chapters[index].IsKeyUnlock(serverTime))
            flag = true;
        }
      }
      if (!Object.op_Inequality((Object) this.KeyQuestOpenEffect, (Object) null))
        return;
      this.KeyQuestOpenEffect.SetActive(flag);
    }
  }
}
