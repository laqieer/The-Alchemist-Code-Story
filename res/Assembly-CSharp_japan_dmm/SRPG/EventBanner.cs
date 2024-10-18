// Decompiled with JetBrains decompiler
// Type: SRPG.EventBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventBanner : MonoBehaviour
  {
    private RawImage mTarget;
    public float UpdateInterval = 4f;
    private float mInterval;
    private string mLastBannerName;
    private LoadRequest mBannerLoadRequest;

    private void Start()
    {
      this.mTarget = ((Component) this).GetComponent<RawImage>();
      this.mInterval = this.UpdateInterval;
      this.Update();
    }

    private void Update()
    {
      if ((double) this.UpdateInterval <= 0.0)
        return;
      if (this.mBannerLoadRequest != null)
      {
        if (!this.mBannerLoadRequest.isDone)
          return;
        if (Object.op_Inequality((Object) this.mTarget, (Object) null))
          this.mTarget.texture = (Texture) (this.mBannerLoadRequest.asset as Texture2D);
        this.mBannerLoadRequest = (LoadRequest) null;
      }
      this.mInterval += Time.unscaledDeltaTime;
      if ((double) this.mInterval < (double) this.UpdateInterval)
        return;
      this.mInterval = 0.0f;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<string> stringList = new List<string>();
      ChapterParam[] chapters = instance.Chapters;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      for (int index1 = 0; index1 < chapters.Length; ++index1)
      {
        bool flag = false;
        string banner = chapters[index1].banner;
        if (!string.IsNullOrEmpty(banner) && !stringList.Contains(banner))
        {
          for (int index2 = 0; index2 < availableQuests.Length; ++index2)
          {
            if (availableQuests[index2].ChapterID == chapters[index1].iname && availableQuests[index2].IsDateUnlock(serverTime))
            {
              flag = true;
              break;
            }
          }
          if (flag)
            stringList.Add(banner);
        }
      }
      if (stringList.Count <= 0)
        return;
      int index = (stringList.IndexOf(this.mLastBannerName) + 1) % stringList.Count;
      this.mLastBannerName = stringList[index];
      this.mBannerLoadRequest = AssetManager.LoadAsync<Texture2D>("Banners/" + this.mLastBannerName);
    }
  }
}
