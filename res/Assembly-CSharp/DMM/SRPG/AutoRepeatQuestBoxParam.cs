// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestBoxParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class AutoRepeatQuestBoxParam
  {
    private int mSize;
    private int mCoin;

    public int Size => this.mSize;

    public int Coin => this.mCoin;

    public void Deserialize(JSON_AutoRepeatQuestBoxParam json)
    {
      if (json == null)
        return;
      this.mSize = json.size;
      this.mCoin = json.coin;
    }

    public static void Deserialize(
      ref AutoRepeatQuestBoxParam[] param,
      JSON_AutoRepeatQuestBoxParam[] json)
    {
      if (json == null)
        return;
      param = new AutoRepeatQuestBoxParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        AutoRepeatQuestBoxParam repeatQuestBoxParam = new AutoRepeatQuestBoxParam();
        repeatQuestBoxParam.Deserialize(json[index]);
        param[index] = repeatQuestBoxParam;
      }
    }
  }
}
