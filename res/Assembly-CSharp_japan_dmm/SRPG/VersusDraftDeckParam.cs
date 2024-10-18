// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftDeckParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class VersusDraftDeckParam
  {
    private int mId;
    private string mName;
    private int mSecretMax;

    public int Id => this.mId;

    public string Name => this.mName;

    public int SecretMax => this.mSecretMax;

    public bool Deserialize(JSON_VersusDraftDeckParam param)
    {
      this.mId = param.id;
      this.mName = param.name;
      this.mSecretMax = param.secret_max;
      return true;
    }
  }
}
