// Decompiled with JetBrains decompiler
// Type: SRPG.GachaReceiptData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GachaReceiptData
  {
    public string iname;
    public string type;
    public int val;

    public void Init()
    {
      this.iname = (string) null;
      this.type = (string) null;
      this.val = 0;
    }

    public bool Deserialize(Json_GachaReceipt json)
    {
      this.Init();
      if (json == null)
        return false;
      this.iname = json.iname;
      this.type = json.type;
      this.val = json.val;
      return true;
    }
  }
}
