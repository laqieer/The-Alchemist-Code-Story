// Decompiled with JetBrains decompiler
// Type: MainCameraProxy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
public class MainCameraProxy : MonoBehaviour
{
  private void Update()
  {
    Camera main = Camera.main;
    Camera component = ((Component) this).GetComponent<Camera>();
    if (Object.op_Equality((Object) main, (Object) null) || Object.op_Equality((Object) component, (Object) null))
      return;
    Transform transform1 = ((Component) main).transform;
    Transform transform2 = ((Component) this).transform;
    transform1.position = transform2.position;
    transform1.rotation = transform2.rotation;
    main.fieldOfView = component.fieldOfView;
    main.farClipPlane = component.farClipPlane;
    main.nearClipPlane = component.nearClipPlane;
    main.orthographic = component.orthographic;
    main.orthographicSize = component.orthographicSize;
  }
}
