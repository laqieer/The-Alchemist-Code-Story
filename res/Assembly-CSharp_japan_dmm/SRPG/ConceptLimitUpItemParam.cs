// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptLimitUpItemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ConceptLimitUpItemParam
  {
    public string iname;
    public int num;

    public static bool Desirialize(
      JSON_ConceptLimitUpItem[] jsons,
      Dictionary<string, ConceptCardParam> concept_card_params,
      Dictionary<string, List<ConceptCardParam>> concept_card_groups)
    {
      foreach (JSON_ConceptLimitUpItem json in jsons)
      {
        List<ConceptCardParam> conceptCardParamList = new List<ConceptCardParam>();
        if (json.all == 1)
        {
          foreach (KeyValuePair<string, ConceptCardParam> conceptCardParam in concept_card_params)
            ConceptLimitUpItemParam.AddParamList(conceptCardParam.Value, conceptCardParamList);
        }
        if (json.cc_groups != null && json.cc_groups.Length > 0 && concept_card_groups != null)
        {
          foreach (string ccGroup in json.cc_groups)
          {
            if (concept_card_groups.ContainsKey(ccGroup))
            {
              foreach (ConceptCardParam add_param in concept_card_groups[ccGroup])
                ConceptLimitUpItemParam.AddParamList(add_param, conceptCardParamList);
            }
          }
        }
        if (json.cards != null && json.cards.Length > 0)
        {
          foreach (string card in json.cards)
          {
            if (concept_card_params.ContainsKey(card))
              ConceptLimitUpItemParam.AddParamList(concept_card_params[card], conceptCardParamList);
          }
        }
        if (json.ignore != null && json.ignore.Length > 0)
        {
          foreach (string key in json.ignore)
          {
            if (concept_card_params.ContainsKey(key))
              ConceptLimitUpItemParam.RemoveParamList(concept_card_params[key], conceptCardParamList);
          }
        }
        if (conceptCardParamList.Count > 0)
        {
          ConceptLimitUpItemParam limitUpItemParam = new ConceptLimitUpItemParam();
          limitUpItemParam.iname = json.iname;
          limitUpItemParam.num = json.num;
          foreach (ConceptCardParam conceptCardParam in conceptCardParamList)
          {
            if (conceptCardParam.limit_up_items == null)
              conceptCardParam.limit_up_items = new List<ConceptLimitUpItemParam>();
            conceptCardParam.limit_up_items.Add(limitUpItemParam);
          }
        }
      }
      return true;
    }

    private static void AddParamList(ConceptCardParam add_param, List<ConceptCardParam> add_list)
    {
      if (add_list.Contains(add_param))
        return;
      add_list.Add(add_param);
    }

    private static void RemoveParamList(
      ConceptCardParam remove_param,
      List<ConceptCardParam> remove_list)
    {
      if (!remove_list.Contains(remove_param))
        return;
      remove_list.Remove(remove_param);
    }
  }
}
