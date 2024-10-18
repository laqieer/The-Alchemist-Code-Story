// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ConceptCardTrustRewardParam
  {
    public string iname;
    public ConceptCardTrustRewardItemParam[] rewards;

    public bool Deserialize(JSON_ConceptCardTrustRewardParam json)
    {
      this.iname = json.iname;
      if (json.rewards != null)
      {
        this.rewards = new ConceptCardTrustRewardItemParam[json.rewards.Length];
        for (int index = 0; index < json.rewards.Length; ++index)
        {
          ConceptCardTrustRewardItemParam trustRewardItemParam = new ConceptCardTrustRewardItemParam();
          if (!trustRewardItemParam.Deserialize(json.rewards[index]))
            return false;
          this.rewards[index] = trustRewardItemParam;
        }
      }
      return true;
    }
  }
}
