// Decompiled with JetBrains decompiler
// Type: SRPG.TwitterMessageDetailParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class TwitterMessageDetailParam
  {
    private string text;
    private string[] hash_tag;
    private string cnds_key;

    public string Text => this.text;

    public string[] HashTag => this.hash_tag;

    public string CndsKey => this.cnds_key;

    public void Deserialize(JSON_TwitterMessageDetailParam json)
    {
      this.text = json.text;
      this.hash_tag = json.hash_tag;
      this.cnds_key = json.cnds_key;
    }
  }
}
