// Decompiled with JetBrains decompiler
// Type: ScoreExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;

#nullable disable
public static class ScoreExtensions
{
  public static void SetScore(this PhotonPlayer player, int newScore)
  {
    player.SetCustomProperties(new Hashtable()
    {
      [(object) "score"] = (object) newScore
    });
  }

  public static void AddScore(this PhotonPlayer player, int scoreToAddToCurrent)
  {
    int num = player.GetScore() + scoreToAddToCurrent;
    player.SetCustomProperties(new Hashtable()
    {
      [(object) "score"] = (object) num
    });
  }

  public static int GetScore(this PhotonPlayer player)
  {
    object obj;
    return ((Dictionary<object, object>) player.CustomProperties).TryGetValue((object) "score", out obj) ? (int) obj : 0;
  }
}
