// Decompiled with JetBrains decompiler
// Type: SRPG.Json_PlayerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Json_PlayerData
  {
    public string name;
    public int exp;
    public int lv;
    public int gold;
    public int abilpt;
    public Json_Coin coin;
    public int arenacoin;
    public int multicoin;
    public int enseicoin;
    public int kakeracoin;
    public int cnt_multi;
    public int cnt_stmrecover;
    public int cnt_buygold;
    public string cuid;
    public string fuid;
    public int logincont;
    public int mail_unread;
    public int mail_f_unread;
    public long btlid;
    public string btltype;
    public Json_Hikkoshi tuid;
    public Json_Stamina stamina;
    public Json_Cave cave;
    public Json_AbilityUp abilup;
    public Json_Arena arena;
    public Json_Tour tour;
    public Json_Vip vip;
    public Json_Premium premium;
    public int unitbox_max;
    public int itembox_max;
    public Json_FreeGacha gachag;
    public Json_FreeGacha gachac;
    public Json_PaidGacha gachap;
    public Json_Friends friends;
    public int newgame_at;
    public string selected_award;
    public Json_MultiOption multi;
    public Json_GuerrillaShopPeriod g_shop;
  }
}
