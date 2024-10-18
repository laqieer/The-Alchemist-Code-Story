// Decompiled with JetBrains decompiler
// Type: EventCoinListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class EventCoinListItem : MonoBehaviour
{
  [SerializeField]
  private GameObject button;

  public ListItemEvents listItemEvents => ((Component) this).GetComponent<ListItemEvents>();

  public GameObject Button => this.button;

  public void Set(bool isPeriod, bool isRead, long post_at, long read)
  {
    if (isRead)
      this.button.gameObject.SetActive(false);
    else
      this.button.gameObject.SetActive(true);
  }
}
