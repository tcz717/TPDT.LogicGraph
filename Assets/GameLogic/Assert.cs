using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Assert
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Test(bool comparison)
    {
        if (!comparison)
        {
            Debug.LogError("Assertion failed");
            Debug.Break();
        }
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Test(bool comparison, System.Func<string> strf)
    {
        if (!comparison)
        {
            Debug.LogError(strf());
            Debug.Break();
        }
    }
}
