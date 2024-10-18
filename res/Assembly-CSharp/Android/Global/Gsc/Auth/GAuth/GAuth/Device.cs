﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.Device
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using DeviceKit;
using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Gsc.Auth.GAuth.GAuth
{
  public class Device : IDevice
  {
    public readonly string IDFA;
    public readonly string ID;

    public Device()
    {
      Device.Instance = this;
      this.IDFA = App.GetIdfa();
      this.ID = App.GetClientId();
    }

    public static Device Instance { get; private set; }

    public string Platform
    {
      get
      {
        return "googleplay";
      }
    }

    public static string NameUUIDFromBytes(byte[] input)
    {
      byte[] hash = MD5.Create().ComputeHash(input);
      hash[6] &= (byte) 15;
      hash[6] |= (byte) 48;
      hash[8] &= (byte) 63;
      hash[8] |= (byte) 128;
      return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower().Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-");
    }

    public static string GetAndroidID()
    {
      return new AndroidJavaClass("android.provider.Settings$Secure").CallStatic<string>("getString", (object) new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getContentResolver"), (object) "android_id");
    }

    public bool initialized
    {
      get
      {
        return true;
      }
    }

    public bool hasError
    {
      get
      {
        return false;
      }
    }
  }
}
