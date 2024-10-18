// Decompiled with JetBrains decompiler
// Type: IPunTurnManagerCallbacks
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

public interface IPunTurnManagerCallbacks
{
  void OnTurnBegins(int turn);

  void OnTurnCompleted(int turn);

  void OnPlayerMove(PhotonPlayer player, int turn, object move);

  void OnPlayerFinished(PhotonPlayer player, int turn, object move);

  void OnTurnTimeEnds(int turn);
}
