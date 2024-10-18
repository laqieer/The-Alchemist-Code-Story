// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Filter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("New/フィルタ(2D)", "画面に効果を適応します", 5592405, 4473992)]
  public class Event2dAction_Filter : EventAction
  {
    public Event2dAction_Filter.FilterType filter;

    public override void OnActivate()
    {
      switch (this.filter)
      {
        case Event2dAction_Filter.FilterType.None:
          Shader.DisableKeyword("EVENT_SEPIA_ON");
          Shader.DisableKeyword("EVENT_MONOCHROME_ON");
          break;
        case Event2dAction_Filter.FilterType.Monochrome:
          Shader.DisableKeyword("EVENT_SEPIA_ON");
          Shader.EnableKeyword("EVENT_MONOCHROME_ON");
          break;
        case Event2dAction_Filter.FilterType.Sepia:
          Shader.EnableKeyword("EVENT_SEPIA_ON");
          Shader.DisableKeyword("EVENT_MONOCHROME_ON");
          break;
      }
      this.ActivateNext();
    }

    public enum FilterType
    {
      None,
      Monochrome,
      Sepia,
    }
  }
}
