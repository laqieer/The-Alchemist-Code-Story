// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchHelp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class RankMatchHelp : MonoBehaviour, IWebHelp
  {
    public bool GetHelpURL(out string url, out string title)
    {
      title = (string) null;
      url = (string) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return false;
      VersusRankParam versusRankParam = instance.GetVersusRankParam(instance.RankMatchScheduleId);
      if (versusRankParam == null || string.IsNullOrEmpty(versusRankParam.HelpURL))
        return false;
      title = versusRankParam.Name;
      url = versusRankParam.HelpURL;
      return true;
    }
  }
}
