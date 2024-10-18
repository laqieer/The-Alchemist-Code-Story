// Decompiled with JetBrains decompiler
// Type: ScoreExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using ExitGames.Client.Photon;

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
    if (player.CustomProperties.TryGetValue((object) "score", out obj))
      return (int) obj;
    return 0;
  }
}
