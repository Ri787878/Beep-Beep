using System;
using UnityEngine;

public class DiceValueReader : MonoBehaviour
{
    public static DiceValueReader Instance { get; private set; }
    [System.Serializable]
    public class Face
    {
        public int value;            // number on this face
        public Transform faceNormal; // forward points out of the face
    }

    [Header("Assign 6 faces here")]
    [SerializeField] private Face[] faces;

    public int CurrentTopValue { get; private set; }
    
    private void Start()
    {
    }

    public int ReadTopValue()
    {
        if (faces == null || faces.Length == 0)
        {
            Debug.LogError("No faces assigned.");
            return 0;
        }

        float bestDot = -Mathf.Infinity;
        int bestValue = 0;

        foreach (var f in faces)
        {
            if (f == null || f.faceNormal == null) continue;

            float dot = Vector3.Dot(f.faceNormal.forward, Vector3.up);
            if (dot > bestDot)
            {
                bestDot = dot;
                bestValue = f.value;
            }
        }

        CurrentTopValue = bestValue;
        return bestValue;
    }
}