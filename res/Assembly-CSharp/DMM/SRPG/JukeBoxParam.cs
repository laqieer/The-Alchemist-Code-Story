// Decompiled with JetBrains decompiler
// Type: SRPG.JukeBoxParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JukeBoxParam
  {
    [IgnoreMember]
    private const string JUKUBOX_TITLE_SUFFIX = "_TITLE";
    [IgnoreMember]
    private const string JUKUBOX_TITLE_EN_SUFFIX = "_TITLE_EN";
    [IgnoreMember]
    private const string JUKUBOX_LYRICIST_SUFFIX = "_LYRICIST";
    [IgnoreMember]
    private const string JUKUBOX_COMPOSER_SUFFIX = "_COMPOSER";
    [IgnoreMember]
    private const string JUKUBOX_SITUATION_SUFFIX = "_SITUATION";
    [IgnoreMember]
    private const string JUKUBOX_TEXT_TABLE = "external_jukebox";
    private string iname;
    private string sheet;
    private string cue;
    private string section;
    private bool default_unlock;
    private int external_link;
    private int rate;
    private JukeBoxParam.eUnlockType unlock_type;
    private string[] quests;
    private string cond_quest;

    public string Iname => this.iname;

    public string Sheet => this.sheet;

    public string Cue => this.cue;

    public string SectionId => this.section;

    public string Title => JukeBoxParam.GetText("external_jukebox", this.iname + "_TITLE");

    public string TitleEn => JukeBoxParam.GetText("external_jukebox", this.iname + "_TITLE_EN");

    public string Lyricist => JukeBoxParam.GetText("external_jukebox", this.iname + "_LYRICIST");

    public string Composer => JukeBoxParam.GetText("external_jukebox", this.iname + "_COMPOSER");

    public string Situation => JukeBoxParam.GetText("external_jukebox", this.iname + "_SITUATION");

    public bool DefaultUnlock => this.default_unlock;

    public int ExternalLink => this.external_link;

    public int Rate => this.rate;

    public JukeBoxParam.eUnlockType UnlockType => this.unlock_type;

    public List<string> CondList
    {
      get
      {
        return this.quests != null ? new List<string>((IEnumerable<string>) this.quests) : new List<string>();
      }
    }

    public string CondQuest => this.cond_quest;

    public bool Deserialize(JSON_JukeBoxParam json)
    {
      this.iname = json.iname;
      this.sheet = json.sheet;
      this.cue = json.cue;
      this.section = json.section;
      this.default_unlock = json.default_unlock != 0;
      this.external_link = json.external_link;
      this.rate = json.rate;
      this.unlock_type = (JukeBoxParam.eUnlockType) json.range_unit;
      this.quests = (string[]) null;
      if (json.quests != null && json.quests.Length != 0)
      {
        this.quests = new string[json.quests.Length];
        for (int index = 0; index < json.quests.Length; ++index)
          this.quests[index] = json.quests[index];
      }
      this.cond_quest = json.cond_quest;
      return true;
    }

    public static string GetText(string table, string key)
    {
      string str = LocalizedText.Get(table + "." + key);
      return str.Equals(key) ? string.Empty : str;
    }

    public static void Deserialize(ref List<JukeBoxParam> ref_params, JSON_JukeBoxParam[] json)
    {
      if (ref_params == null)
        ref_params = new List<JukeBoxParam>();
      ref_params.Clear();
      if (json == null)
        return;
      foreach (JSON_JukeBoxParam json1 in json)
      {
        JukeBoxParam jukeBoxParam = new JukeBoxParam();
        jukeBoxParam.Deserialize(json1);
        ref_params.Add(jukeBoxParam);
      }
    }

    public enum eUnlockType
    {
      QUEST,
      AREA,
    }
  }
}
