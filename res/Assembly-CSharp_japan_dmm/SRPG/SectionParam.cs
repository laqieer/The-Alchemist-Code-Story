// Decompiled with JetBrains decompiler
// Type: SRPG.SectionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class SectionParam
  {
    public string iname;
    public string name;
    public string expr;
    public long start;
    public long end;
    public bool hidden;
    public string home;
    public string unit;
    public string prefabPath;
    public string shop;
    public string inn;
    public string bar;
    public string bgm;
    public int storyPart;
    public string releaseKeyQuest;
    public string message_sys_id;
    public const string SEISEKI_SECTION_INAME = "WD_SEISEKI";
    public const string BABEL_SECTION_INAME = "WD_BABEL";
    public const string DAILY_SECTION_INAME = "WD_DAILY";
    public const string GENESIS_SECTION_INAME = "WD_GENESIS";
    public const string ADVANCE_SECTION_INAME = "WD_ADVANCE";
    public const string RENTAL_SECTION_INAME = "WD_RENTAL";

    public void Deserialize(JSON_SectionParam json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
      this.name = json.name;
      this.expr = json.expr;
      this.start = json.start;
      this.end = json.end;
      this.hidden = json.hide != 0;
      this.home = json.home;
      this.unit = json.unit;
      this.prefabPath = json.item;
      this.shop = json.shop;
      this.inn = json.inn;
      this.bar = json.bar;
      this.bgm = json.bgm;
      this.storyPart = json.story_part;
      this.releaseKeyQuest = json.release_key_quest;
      this.message_sys_id = json.message_sys_id;
    }

    public bool IsDateUnlock()
    {
      long serverTime = Network.GetServerTime();
      if (this.end == 0L)
        return !this.hidden;
      return this.start <= serverTime && serverTime < this.end;
    }
  }
}
