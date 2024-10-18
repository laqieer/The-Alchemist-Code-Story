// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardTrustRewardItemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardTrustRewardItemParam
  {
    public eRewardType reward_type;
    public string iname;
    public int reward_num;

    public bool Deserialize(JSON_ConceptCardTrustRewardItemParam json)
    {
      this.reward_type = (eRewardType) json.reward_type;
      this.iname = json.reward_iname;
      this.reward_num = json.reward_num;
      return true;
    }

    public string GetItemName()
    {
      switch (this.reward_type)
      {
        case eRewardType.Item:
          return MonoSingleton<GameManager>.Instance.GetItemParam(this.iname).name;
        case eRewardType.Artifact:
          return MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.iname).name;
        case eRewardType.ConceptCard:
          return MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(this.iname).name;
        default:
          return string.Empty;
      }
    }

    public string GetIconPath()
    {
      switch (this.reward_type)
      {
        case eRewardType.Item:
          return AssetPath.ItemIcon(MonoSingleton<GameManager>.Instance.GetItemParam(this.iname));
        case eRewardType.Artifact:
          return AssetPath.ArtifactIcon(MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.iname));
        case eRewardType.ConceptCard:
          return AssetPath.ConceptCardIcon(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(this.iname));
        default:
          return string.Empty;
      }
    }

    public Sprite GetFrameSprite()
    {
      switch (this.reward_type)
      {
        case eRewardType.Item:
          return GameSettings.Instance.GetItemFrame(MonoSingleton<GameManager>.Instance.GetItemParam(this.iname));
        case eRewardType.Artifact:
          return this.GetArtifactSprite(MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.iname));
        case eRewardType.ConceptCard:
          return this.GetConceptCardSprite(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(this.iname));
        default:
          return (Sprite) null;
      }
    }

    public Sprite GetArtifactSprite(ArtifactParam param)
    {
      if (param == null)
        return (Sprite) null;
      int rareini = param.rareini;
      Sprite[] artifactIconFrames = GameSettings.Instance.ArtifactIcon_Frames;
      return rareini >= 0 && rareini < artifactIconFrames.Length ? artifactIconFrames[rareini] : (Sprite) null;
    }

    public Sprite GetConceptCardSprite(ConceptCardParam param)
    {
      if (param == null)
        return (Sprite) null;
      Sprite[] conceptCardIconRarity = GameSettings.Instance.ConceptCardIcon_Rarity;
      return param.rare >= 0 && param.rare < conceptCardIconRarity.Length ? conceptCardIconRarity[param.rare] : (Sprite) null;
    }
  }
}
