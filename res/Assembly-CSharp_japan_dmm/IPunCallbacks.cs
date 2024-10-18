// Decompiled with JetBrains decompiler
// Type: IPunCallbacks
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;

#nullable disable
public interface IPunCallbacks
{
  void OnConnectedToPhoton();

  void OnLeftRoom();

  void OnMasterClientSwitched(PhotonPlayer newMasterClient);

  void OnPhotonCreateRoomFailed(object[] codeAndMsg);

  void OnPhotonJoinRoomFailed(object[] codeAndMsg);

  void OnCreatedRoom();

  void OnJoinedLobby();

  void OnLeftLobby();

  void OnFailedToConnectToPhoton(DisconnectCause cause);

  void OnConnectionFail(DisconnectCause cause);

  void OnDisconnectedFromPhoton();

  void OnPhotonInstantiate(PhotonMessageInfo info);

  void OnReceivedRoomListUpdate();

  void OnJoinedRoom();

  void OnPhotonPlayerConnected(PhotonPlayer newPlayer);

  void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer);

  void OnPhotonRandomJoinFailed(object[] codeAndMsg);

  void OnConnectedToMaster();

  void OnPhotonMaxCccuReached();

  void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged);

  void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps);

  void OnUpdatedFriendList();

  void OnCustomAuthenticationFailed(string debugMessage);

  void OnCustomAuthenticationResponse(Dictionary<string, object> data);

  void OnWebRpcResponse(OperationResponse response);

  void OnOwnershipRequest(object[] viewAndPlayer);

  void OnLobbyStatisticsUpdate();

  void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer);

  void OnOwnershipTransfered(object[] viewAndPlayers);
}
