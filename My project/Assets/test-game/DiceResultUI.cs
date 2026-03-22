using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class DiceResultUI : MonoBehaviour
{
    public static DiceResultUI Instance;
    
    private DiceValueReader valueReader;
    [SerializeField] private TextMeshProUGUI Dice_Roll;

    public int lastRoll { get; private set; }

    private void Start()
    {
    }

    public int ReadDiceAndShow()
    {
        if (valueReader == null)
        {
            Debug.LogError("DiceResultUI: valueReader not assigned.");
            return 0;
        }

        if (Dice_Roll == null)
        {
            Debug.LogError("DiceResultUI: Dice_Roll text not assigned.");
            return 0;
        }

        lastRoll = valueReader.ReadTopValue();
        Dice_Roll.text = "Rolled: " + lastRoll;
        return lastRoll;
    }
}