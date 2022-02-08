using UnityEngine;
using System.Runtime.CompilerServices;

namespace Common
{
/// <summary>
/// Debugging wrapper for simplify Debug in Unity.
/// </summary>
public class Dbg
{
#region Log
  public static void Log(object message, Object context, [CallerMemberName] string memberName = "",
                         [CallerFilePath] string sourceFilePath = "",
                         [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.Log($"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}",
              context);
  }
  public static void Log(object message, [CallerMemberName] string memberName = "",
                         [CallerFilePath] string sourceFilePath = "",
                         [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.Log($"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
#endregion

#region LogWarning
  public static void LogWarning(object message, Object context,
                                [CallerMemberName] string memberName = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.LogWarning(
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}",
      context);
  }
  public static void LogWarning(object message, [CallerMemberName] string memberName = "",
                                [CallerFilePath] string sourceFilePath = "",
                                [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.LogWarning(
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
#endregion

#region LogError
  public static void LogError(object message, Object context,
                              [CallerMemberName] string memberName = "",
                              [CallerFilePath] string sourceFilePath = "",
                              [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.LogError(
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}",
      context);
  }
  public static void LogError(object message, [CallerMemberName] string memberName = "",
                              [CallerFilePath] string sourceFilePath = "",
                              [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.LogError(
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
#endregion

#region LogAssertion
  public static void LogAssertion(object message, Object context,
                                  [CallerMemberName] string memberName = "",
                                  [CallerFilePath] string sourceFilePath = "",
                                  [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.LogAssertion(
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}",
      context);
  }
  public static void LogAssertion(object message, [CallerMemberName] string memberName = "",
                                  [CallerFilePath] string sourceFilePath = "",
                                  [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.LogAssertion(
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
#endregion

#region LogException
  public static void LogException(System.Exception exception, Object context)
  {
    Debug.LogException(exception, context);
  }
  public static void LogException(System.Exception exception) { Debug.LogException(exception); }
#endregion

#region Assert
  public static void Assert(bool condition, [CallerMemberName] string memberName = "",
                            [CallerFilePath] string sourceFilePath = "",
                            [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.Assert(
      condition,
      $"{memberName} :: Assertion failed\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
  public static void Assert(bool condition, string message, Object context,
                            [CallerMemberName] string memberName = "",
                            [CallerFilePath] string sourceFilePath = "",
                            [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.Assert(
      condition,
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}",
      context);
  }
  public static void Assert(bool condition, string message,
                            [CallerMemberName] string memberName = "",
                            [CallerFilePath] string sourceFilePath = "",
                            [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.Assert(
      condition,
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
#endregion

#region Break
  public static void Break([CallerMemberName] string memberName = "",
                           [CallerFilePath] string sourceFilePath = "",
                           [CallerLineNumber] int sourceLineNumber = 0)
  {
    Debug.Log(
      $"{memberName} :: Break call\n source file path: {sourceFilePath}:{sourceLineNumber}");
    Debug.Break();
  }
#endregion

#region Assert wrapper

#region IsTrue
  public static void IsTrue(bool condition, string message,
                            [CallerMemberName] string memberName = "",
                            [CallerFilePath] string sourceFilePath = "",
                            [CallerLineNumber] int sourceLineNumber = 0)
  {
    UnityEngine.Assertions.Assert.IsTrue(
      condition,
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
  public static void IsTrue(bool condition, [CallerMemberName] string memberName = "",
                            [CallerFilePath] string sourceFilePath = "",
                            [CallerLineNumber] int sourceLineNumber = 0)
  {
    UnityEngine.Assertions.Assert.IsTrue(
      condition, $"{memberName} :: source file path: {sourceFilePath}:{sourceLineNumber}");
  }
#endregion

#region IsFalse
  public static void IsFalse(bool condition, string message,
                             [CallerMemberName] string memberName = "",
                             [CallerFilePath] string sourceFilePath = "",
                             [CallerLineNumber] int sourceLineNumber = 0)
  {
    UnityEngine.Assertions.Assert.IsFalse(
      condition,
      $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
  public static void IsFalse(bool condition, [CallerMemberName] string memberName = "",
                             [CallerFilePath] string sourceFilePath = "",
                             [CallerLineNumber] int sourceLineNumber = 0)
  {
    UnityEngine.Assertions.Assert.IsFalse(
      condition, $"{memberName} :: source file path: {sourceFilePath}:{sourceLineNumber}");
  }
#endregion

#region IsNull
  public static void IsNull<T>(T value, string message, [CallerMemberName] string memberName = "",
                               [CallerFilePath] string sourceFilePath = "",
                               [CallerLineNumber] int sourceLineNumber = 0)
    where T : class
  {
    UnityEngine.Assertions.Assert.IsNull(
      value, $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
  public static void IsNull<T>(T value, [CallerMemberName] string memberName = "",
                               [CallerFilePath] string sourceFilePath = "",
                               [CallerLineNumber] int sourceLineNumber = 0)
    where T : class
  {
    UnityEngine.Assertions.Assert.IsNull(
      value, $"{memberName} :: source file path: {sourceFilePath}:{sourceLineNumber}");
  }

#endregion

#region IsNotNull
  public static void IsNotNull<T>(T value, string message,
                                  [CallerMemberName] string memberName = "",
                                  [CallerFilePath] string sourceFilePath = "",
                                  [CallerLineNumber] int sourceLineNumber = 0)
    where T : class
  {
    UnityEngine.Assertions.Assert.IsNotNull(
      value, $"{memberName} :: {message}\n source file path: {sourceFilePath}:{sourceLineNumber}");
  }
  public static void IsNotNull<T>(T value, [CallerMemberName] string memberName = "",
                                  [CallerFilePath] string sourceFilePath = "",
                                  [CallerLineNumber] int sourceLineNumber = 0)
    where T : class
  {
    UnityEngine.Assertions.Assert.IsNotNull(
      value, $"{memberName} :: source file path: {sourceFilePath}:{sourceLineNumber}");
  }

#endregion

  // TODO: реализации функционала AreEqual, AreNotEqual, AreApproximatelyEqual,
  // AreNotApproximatelyEqual
  // не рассматриваются в данный момент, так как они не требуются в проекте.
  // их реализация потребует написания тестов, для проверки правильности распределения
  // функционала через generic ограничения

#endregion
}
}  // namespace
