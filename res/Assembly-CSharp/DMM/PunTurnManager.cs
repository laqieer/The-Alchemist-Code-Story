// Decompiled with JetBrains decompiler
// Type: PunTurnManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using Photon;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PunTurnManager : PunBehaviour
{
  public float TurnDuration = 20f;
  public IPunTurnManagerCallbacks TurnManagerListener;
  private readonly HashSet<PhotonPlayer> finishedPlayers = new HashSet<PhotonPlayer>();
  public const byte TurnManagerEventOffset = 0;
  public const byte EvMove = 1;
  public const byte EvFinalMove = 2;
  private bool _isOverCallProcessed;

  public int Turn
  {
    get => PhotonNetwork.room.GetTurn();
    private set
    {
      this._isOverCallProcessed = false;
      PhotonNetwork.room.SetTurn(value, true);
    }
  }

  public float ElapsedTimeInTurn
  {
    get => (float) (PhotonNetwork.ServerTimestamp - PhotonNetwork.room.GetTurnStart()) / 1000f;
  }

  public float RemainingSecondsInTurn
  {
    get => Mathf.Max(0.0f, this.TurnDuration - this.ElapsedTimeInTurn);
  }

  public bool IsCompletedByAll
  {
    get
    {
      return PhotonNetwork.room != null && this.Turn > 0 && this.finishedPlayers.Count == PhotonNetwork.room.PlayerCount;
    }
  }

  public bool IsFinishedByMe => this.finishedPlayers.Contains(PhotonNetwork.player);

  public bool IsOver => (double) this.RemainingSecondsInTurn <= 0.0;

  private void Start()
  {
    PhotonNetwork.OnEventCall += new PhotonNetwork.EventCallback(this.OnEvent);
  }

  private void Update()
  {
    if (this.Turn <= 0 || !this.IsOver || this._isOverCallProcessed)
      return;
    this._isOverCallProcessed = true;
    this.TurnManagerListener.OnTurnTimeEnds(this.Turn);
  }

  public void BeginTurn() => ++this.Turn;

  public void SendMove(object move, bool finished)
  {
    if (this.IsFinishedByMe)
    {
      Debug.LogWarning((object) "Can't SendMove. Turn is finished by this player.");
    }
    else
    {
      Hashtable hashtable = new Hashtable();
      ((Dictionary<object, object>) hashtable).Add((object) "turn", (object) this.Turn);
      ((Dictionary<object, object>) hashtable).Add((object) nameof (move), move);
      byte eventCode = !finished ? (byte) 1 : (byte) 2;
      PhotonNetwork.RaiseEvent(eventCode, (object) hashtable, true, new RaiseEventOptions()
      {
        CachingOption = EventCaching.AddToRoomCache
      });
      if (finished)
        PhotonNetwork.player.SetFinishedTurn(this.Turn);
      this.OnEvent(eventCode, (object) hashtable, PhotonNetwork.player.ID);
    }
  }

  public bool GetPlayerFinishedTurn(PhotonPlayer player)
  {
    return player != null && this.finishedPlayers != null && this.finishedPlayers.Contains(player);
  }

  public void OnEvent(byte eventCode, object content, int senderId)
  {
    PhotonPlayer player = PhotonPlayer.Find(senderId);
    switch (eventCode)
    {
      case 1:
        Hashtable hashtable1 = content as Hashtable;
        int turn1 = (int) hashtable1[(object) "turn"];
        object move1 = hashtable1[(object) "move"];
        this.TurnManagerListener.OnPlayerMove(player, turn1, move1);
        break;
      case 2:
        Hashtable hashtable2 = content as Hashtable;
        int turn2 = (int) hashtable2[(object) "turn"];
        object move2 = hashtable2[(object) "move"];
        if (turn2 == this.Turn)
        {
          this.finishedPlayers.Add(player);
          this.TurnManagerListener.OnPlayerFinished(player, turn2, move2);
        }
        if (!this.IsCompletedByAll)
          break;
        this.TurnManagerListener.OnTurnCompleted(this.Turn);
        break;
    }
  }

  public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
  {
    if (!((Dictionary<object, object>) propertiesThatChanged).ContainsKey((object) "Turn"))
      return;
    this._isOverCallProcessed = false;
    this.finishedPlayers.Clear();
    this.TurnManagerListener.OnTurnBegins(this.Turn);
  }
}
