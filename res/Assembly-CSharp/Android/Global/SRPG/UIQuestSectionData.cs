// Decompiled with JetBrains decompiler
// Type: SRPG.UIQuestSectionData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class UIQuestSectionData
  {
    private SectionParam mParam;

    public UIQuestSectionData(SectionParam param)
    {
      this.mParam = param;
    }

    public string Name
    {
      get
      {
        return this.mParam.name;
      }
    }

    public string SectionID
    {
      get
      {
        return this.mParam.iname;
      }
    }
  }
}
