// Decompiled with JetBrains decompiler
// Type: SRPG.Json_Gift
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Json_Gift
  {
    public string iname;
    public int num;
    public int rare = -1;
    public int gold;
    public int coin;
    public int arenacoin;
    public int multicoin;
    public int kakeracoin;
    public Json_GiftConceptCard concept_card;
  }
}
