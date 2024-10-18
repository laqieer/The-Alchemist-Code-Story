// Decompiled with JetBrains decompiler
// Type: SRPG.GachaReceiptData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
