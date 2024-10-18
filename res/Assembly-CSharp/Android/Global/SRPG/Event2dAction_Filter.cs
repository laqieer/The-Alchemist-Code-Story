// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Filter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

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
