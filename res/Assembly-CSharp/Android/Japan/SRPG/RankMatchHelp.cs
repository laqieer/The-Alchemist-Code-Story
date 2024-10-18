// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchHelp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
