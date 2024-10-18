// Decompiled with JetBrains decompiler
// Type: LogKit.Log
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace LogKit
{
  public struct Log
  {
    public Guid ID;
    public string Tag;
    public DateTime Date;
    public LogLevel LogLevel;
    public UserInfo UserInfo;
  }
}
