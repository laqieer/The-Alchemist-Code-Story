﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FreeGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class FreeGacha
  {
    public int num;
    public long at;

    public bool Deserialize(Json_FreeGacha json)
    {
      this.num = json.num;
      this.at = json.at;
      return true;
    }
  }
}