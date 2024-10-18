// Decompiled with JetBrains decompiler
// Type: SRPG.GachaAnimationParts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GachaAnimationParts : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] LowObjects;
    [SerializeField]
    private GameObject[] MiddleObjects;
    [SerializeField]
    private GameObject[] HighObjects;
    [SerializeField]
    private GachaAnimationParts.StateType SType;

    public GachaAnimationParts.StateType state => this.SType;

    public void Setup(int excite)
    {
      this.Reset();
      GameObject[] gameObjectArray;
      switch (excite)
      {
        case 1:
          gameObjectArray = this.LowObjects;
          break;
        case 2:
          gameObjectArray = this.MiddleObjects;
          break;
        case 3:
          gameObjectArray = this.HighObjects;
          break;
        default:
          gameObjectArray = (GameObject[]) null;
          break;
      }
      if (gameObjectArray == null)
        return;
      for (int index = 0; index < gameObjectArray.Length; ++index)
      {
        if (Object.op_Inequality((Object) gameObjectArray[index], (Object) null))
          gameObjectArray[index].SetActive(true);
      }
    }

    private void Reset()
    {
      if (this.LowObjects != null)
      {
        for (int index = 0; index < this.LowObjects.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.LowObjects[index], (Object) null))
            this.LowObjects[index].SetActive(false);
        }
      }
      if (this.MiddleObjects != null)
      {
        for (int index = 0; index < this.MiddleObjects.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.MiddleObjects[index], (Object) null))
            this.MiddleObjects[index].SetActive(false);
        }
      }
      if (this.HighObjects == null)
        return;
      for (int index = 0; index < this.HighObjects.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.HighObjects[index], (Object) null))
          this.HighObjects[index].SetActive(false);
      }
    }

    public enum StateType : byte
    {
      None,
      Before,
      After,
    }
  }
}
