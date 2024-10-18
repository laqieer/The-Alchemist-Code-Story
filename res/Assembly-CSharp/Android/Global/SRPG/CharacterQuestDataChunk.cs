// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestDataChunk
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class CharacterQuestDataChunk
  {
    public List<QuestParam> questParams = new List<QuestParam>();
    public string areaName;
    public string unitName;
    public UnitParam unitParam;

    public void SetUnitNameFromChapterID(string chapterID)
    {
      this.unitName = chapterID;
    }
  }
}
