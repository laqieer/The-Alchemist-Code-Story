﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TargetID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public struct TargetID
  {
    public TargetID.IDType Type;
    public string ID;

    public enum IDType
    {
      ObjectID,
      UnitID,
      ActorID,
    }
  }
}
