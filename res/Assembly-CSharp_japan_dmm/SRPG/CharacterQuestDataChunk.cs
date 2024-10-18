// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestDataChunk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class CharacterQuestDataChunk
  {
    public string areaName;
    public string unitName;
    public UnitParam unitParam;
    public List<QuestParam> questParams = new List<QuestParam>();

    public void SetUnitNameFromChapterID(string chapterID) => this.unitName = chapterID;
  }
}
