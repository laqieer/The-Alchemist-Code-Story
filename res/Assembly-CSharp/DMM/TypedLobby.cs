// Decompiled with JetBrains decompiler
// Type: TypedLobby
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public class TypedLobby
{
  public string Name;
  public LobbyType Type;
  public static readonly TypedLobby Default = new TypedLobby();

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

  public bool IsDefault => this.Type == LobbyType.Default && string.IsNullOrEmpty(this.Name);

  public override string ToString()
  {
    return string.Format("lobby '{0}'[{1}]", (object) this.Name, (object) this.Type);
  }
}
