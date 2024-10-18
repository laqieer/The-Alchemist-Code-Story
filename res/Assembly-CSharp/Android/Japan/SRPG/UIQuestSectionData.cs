// Decompiled with JetBrains decompiler
// Type: SRPG.UIQuestSectionData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
