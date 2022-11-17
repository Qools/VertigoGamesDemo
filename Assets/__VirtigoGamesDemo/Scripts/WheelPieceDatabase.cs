using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWheelPieceDatabase", menuName = "Prizes/NewWheelPieceDatabase", order = 1)]
public class WheelPieceDatabase : ScriptableObject
{
    [Header("Basic Prizes")]
    public List<WheelPiece> basicPrizes;

    [Header("Epic Prizes")]
    public List<WheelPiece> epicPrizes;
}
