// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardGroupParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ConceptCardGroupParam
  {
    public static bool Deserialize(
      JSON_ConceptCardGroup[] json_groups,
      Dictionary<string, ConceptCardParam> concept_card_params,
      ref Dictionary<string, List<ConceptCardParam>> ref_params)
    {
      ref_params = new Dictionary<string, List<ConceptCardParam>>();
      foreach (JSON_ConceptCardGroup jsonGroup in json_groups)
      {
        if (jsonGroup.cards != null && jsonGroup.cards.Length > 0 && !ref_params.ContainsKey(jsonGroup.iname))
        {
          List<ConceptCardParam> conceptCardParamList = new List<ConceptCardParam>();
          for (int index = 0; index < jsonGroup.cards.Length; ++index)
          {
            ConceptCardParam conceptCardParam = (ConceptCardParam) null;
            if (concept_card_params.ContainsKey(jsonGroup.cards[index]))
              conceptCardParam = concept_card_params[jsonGroup.cards[index]];
            if (conceptCardParam != null)
              conceptCardParamList.Add(conceptCardParam);
          }
          if (conceptCardParamList.Count > 0)
            ref_params[jsonGroup.iname] = conceptCardParamList;
        }
      }
      return true;
    }
  }
}
