// Decompiled with JetBrains decompiler
// Type: SRPG.SortRuneConditionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class SortRuneConditionParam
  {
    public SortRuneParam parent;
    public string cnds_iname;
    public string name;
    public eRuneSortType sort_type;

    public SortRuneConditionParam(SortRuneParam _parent) => this.parent = _parent;

    public void Deserialize(JSON_SortRuneConditionParam json)
    {
      this.cnds_iname = json.cnds_iname;
      this.name = json.name;
      this.sort_type = (eRuneSortType) json.sort_type;
    }

    public string PrefsKey => SortUtility.SortPrefs.MakeKey(this.parent.iname, this.cnds_iname);
  }
}
