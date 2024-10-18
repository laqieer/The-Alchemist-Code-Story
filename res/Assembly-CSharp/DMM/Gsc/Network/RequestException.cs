// Decompiled with JetBrains decompiler
// Type: Gsc.Network.RequestException
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Runtime.Serialization;

#nullable disable
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
