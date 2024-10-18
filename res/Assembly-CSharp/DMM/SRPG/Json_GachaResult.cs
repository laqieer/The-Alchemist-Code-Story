// Decompiled with JetBrains decompiler
// Type: SRPG.Json_GachaResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class Json_GachaResult
  {
    public Json_DropInfo[] add;
    public Json_DropInfo[] add_mail;
    public Json_GachaReceipt receipt;
    public Json_PlayerData player;
    public Json_Item[] items;
    public Json_Unit[] units;
    public Json_Mail[] mails;
    public Json_Artifact[] artifacts;
    public int is_pending = -1;
    public int rest = -1;
    public JSON_TrophyProgress[] trophyprogs;
    public JSON_TrophyProgress[] bingoprogs;
    public Json_Item[] runes;
    public int rune_storage_used;
  }
}
