// Decompiled with JetBrains decompiler
// Type: Gsc.Network.RequestException
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.Serialization;

namespace Gsc.Network
{
  [Serializable]
  public class RequestException : Exception
  {
    public RequestException()
    {
    }

    public RequestException(string message)
      : base(message)
    {
    }

    public RequestException(string message, Exception inner)
      : base(message, inner)
    {
    }

    protected RequestException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
