// Decompiled with JetBrains decompiler
// Type: SRPG.ChatBlackListRes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ChatBlackListRes
  {
    public byte is_success;

    public bool IsSuccess
    {
      get
      {
        return this.is_success == (byte) 1;
      }
    }

    public void Deserialize(JSON_ChatBlackListRes json)
    {
      if (json == null)
        return;
      this.is_success = json.is_success;
    }
  }
}
