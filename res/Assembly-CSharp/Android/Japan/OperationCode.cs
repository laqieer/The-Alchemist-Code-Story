﻿// Decompiled with JetBrains decompiler
// Type: OperationCode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

public class OperationCode
{
  [Obsolete("Exchanging encrpytion keys is done internally in the lib now. Don't expect this operation-result.")]
  public const byte ExchangeKeysForEncryption = 250;
  public const byte Join = 255;
  public const byte AuthenticateOnce = 231;
  public const byte Authenticate = 230;
  public const byte JoinLobby = 229;
  public const byte LeaveLobby = 228;
  public const byte CreateGame = 227;
  public const byte JoinGame = 226;
  public const byte JoinRandomGame = 225;
  public const byte Leave = 254;
  public const byte RaiseEvent = 253;
  public const byte SetProperties = 252;
  public const byte GetProperties = 251;
  public const byte ChangeGroups = 248;
  public const byte FindFriends = 222;
  public const byte GetLobbyStats = 221;
  public const byte GetRegions = 220;
  public const byte WebRpc = 219;
  public const byte ServerSettings = 218;
  public const byte GetGameList = 217;
}
