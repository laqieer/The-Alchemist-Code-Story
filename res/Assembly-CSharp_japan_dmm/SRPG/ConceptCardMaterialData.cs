// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardMaterialData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class ConceptCardMaterialData
  {
    private OLong mUniqueID = (OLong) 0L;
    private OString mIName;
    private OInt mNum;
    private ConceptCardParam mParam;

    public OLong UniqueID => this.mUniqueID;

    public OString IName => this.mIName;

    public OInt Num
    {
      get => this.mNum;
      set => this.mNum = value;
    }

    public ConceptCardParam Param => this.mParam;

    public bool Deserialize(JSON_ConceptCardMaterial json)
    {
      this.mUniqueID = (OLong) json.id;
      this.mIName = (OString) json.iname;
      this.mNum = (OInt) json.num;
      this.mParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
      return true;
    }
  }
}
