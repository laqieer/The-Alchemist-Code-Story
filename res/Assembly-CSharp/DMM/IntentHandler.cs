// Decompiled with JetBrains decompiler
// Type: IntentHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public class IntentHandler
{
  private static IntentHandler instance = new IntentHandler();

  public static IntentHandler GetInstance() => IntentHandler.instance;

  public void ClearIntentHandlers()
  {
  }

  public void AddNoopIntentHandler()
  {
  }

  public void AddUrlIntentHandler()
  {
  }

  public void AddCustomIntentHandler(string gameObjectName, string methodName)
  {
  }
}
