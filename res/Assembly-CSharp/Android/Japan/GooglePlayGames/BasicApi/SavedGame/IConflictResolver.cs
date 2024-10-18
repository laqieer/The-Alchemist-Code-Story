// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.SavedGame.IConflictResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.SavedGame
{
  public interface IConflictResolver
  {
    void ChooseMetadata(ISavedGameMetadata chosenMetadata);

    void ResolveConflict(ISavedGameMetadata chosenMetadata, SavedGameMetadataUpdate metadataUpdate, byte[] updatedData);
  }
}
