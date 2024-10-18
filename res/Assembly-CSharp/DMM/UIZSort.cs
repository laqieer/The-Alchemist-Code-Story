// Decompiled with JetBrains decompiler
// Type: UIZSort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("UI/Z Sort")]
[DisallowMultipleComponent]
public class UIZSort : MonoBehaviour
{
  private Transform mTransform;

  private void Start()
  {
    this.mTransform = ((Component) this).transform;
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
        int index4 = childCount - 1;
        int index5 = childCount - 1;
        for (; index4 >= index1; --index4)
        {
          if (index4 != index2)
          {
            transformArray[index5] = transformArray[index4];
            --index5;
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
