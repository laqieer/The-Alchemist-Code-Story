// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraRecipeMaterialParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class TobiraRecipeMaterialParam
  {
    private string mIname;
    private int mNum;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public int Num
    {
      get
      {
        return this.mNum;
      }
    }

    public void Deserialize(JSON_TobiraRecipeMaterialParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mNum = json.num;
    }
  }
}
