// Decompiled with JetBrains decompiler
// Type: AdjustTownIconPos
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AdjustTownIconPos : MonoBehaviour
{
  [SerializeField]
  private float SetOffsetPos = 1f;

  private void Start()
  {
  }

  public void AdjustIconPos()
  {
    Vector3 localPosition = ((Component) this).transform.localPosition;
    Rect safeArea = SetCanvasBounds.GetSafeArea();
    foreach (GameObject gameObject in GameObjectID.FindGameObjects("BGSCROLL"))
    {
      AdjustTownBGScale component = gameObject.GetComponent<AdjustTownBGScale>();
      if (Object.op_Inequality((Object) component, (Object) null) && (double) ((Rect) ref safeArea).width < (double) Screen.width)
      {
        float num = this.SetOffsetPos * ((float) (1.0 + (1.0 - (double) ((Rect) ref safeArea).width / (double) Screen.width)) + component.RevisionScale);
        localPosition.x -= num;
        ((Component) this).transform.localPosition = localPosition;
        break;
      }
    }
  }
}
