// Decompiled with JetBrains decompiler
// Type: Fabric.Answers.Internal.AnswersStubImplementation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric.Answers.Internal
{
  internal class AnswersStubImplementation : IAnswers
  {
    public AnswersStubImplementation()
    {
      Debug.Log((object) "Answers will no-op because it was initialized for a non-Android, non-Apple platform.");
    }

    public void LogSignUp(string method, bool? success, Dictionary<string, object> customAttributes)
    {
    }

    public void LogLogin(string method, bool? success, Dictionary<string, object> customAttributes)
    {
    }

    public void LogShare(string method, string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes)
    {
    }

    public void LogInvite(string method, Dictionary<string, object> customAttributes)
    {
    }

    public void LogLevelStart(string level, Dictionary<string, object> customAttributes)
    {
    }

    public void LogLevelEnd(string level, double? score, bool? success, Dictionary<string, object> customAttributes)
    {
    }

    public void LogAddToCart(Decimal? itemPrice, string currency, string itemName, string itemType, string itemId, Dictionary<string, object> customAttributes)
    {
    }

    public void LogPurchase(Decimal? price, string currency, bool? success, string itemName, string itemType, string itemId, Dictionary<string, object> customAttributes)
    {
    }

    public void LogStartCheckout(Decimal? totalPrice, string currency, int? itemCount, Dictionary<string, object> customAttributes)
    {
    }

    public void LogRating(int? rating, string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes)
    {
    }

    public void LogContentView(string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes)
    {
    }

    public void LogSearch(string query, Dictionary<string, object> customAttributes)
    {
    }

    public void LogCustom(string eventName, Dictionary<string, object> customAttributes)
    {
    }
  }
}
