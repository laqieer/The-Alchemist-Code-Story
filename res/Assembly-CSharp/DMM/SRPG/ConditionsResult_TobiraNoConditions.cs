// Decompiled with JetBrains decompiler
// Type: SRPG.ConditionsResult_TobiraNoConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ConditionsResult_TobiraNoConditions : ConditionsResult
  {
    public ConditionsResult_TobiraNoConditions() => this.mIsClear = true;

    public override string text => LocalizedText.Get("sys.TOBIRA_CONDITIONS_NOTHING");

    public override string errorText => LocalizedText.Get("sys.ITEM_NOT_ENOUGH");
  }
}
