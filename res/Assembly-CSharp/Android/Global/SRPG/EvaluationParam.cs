﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EvaluationParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class EvaluationParam
  {
    public StatusParam status = new StatusParam();
    public string iname;
    public OInt value;

    public bool Deserialize(JSON_EvaluationParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.value = (OInt) json.val;
      this.status.Clear();
      this.status.hp = (OInt) json.hp;
      this.status.mp = (OShort) json.mp;
      this.status.atk = (OShort) json.atk;
      this.status.def = (OShort) json.def;
      this.status.mag = (OShort) json.mag;
      this.status.mnd = (OShort) json.mnd;
      this.status.dex = (OShort) json.dex;
      this.status.spd = (OShort) json.spd;
      this.status.cri = (OShort) json.cri;
      this.status.luk = (OShort) json.luk;
      return true;
    }
  }
}
