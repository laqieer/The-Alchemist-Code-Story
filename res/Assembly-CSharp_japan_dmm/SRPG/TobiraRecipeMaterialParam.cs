// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraRecipeMaterialParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class TobiraRecipeMaterialParam
  {
    private string mIname;
    private int mNum;

    public string Iname => this.mIname;

    public int Num => this.mNum;

    public void Deserialize(JSON_TobiraRecipeMaterialParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mNum = json.num;
    }
  }
}
