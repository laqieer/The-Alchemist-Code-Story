// Decompiled with JetBrains decompiler
// Type: AdjustTownIconPos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AdjustTownIconPos : MonoBehaviour
{
  [SerializeField]
  private float SetOffsetPos = 1f;

  private void Start()
  {
  }

  public void AdjustIconPos()
  {
    Vector3 localPosition = this.transform.localPosition;
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    foreach (GameObject gameObject in GameObjectID.FindGameObjects("BGSCROLL"))
    {
      AdjustTownBGScale component = gameObject.GetComponent<AdjustTownBGScale>();
      if ((Object) component != (Object) null && (double) safeArea.width < (double) Screen.width)
      {
        float num = this.SetOffsetPos * ((float) (1.0 + (1.0 - (double) safeArea.width / (double) Screen.width)) + component.RevisionScale);
        localPosition.x -= num;
        this.transform.localPosition = localPosition;
        break;
      }
    }
  }
}
