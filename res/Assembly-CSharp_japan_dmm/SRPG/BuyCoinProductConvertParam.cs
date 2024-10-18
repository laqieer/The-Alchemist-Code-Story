// Decompiled with JetBrains decompiler
// Type: SRPG.BuyCoinProductConvertParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class BuyCoinProductConvertParam
  {
    private string mName;
    private string mConvert;

    public string Name => this.mName;

    public string Convert => this.mConvert;

    public bool Deserialize(JSON_BuyCoinProductConvertParam json)
    {
      if (json == null)
        return false;
      this.mName = json.name;
      this.mConvert = json.convert;
      return true;
    }
  }
}
