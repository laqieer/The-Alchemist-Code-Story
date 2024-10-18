// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.ISavedGameMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace GooglePlayGames.BasicApi.SavedGame
{
  public interface ISavedGameMetadata
  {
    bool IsOpen { get; }

    string Filename { get; }

    string Description { get; }

    string CoverImageURL { get; }

    TimeSpan TotalTimePlayed { get; }

    DateTime LastModifiedTimestamp { get; }
  }
}
