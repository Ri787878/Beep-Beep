using TMPro;
using UnityEngine;


public class ReadAndStoreExample : MonoBehaviour
{
    [SerializeField] private DiceValueReader valueReader;

    public int lastRoll;

    public void ReadNow()
    {
        lastRoll = valueReader.ReadTopValue();
        Debug.Log("Top number is: " + lastRoll);
    }
}