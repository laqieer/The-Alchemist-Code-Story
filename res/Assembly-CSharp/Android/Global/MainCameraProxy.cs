// Decompiled with JetBrains decompiler
// Type: MainCameraProxy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Camera))]
public class MainCameraProxy : MonoBehaviour
{
  private void Update()
  {
    Camera main = Camera.main;
    Camera component = this.GetComponent<Camera>();
    if ((Object) main == (Object) null || (Object) component == (Object) null)
      return;
    Transform transform1 = main.transform;
    Transform transform2 = this.transform;
    transform1.position = transform2.position;
    transform1.rotation = transform2.rotation;
    main.fieldOfView = component.fieldOfView;
    main.farClipPlane = component.farClipPlane;
    main.nearClipPlane = component.nearClipPlane;
    main.orthographic = component.orthographic;
    main.orthographicSize = component.orthographicSize;
  }
}
