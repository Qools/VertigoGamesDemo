using System;

public static class BusSystem
{
    public static Action OnSpinStart;
    public static void CallSpinStart() => OnSpinStart?.Invoke();

    public static Action<Reward> OnSpinEnd;
    public static void CallSpinEnd(Reward reward) => OnSpinEnd?.Invoke(reward);

    public static Action OnPopUpCollectButtonClicked;
    public static void CallPopUpCollectButtonClicked() => OnPopUpCollectButtonClicked?.Invoke();
    
    public static Action OnZoneEnded;
    public static void CallZoneEnded() => OnZoneEnded?.Invoke();

    public static Action OnGameOver;
    public static void CallGameOver() => OnGameOver?.Invoke();
}
