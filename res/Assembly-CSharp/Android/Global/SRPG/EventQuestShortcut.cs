// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestShortcut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class EventQuestShortcut : MonoBehaviour
  {
    public GameObject KeyQuestOpenEffect;

    private void Start()
    {
      this.RefreshSwitchButton();
    }

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
      if (!((UnityEngine.Object) this.KeyQuestOpenEffect != (UnityEngine.Object) null))
        return;
      this.KeyQuestOpenEffect.SetActive(flag);
    }
  }
}
