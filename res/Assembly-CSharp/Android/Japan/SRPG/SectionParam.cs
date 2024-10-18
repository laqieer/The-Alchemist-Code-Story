// Decompiled with JetBrains decompiler
// Type: SRPG.SectionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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

    public void Deserialize(JSON_SectionParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
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
