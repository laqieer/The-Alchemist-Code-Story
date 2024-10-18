// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestShortcut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
