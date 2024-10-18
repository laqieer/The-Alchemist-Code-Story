// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.TokenClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames
{
  internal interface TokenClient
  {
    string GetEmail();

    string GetAccessToken();

    string GetAuthorizationCode(string serverClientId);

    string GetIdToken(string serverClientId);
  }
}
