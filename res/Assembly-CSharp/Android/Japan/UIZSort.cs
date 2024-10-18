// Decompiled with JetBrains decompiler
// Type: UIZSort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("UI/Z Sort")]
[DisallowMultipleComponent]
public class UIZSort : MonoBehaviour
{
  private Transform mTransform;

  private void Start()
  {
    this.mTransform = this.transform;
    this.Update();
  }

  private void Update()
  {
    int childCount = this.mTransform.childCount;
    if (childCount <= 0)
      return;
    Transform[] transformArray = new Transform[childCount];
    bool flag = false;
    for (int index = 0; index < childCount; ++index)
      transformArray[index] = this.mTransform.GetChild(index);
    for (int index1 = 0; index1 < childCount; ++index1)
    {
      float z = transformArray[index1].position.z;
      int index2 = index1;
      for (int index3 = index1 + 1; index3 < childCount; ++index3)
      {
        if ((double) transformArray[index3].position.z > (double) z)
        {
          index2 = index3;
          z = transformArray[index3].position.z;
        }
      }
      if (index2 != index1)
      {
        Transform transform = transformArray[index2];
        int index3 = childCount - 1;
        int index4 = childCount - 1;
        for (; index3 >= index1; --index3)
        {
          if (index3 != index2)
          {
            transformArray[index4] = transformArray[index3];
            --index4;
          }
        }
        flag = true;
        transformArray[index1] = transform;
      }
    }
    if (!flag)
      return;
    for (int index = 0; index < childCount; ++index)
      transformArray[index].SetSiblingIndex(index);
  }
}
