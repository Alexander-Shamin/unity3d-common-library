using System;
using UnityEngine;

namespace Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NotNullAttribute : PropertyAttribute
    {
        public bool IgnorePrefab { get; set; }
    }
}
