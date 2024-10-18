// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsDemoResourcePath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class StringIsDemoResourcePath : PropertyAttribute
  {
    public System.Type ResourceType;
    public string ParentDirectory;

    public StringIsDemoResourcePath(System.Type type)
    {
      this.ResourceType = type;
      this.ParentDirectory = (string) null;
    }

    public StringIsDemoResourcePath(System.Type type, string dir)
    {
      this.ResourceType = type;
      this.ParentDirectory = dir;
    }
  }
}
