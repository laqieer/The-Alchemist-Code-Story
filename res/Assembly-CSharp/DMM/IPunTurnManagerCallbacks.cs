// Decompiled with JetBrains decompiler
// Type: IPunTurnManagerCallbacks
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public interface IPunTurnManagerCallbacks
{
  void OnTurnBegins(int turn);

  void OnTurnCompleted(int turn);

  void OnPlayerMove(PhotonPlayer player, int turn, object move);

  void OnPlayerFinished(PhotonPlayer player, int turn, object move);

  void OnTurnTimeEnds(int turn);
}
