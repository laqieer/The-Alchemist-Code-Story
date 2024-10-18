// Decompiled with JetBrains decompiler
// Type: Fabric.Answers.Internal.AnswersSharedInstanceJavaObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Fabric.Answers.Internal
{
  internal class AnswersSharedInstanceJavaObject
  {
    private AndroidJavaObject javaObject;

    public AnswersSharedInstanceJavaObject()
    {
      this.javaObject = new AndroidJavaClass("com.crashlytics.android.answers.Answers").CallStatic<AndroidJavaObject>("getInstance");
    }

    public void Log(string methodName, AnswersEventInstanceJavaObject eventInstance)
    {
      this.javaObject.Call(methodName, new object[1]
      {
        (object) eventInstance.javaObject
      });
    }
  }
}
