// Decompiled with JetBrains decompiler
// Type: SwitchDemoFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SwitchDemoFrame : MonoBehaviour
{
  [SerializeField]
  private GameObject Frame;

  private void Start()
  {
    if (!((Object) this.Frame != (Object) null) || (double) SetCanvasBounds.GetSafeArea().width >= (double) Screen.width)
      return;
    this.Frame.SetActive(true);
  }
}
