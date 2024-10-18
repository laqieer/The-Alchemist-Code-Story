// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchHelp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RankMatchHelp : MonoBehaviour, IWebHelp
  {
    public bool GetHelpURL(out string url, out string title)
    {
      title = (string) null;
      url = (string) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
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
