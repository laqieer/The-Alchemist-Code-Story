// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_StatusCoefficientParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_StatusCoefficientParam
  {
    public float hp;
    public float atk;
    public float def;
    public float matk;
    public float mdef;
    public float dex;
    public float spd;
    public float cri;
    public float luck;
    public float cmb;
    public float move;
    public float jmp;
  }
}
