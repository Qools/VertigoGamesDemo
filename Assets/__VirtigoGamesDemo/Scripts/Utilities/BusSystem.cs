using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BusSystem
{
    public static Action OnSpinStart;
    public static void CallSpinStart() => OnSpinStart?.Invoke();

    public static Action<WheelPiece> OnSpinEnd;
    public static void CallSpinEnd(WheelPiece reward) => OnSpinEnd?.Invoke(reward);
}
