﻿// Decompiled with JetBrains decompiler
// Type: EventCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

public class EventCode
{
  public const byte GameList = 230;
  public const byte GameListUpdate = 229;
  public const byte QueueState = 228;
  public const byte Match = 227;
  public const byte AppStats = 226;
  public const byte LobbyStats = 224;
  [Obsolete("TCP routing was removed after becoming obsolete.")]
  public const byte AzureNodeInfo = 210;
  public const byte Join = 255;
  public const byte Leave = 254;
  public const byte PropertiesChanged = 253;
  [Obsolete("Use PropertiesChanged now.")]
  public const byte SetProperties = 253;
  public const byte ErrorInfo = 251;
  public const byte CacheSliceChanged = 250;
  public const byte AuthEvent = 223;
}
