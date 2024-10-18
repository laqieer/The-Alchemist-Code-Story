// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.SelectUIStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.SavedGame
{
  public enum SelectUIStatus
  {
    BadInputError = -4,
    AuthenticationError = -3,
    TimeoutError = -2,
    InternalError = -1,
    SavedGameSelected = 1,
    UserClosedUI = 2,
  }
}
