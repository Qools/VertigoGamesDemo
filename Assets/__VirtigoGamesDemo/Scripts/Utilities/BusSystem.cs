using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BusSystem
{
    public static Action OnSpinStart;
    public static void CallSpinStart() => OnSpinStart?.Invoke();

    public static Action<Reward> OnSpinEnd;
    public static void CallSpinEnd(Reward reward) => OnSpinEnd?.Invoke(reward);

    public static Action<Reward> OnPopUpScreenOpened;
    public static void CallPopUpScreenOpened(Reward reward) => OnPopUpScreenOpened?.Invoke(reward);
}
