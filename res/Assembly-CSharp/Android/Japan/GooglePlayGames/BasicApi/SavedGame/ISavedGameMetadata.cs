// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.ISavedGameMetadata
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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
