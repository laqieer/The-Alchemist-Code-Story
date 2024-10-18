// Decompiled with JetBrains decompiler
// Type: SRPG.ArchiveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class ArchiveParam
  {
    public string iname;
    public string area_iname;
    public string area_iname_multi;
    public ArchiveTypes type;
    public DateTime begin_at;
    public DateTime end_at;
    public List<KeyItem> keys;
    public int keytime;
    public string unit1;
    public string unit2;
    public ArchiveItemsParam[] items;

    public void Deserialize(JSON_ArchiveParam json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
      this.area_iname = json.area_iname;
      this.area_iname_multi = json.area_iname_multi;
      this.type = (ArchiveTypes) json.type;
      this.begin_at = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.begin_at);
      this.end_at = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out this.end_at);
      this.keys = new List<KeyItem>();
      if (!string.IsNullOrEmpty(json.keyitem1) && json.keynum1 > 0)
        this.keys.Add(new KeyItem()
        {
          iname = json.keyitem1,
          num = json.keynum1
        });
      this.keytime = json.keytime;
      this.unit1 = json.unit1;
      this.unit2 = json.unit2;
      if (json.items == null)
        return;
      this.items = new ArchiveItemsParam[json.items.Length];
      int index = 0;
      foreach (JSON_ArchiveItemsParam archiveItemsParam in json.items)
      {
        this.items[index] = new ArchiveItemsParam();
        this.items[index].type = (ArchiveItemTypes) archiveItemsParam.type;
        this.items[index++].id = archiveItemsParam.id;
      }
    }

    public bool IsAvailable()
    {
      DateTime serverTime = TimeManager.ServerTime;
      return this.begin_at <= serverTime && serverTime < this.end_at;
    }

    public bool ArePrerequsiteQuestsCleared()
    {
      string iname = string.Empty;
      if (!string.IsNullOrEmpty(this.area_iname))
        iname = this.area_iname;
      else if (!string.IsNullOrEmpty(this.area_iname_multi))
        iname = this.area_iname_multi;
      ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(iname);
      if (area == null || area.iname == null || area.quests == null)
        return false;
      bool flag = false;
      for (int index1 = 0; index1 < area.quests.Count && !flag; ++index1)
      {
        QuestParam quest1 = area.quests[index1];
        if (!quest1.hidden)
        {
          if (quest1.cond_quests == null || quest1.cond_quests.Length == 0)
          {
            flag = true;
            break;
          }
          for (int index2 = 0; index2 < quest1.cond_quests.Length; ++index2)
          {
            QuestParam quest2 = MonoSingleton<GameManager>.Instance.FindQuest(quest1.cond_quests[index2]);
            if (quest2 != null && quest2.iname != null && MonoSingleton<GameManager>.Instance.Player.IsQuestCleared(quest2.iname))
            {
              flag = true;
              break;
            }
          }
        }
      }
      return flag;
    }
  }
}
