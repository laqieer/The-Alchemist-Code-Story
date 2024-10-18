// Decompiled with JetBrains decompiler
// Type: SRPG.Json_GachaResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_GachaResult
  {
    public int is_pending = -1;
    public int rest = -1;
    public Json_DropInfo[] add;
    public Json_DropInfo[] add_mail;
    public Json_GachaReceipt receipt;
    public Json_PlayerData player;
    public Json_Item[] items;
    public Json_Unit[] units;
    public Json_Mail[] mails;
    public Json_Artifact[] artifacts;
    public JSON_TrophyProgress[] trophyprogs;
    public JSON_TrophyProgress[] bingoprogs;
  }
}
