// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEffectDecreaseInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ConceptCardEffectDecreaseInfo
  {
    public const int DECREASE_RATE_DEFAULT = 0;
    public ConceptCardData m_ConceptCardData;
    public bool m_IsDecreaseEffect;

    public ConceptCardEffectDecreaseInfo(ConceptCardData conceptCard, bool isDecreaseEffect)
    {
      this.m_ConceptCardData = conceptCard;
      this.m_IsDecreaseEffect = isDecreaseEffect;
    }
  }
}
