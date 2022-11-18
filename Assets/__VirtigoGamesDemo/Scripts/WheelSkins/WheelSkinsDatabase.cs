using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWheelSkinsDatabase", menuName = "WheelSkin/NewWheelSkinsDatabase", order = 1)]
public class WheelSkinsDatabase : ScriptableObject
{
    public List<WheelSkins> wheelSkins = new List<WheelSkins>();
}
