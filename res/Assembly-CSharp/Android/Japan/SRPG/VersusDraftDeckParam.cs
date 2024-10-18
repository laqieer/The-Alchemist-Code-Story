// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftDeckParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusDraftDeckParam
  {
    private int mId;
    private string mName;
    private int mSecretMax;

    public int Id
    {
      get
      {
        return this.mId;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public int SecretMax
    {
      get
      {
        return this.mSecretMax;
      }
    }

    public bool Deserialize(JSON_VersusDraftDeckParam param)
    {
      this.mId = param.id;
      this.mName = param.name;
      this.mSecretMax = param.secret_max;
      return true;
    }
  }
}
