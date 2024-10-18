// Decompiled with JetBrains decompiler
// Type: SRPG.UIQuestSectionData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class UIQuestSectionData
  {
    private SectionParam mParam;

    public UIQuestSectionData(SectionParam param) => this.mParam = param;

    public string Name => this.mParam.name;

    public string SectionID => this.mParam.iname;
  }
}
