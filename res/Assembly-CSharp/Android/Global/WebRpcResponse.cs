﻿// Decompiled with JetBrains decompiler
// Type: WebRpcResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;

public class WebRpcResponse
{
  public WebRpcResponse(OperationResponse response)
  {
    object obj;
    ((Dictionary<byte, object>) response.Parameters).TryGetValue((byte) 209, out obj);
    this.Name = obj as string;
    ((Dictionary<byte, object>) response.Parameters).TryGetValue((byte) 207, out obj);
    this.ReturnCode = obj == null ? -1 : (int) (byte) obj;
    ((Dictionary<byte, object>) response.Parameters).TryGetValue((byte) 208, out obj);
    this.Parameters = obj as Dictionary<string, object>;
    ((Dictionary<byte, object>) response.Parameters).TryGetValue((byte) 206, out obj);
    this.DebugMessage = obj as string;
  }

  public string Name { get; private set; }

  public int ReturnCode { get; private set; }

  public string DebugMessage { get; private set; }

  public Dictionary<string, object> Parameters { get; private set; }

  public string ToStringFull()
  {
    return string.Format("{0}={2}: {1} \"{3}\"", new object[4]
    {
      (object) this.Name,
      (object) SupportClass.DictionaryToString((IDictionary) this.Parameters),
      (object) this.ReturnCode,
      (object) this.DebugMessage
    });
  }
}
