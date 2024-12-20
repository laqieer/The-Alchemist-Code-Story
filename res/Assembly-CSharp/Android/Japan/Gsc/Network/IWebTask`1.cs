﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IWebTask`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Tasks;
using System.Collections;

namespace Gsc.Network
{
  public interface IWebTask<TResponse> : IWebTask, IWebTaskBase, ITask, IEnumerator where TResponse : IResponse<TResponse>
  {
    TResponse Response { get; }

    IErrorResponse ErrorResponse { get; }
  }
}
