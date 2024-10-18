// Decompiled with JetBrains decompiler
// Type: EventCoinListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EventCoinListItem : MonoBehaviour
{
  [SerializeField]
  private GameObject button;

  public ListItemEvents listItemEvents
  {
    get
    {
      return this.GetComponent<ListItemEvents>();
    }
  }

  public GameObject Button
  {
    get
    {
      return this.button;
    }
  }

  public void Set(bool isPeriod, bool isRead, long post_at, long read)
  {
    if (isRead)
      this.button.gameObject.SetActive(false);
    else
      this.button.gameObject.SetActive(true);
  }
}
