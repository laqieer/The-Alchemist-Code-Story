// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardMaterialData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ConceptCardMaterialData
  {
    private OLong mUniqueID = (OLong) 0L;
    private OString mIName;
    private OInt mNum;
    private ConceptCardParam mParam;

    public OLong UniqueID
    {
      get
      {
        return this.mUniqueID;
      }
    }

    public OString IName
    {
      get
      {
        return this.mIName;
      }
    }

    public OInt Num
    {
      get
      {
        return this.mNum;
      }
      set
      {
        this.mNum = value;
      }
    }

    public ConceptCardParam Param
    {
      get
      {
        return this.mParam;
      }
    }

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
