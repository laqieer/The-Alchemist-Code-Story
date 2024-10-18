// Decompiled with JetBrains decompiler
// Type: IPunTurnManagerCallbacks
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

public interface IPunTurnManagerCallbacks
{
  void OnTurnBegins(int turn);

  void OnTurnCompleted(int turn);

  void OnPlayerMove(PhotonPlayer player, int turn, object move);

  void OnPlayerFinished(PhotonPlayer player, int turn, object move);

  void OnTurnTimeEnds(int turn);
}
