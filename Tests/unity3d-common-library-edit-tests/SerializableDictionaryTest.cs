using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Common;

public class SerializableDictionaryTest
{
  [Test]
  public void SerializableDictionaryEqualTest()
  {
    SerializableDictionary<string, bool> dictA =
      new SerializableDictionary<string, bool>() { { "1", true }, { "2", false } };
    SerializableDictionary<string, bool> dictB =
      new SerializableDictionary<string, bool>() { { "1", true }, { "2", false } };
    Assert.IsTrue(dictA.Equals(dictB));
    dictB["2"] = true;
    Assert.IsFalse(dictA.Equals(dictB));
  }
}
