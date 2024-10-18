// Decompiled with JetBrains decompiler
// Type: SRPG.GvGUsedData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGUsedData
  {
    public List<int> Artifacts;
    public List<int> Cards;
    public List<int> Runes;

    public void Deserialize(int[] artifacts, int[] cards, int[] runes)
    {
      if (artifacts != null)
      {
        if (this.Artifacts == null)
          this.Artifacts = new List<int>();
        for (int index = 0; index < artifacts.Length; ++index)
        {
          if (!this.Artifacts.Contains(artifacts[index]))
            this.Artifacts.Add(artifacts[index]);
        }
      }
      if (cards != null)
      {
        if (this.Cards == null)
          this.Cards = new List<int>();
        for (int index = 0; index < cards.Length; ++index)
        {
          if (!this.Cards.Contains(cards[index]))
            this.Cards.Add(cards[index]);
        }
      }
      if (runes == null)
        return;
      if (this.Runes == null)
        this.Runes = new List<int>();
      for (int index = 0; index < runes.Length; ++index)
      {
        if (!this.Runes.Contains(runes[index]))
          this.Runes.Add(runes[index]);
      }
    }

    public bool IsArtifactUsed(long id)
    {
      return this.Artifacts != null && this.Artifacts.Contains((int) id);
    }

    public bool IsConceptCardUsed(long id) => this.Cards != null && this.Cards.Contains((int) id);

    public bool IsRuneUsed(long id) => this.Runes != null && this.Runes.Contains((int) id);

    public static string CreateUsedItemsStr(JSON_GvGUsedItems[] items)
    {
      string usedItemsStr = string.Empty;
      string str = string.Empty;
      bool flag = false;
      if (items == null)
        return string.Empty;
      for (int i = 0; i < items.Length; ++i)
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) items[i].uiid);
        if (unitDataByUniqueId == null || unitDataByUniqueId.UnitParam == null)
        {
          DebugUtility.LogError("CreateUsedItems:uiidがおかしい");
        }
        else
        {
          string name = unitDataByUniqueId.UnitParam.name;
          string empty = string.Empty;
          if (items[i].artifacts != null)
          {
            for (int j = 0; j < items[i].artifacts.Length; ++j)
            {
              ArtifactData artifactData = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find((Predicate<ArtifactData>) (p => (long) p.UniqueID == (long) items[i].artifacts[j]));
              if (artifactData == null || artifactData.ArtifactParam == null)
              {
                DebugUtility.LogError("CreateUsedItems:artifactsのUniqueIDがおかしい");
              }
              else
              {
                empty += string.Format(LocalizedText.Get("sys.GVG_USED_ITEMLIST_ARTI"), (object) artifactData.ArtifactParam.name);
                flag = true;
              }
            }
          }
          if (items[i].cards != null)
          {
            for (int j = 0; j < items[i].cards.Length; ++j)
            {
              ConceptCardData conceptCardData = MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find((Predicate<ConceptCardData>) (p => (long) p.UniqueID == (long) items[i].cards[j]));
              if (conceptCardData == null || conceptCardData.Param == null)
              {
                DebugUtility.LogError("CreateUsedItems:cardsのUniqueIDがおかしい");
              }
              else
              {
                empty += string.Format(LocalizedText.Get("sys.GVG_USED_ITEMLIST_CARD"), (object) conceptCardData.Param.name);
                flag = true;
              }
            }
          }
          if (items[i].runes != null)
          {
            for (int index = 0; index < items[i].runes.Length; ++index)
            {
              RuneData rune = MonoSingleton<GameManager>.Instance.Player.Runes[(long) items[i].runes[index]];
              if (rune == null || rune.Item == null)
              {
                DebugUtility.LogError("CreateUsedItems:runeのキーかitemparamがおかしい");
              }
              else
              {
                empty += string.Format(LocalizedText.Get("sys.GVG_USED_ITEMLIST_RUNE"), (object) rune.Item.name);
                flag = true;
              }
            }
          }
          if (flag)
            str = str + string.Format(LocalizedText.Get("sys.GVG_USED_UNIT_CONFIRM"), (object) name) + string.Format(LocalizedText.Get("sys.GVG_USED_ITEM_CONFIRM"), (object) empty);
        }
      }
      if (flag)
        usedItemsStr = usedItemsStr + LocalizedText.Get("sys.GVG_USED_BEGIN_CONFIRM") + str + LocalizedText.Get("sys.GVG_USED_CONFIRM");
      return usedItemsStr;
    }
  }
}
