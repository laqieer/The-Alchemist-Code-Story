// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GachaResultData
  {
    private static List<GachaDropData> drops_ = new List<GachaDropData>();
    private static List<GachaDropData> dropMails_ = new List<GachaDropData>();
    private static List<int> summonCoins_ = new List<int>();
    private static int[] excites_ = new int[5];
    private static bool use_one_more_ = false;
    private static int m_is_pending = 0;
    private static int m_redraw_rest = 0;
    private static int m_max_excite = 1;

    public static GachaDropData[] drops => GachaResultData.drops_.ToArray();

    public static GachaDropData[] dropMails => GachaResultData.dropMails_.ToArray();

    public static List<int> summonCoins => GachaResultData.summonCoins_;

    public static int[] excites => GachaResultData.excites_;

    public static GachaReceiptData receipt { get; private set; }

    public static bool IsCoin
    {
      get => GachaResultData.receipt == null || !(GachaResultData.receipt.type == "gold");
    }

    public static bool UseOneMore => GachaResultData.use_one_more_;

    public static bool IsPending => GachaResultData.m_is_pending == 1;

    public static int RedrawRest => GachaResultData.m_redraw_rest;

    public static bool IsRedrawGacha => GachaResultData.m_is_pending != -1;

    public static int DropMaxExcite => GachaResultData.m_max_excite;

    public static void Reset()
    {
      GachaResultData.drops_.Clear();
      GachaResultData.dropMails_.Clear();
      GachaResultData.summonCoins_.Clear();
      for (int index = 0; index < GachaResultData.excites_.Length; ++index)
        GachaResultData.excites_[index] = 1;
      GachaResultData.receipt = (GachaReceiptData) null;
      GachaResultData.use_one_more_ = false;
      GachaResultData.m_is_pending = -1;
      GachaResultData.m_redraw_rest = -1;
      GachaResultData.m_max_excite = 1;
    }

    public static void Init(
      List<GachaDropData> a_drops = null,
      List<GachaDropData> a_dropMails = null,
      List<int> a_summonCoins = null,
      GachaReceiptData a_receipt = null,
      bool a_use_onemore = false,
      int a_is_pending = -1,
      int a_redraw_rest = -1)
    {
      GachaResultData.Reset();
      if (a_drops != null)
        GachaResultData.drops_ = a_drops;
      if (a_dropMails != null)
        GachaResultData.dropMails_ = a_dropMails;
      if (a_summonCoins != null)
        GachaResultData.summonCoins_ = a_summonCoins;
      if (a_receipt != null)
        GachaResultData.receipt = a_receipt;
      if (GachaResultData.drops != null)
      {
        if (GachaResultData.drops.Length > GachaController.MAX_VIEW_STONE)
        {
          GachaResultData.drops[0].excites = GachaResultData.CalcExcitesForDrop(GachaResultData.drops[0]);
          GachaResultData.m_max_excite = GachaResultData.drops[0].FirstExcite;
        }
        else
        {
          foreach (GachaDropData drop in GachaResultData.drops_)
          {
            drop.excites = GachaResultData.CalcExcitesForDrop(drop);
            GachaResultData.m_max_excite = Math.Max(GachaResultData.m_max_excite, drop.FirstExcite);
          }
        }
      }
      GachaResultData.excites_ = GachaResultData.CalcExcites(GachaResultData.m_max_excite);
      GachaResultData.use_one_more_ = a_use_onemore;
      GachaResultData.m_is_pending = a_is_pending;
      GachaResultData.m_redraw_rest = a_redraw_rest;
    }

    public static int[] CalcExcites(int max_rarity)
    {
      return GachaExciteMaster.Select(JSONParser.parseJSONArray<Json_GachaExcite>(AssetManager.LoadTextData("Data/gacha/animation_pattern")), max_rarity + 1);
    }

    public static int[] CalcExcitesForDrop(GachaDropData a_drop)
    {
      int rare = 0;
      if (a_drop != null)
        rare = a_drop.Rare;
      return GachaExciteMaster.SelectStone(JSONParser.parseJSONArray<Json_GachaExcite>(AssetManager.LoadTextData("Data/gacha/stone_animation_pattern")), rare);
    }
  }
}
