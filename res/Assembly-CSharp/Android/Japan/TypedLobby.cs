﻿// Decompiled with JetBrains decompiler
// Type: TypedLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

public class TypedLobby
{
  public static readonly TypedLobby Default = new TypedLobby();
  public string Name;
  public LobbyType Type;

  public TypedLobby()
  {
    this.Name = string.Empty;
    this.Type = LobbyType.Default;
  }

  public TypedLobby(string name, LobbyType type)
  {
    this.Name = name;
    this.Type = type;
  }

  public bool IsDefault
  {
    get
    {
      if (this.Type == LobbyType.Default)
        return string.IsNullOrEmpty(this.Name);
      return false;
    }
  }

  public override string ToString()
  {
    return string.Format("lobby '{0}'[{1}]", (object) this.Name, (object) this.Type);
  }
}
