using TMPro;
using UnityEngine;

public class DiceResultUI : MonoBehaviour
{
    [SerializeField] private DiceValueReader valueReader;
    [SerializeField] private TextMeshProUGUI Dice_Roll;

    public int lastRoll { get; private set; }

    public void ReadDiceAndShow()
    {
        if (valueReader == null)
        {
            Debug.LogError("DiceResultUI: valueReader not assigned.");
            return;
        }

        if (Dice_Roll == null)
        {
            Debug.LogError("DiceResultUI: Dice_Roll text not assigned.");
            return;
        }

        lastRoll = valueReader.ReadTopValue();
        Dice_Roll.text = "Rolled: " + lastRoll;
    }
}