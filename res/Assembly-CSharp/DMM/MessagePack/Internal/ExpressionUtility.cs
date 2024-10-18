// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ExpressionUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace MessagePack.Internal
{
  public static class ExpressionUtility
  {
    private static MethodInfo GetMethodInfoCore(LambdaExpression expression)
    {
      if (expression == null)
        throw new ArgumentNullException(nameof (expression));
      return (expression.Body as MethodCallExpression).Method;
    }

    public static MethodInfo GetMethodInfo<T>(Expression<Func<T>> expression)
    {
      return ExpressionUtility.GetMethodInfoCore((LambdaExpression) expression);
    }

    public static MethodInfo GetMethodInfo(Expression<Action> expression)
    {
      return ExpressionUtility.GetMethodInfoCore((LambdaExpression) expression);
    }

    public static MethodInfo GetMethodInfo<T, TR>(Expression<Func<T, TR>> expression)
    {
      return ExpressionUtility.GetMethodInfoCore((LambdaExpression) expression);
    }

    public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
    {
      return ExpressionUtility.GetMethodInfoCore((LambdaExpression) expression);
    }

    public static MethodInfo GetMethodInfo<T, TArg1, TR>(Expression<Func<T, TArg1, TR>> expression)
    {
      return ExpressionUtility.GetMethodInfoCore((LambdaExpression) expression);
    }

    private static MemberInfo GetMemberInfoCore<T>(Expression<T> source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return (source.Body as MemberExpression).Member;
    }

    public static PropertyInfo GetPropertyInfo<T, TR>(Expression<Func<T, TR>> expression)
    {
      return ExpressionUtility.GetMemberInfoCore<Func<T, TR>>(expression) as PropertyInfo;
    }

    public static FieldInfo GetFieldInfo<T, TR>(Expression<Func<T, TR>> expression)
    {
      return ExpressionUtility.GetMemberInfoCore<Func<T, TR>>(expression) as FieldInfo;
    }
  }
}
