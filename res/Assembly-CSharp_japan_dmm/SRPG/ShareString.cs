// Decompiled with JetBrains decompiler
// Type: SRPG.ShareString
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ShareString
  {
    private List<List<string>> m_string_types = new List<List<string>>();

    public ShareString()
    {
      for (int index = 0; index < 9; ++index)
        this.m_string_types.Add(new List<string>());
    }

    public short Set(ShareString.Type type, string val)
    {
      if (string.IsNullOrEmpty(val))
        return -1;
      List<string> stringList = this.ChoiceDicitionary(type);
      short num = (short) stringList.IndexOf(val);
      if (num == (short) -1)
      {
        if (stringList.Count >= (int) short.MaxValue)
          DebugUtility.LogError("The registered character has exceeded the prescribed value. ShareString.Type = " + (object) type + ", Please change short to int.");
        num = (short) stringList.Count;
        stringList.Add(val);
      }
      return num;
    }

    public string Get(ShareString.Type type, short index)
    {
      List<string> stringList = this.ChoiceDicitionary(type);
      return index == (short) -1 || (int) index >= stringList.Count ? string.Empty : stringList[(int) index];
    }

    private List<string> ChoiceDicitionary(ShareString.Type type)
    {
      return type >= (ShareString.Type) this.m_string_types.Count ? (List<string>) null : this.m_string_types[(int) type];
    }

    public enum Type : byte
    {
      QuestParam_cond,
      QuestParam_world,
      QuestParam_area,
      QuestParam_units,
      QuestParam_ticket,
      ChapterParam_world,
      ChapterParam_section,
      MapParam_battleSceneName,
      MapParam_bgmName,
      MAX_TYPE,
    }
  }
}
