// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTabList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class GachaTabList : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject TabTemplate;
    [SerializeField]
    private GameObject RareTab;
    [SerializeField]
    private GameObject NormalTab;
    [SerializeField]
    private GameObject ArtifactTab;
    [SerializeField]
    private GameObject TicketTab;

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.TabTemplate != (UnityEngine.Object) null)
        this.TabTemplate.SetActive(false);
      if ((UnityEngine.Object) this.RareTab != (UnityEngine.Object) null)
        this.RareTab.SetActive(false);
      if ((UnityEngine.Object) this.NormalTab != (UnityEngine.Object) null)
        this.NormalTab.SetActive(false);
      if ((UnityEngine.Object) this.ArtifactTab != (UnityEngine.Object) null)
        this.ArtifactTab.SetActive(false);
      if (!((UnityEngine.Object) this.TicketTab != (UnityEngine.Object) null))
        return;
      this.TicketTab.SetActive(false);
    }

    private void Update()
    {
    }

    private void Refresh()
    {
    }
  }
}
