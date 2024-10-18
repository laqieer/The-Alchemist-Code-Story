// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.Multiplayer
{
  public interface RealTimeMultiplayerListener
  {
    void OnRoomSetupProgress(float percent);

    void OnRoomConnected(bool success);

    void OnLeftRoom();

    void OnParticipantLeft(Participant participant);

    void OnPeersConnected(string[] participantIds);

    void OnPeersDisconnected(string[] participantIds);

    void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data);
  }
}
