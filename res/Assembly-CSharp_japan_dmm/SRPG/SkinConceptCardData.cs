// Decompiled with JetBrains decompiler
// Type: SRPG.SkinConceptCardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class SkinConceptCardData
  {
    public string iname;
    public ConceptCardParam param;

    public bool Deserialize(string _iname)
    {
      this.iname = _iname;
      this.param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(_iname);
      return true;
    }
  }
}
