// Decompiled with JetBrains decompiler
// Type: SRPG.SelecteConceptCardMaterial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class SelecteConceptCardMaterial
  {
    public OLong mUniqueID;
    public ConceptCardData mSelectedData;
    public int mSelectNum;

    public string iname
    {
      get => this.mSelectedData == null ? (string) null : this.mSelectedData.Param.iname;
    }
  }
}
