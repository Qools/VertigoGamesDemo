using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static int GetIntValue(string _key)
    {
        int value = 0;

        if (PlayerPrefs.HasKey(_key))
        {
            value = PlayerPrefs.GetInt(_key);
        }

        else
        {
            SetIntValue(_key, value);
        }

        return value;
    }

    public static void SetIntValue(string _key, int _value)
    {
        PlayerPrefs.SetInt(_key, _value);
    }
}
