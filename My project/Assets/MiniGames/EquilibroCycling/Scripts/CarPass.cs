using Unity.Mathematics.Geometry;
using UnityEditor;
using UnityEngine;

public class CarPass : MonoBehaviour
{
    public float speed = 20f; // Speed at which the car moves
    public float rotationAddded = 20f;
    public float minTimeToPass = 3f;
    public float maxTimeToPass = 8f;
    private float _timeToPass;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timeToPass = Random.Range(minTimeToPass, maxTimeToPass);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeToPass > 0)
        {
            _timeToPass -= Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            var Wabble = Player.GetComponent<PlayerWabble>();
            Wabble.AddWabble(rotationAddded);
        }
    }
}
