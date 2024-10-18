// Decompiled with JetBrains decompiler
// Type: ScoreExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;

public static class ScoreExtensions
{
  public static void SetScore(this PhotonPlayer player, int newScore)
  {
    player.SetCustomProperties(new Hashtable()
    {
      [(object) "score"] = (object) newScore
    }, (Hashtable) null, false);
  }

  public static void AddScore(this PhotonPlayer player, int scoreToAddToCurrent)
  {
    int num = player.GetScore() + scoreToAddToCurrent;
    player.SetCustomProperties(new Hashtable()
    {
      [(object) "score"] = (object) num
    }, (Hashtable) null, false);
  }

  public static int GetScore(this PhotonPlayer player)
  {
    object obj;
    if (((Dictionary<object, object>) player.CustomProperties).TryGetValue((object) "score", out obj))
      return (int) obj;
    return 0;
  }
}
