// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.SelectUIStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
