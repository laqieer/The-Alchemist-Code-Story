// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_TobiraNoConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ConditionsResult_TobiraNoConditions : ConditionsResult
  {
    public ConditionsResult_TobiraNoConditions()
    {
      this.mIsClear = true;
    }

    public override string text
    {
      get
      {
        return LocalizedText.Get("sys.TOBIRA_CONDITIONS_NOTHING");
      }
    }

    public override string errorText
    {
      get
      {
        return LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
      }
    }
  }
}
