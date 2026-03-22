using UnityEngine;

public class PlayerWabble : MonoBehaviour
{
    public float gravForce = 1;
    public float sensibility = 1;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

// Update is called once per frame
    void Update()
    {
        var currentRotation = this.transform.rotation.eulerAngles;
        currentRotation.z = this.transform.rotation.eulerAngles.z > 180
            ? this.transform.rotation.eulerAngles.z - 360
            : this.transform.rotation.eulerAngles.z;
        currentRotation.x = 0;
        currentRotation.y = 0;

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.forward * Time.deltaTime * sensibility);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.back * Time.deltaTime * sensibility);
        }
        else
        {
            this.transform.Rotate(currentRotation * Time.deltaTime * gravForce);
        }
    
        if (currentRotation.z > 90 || currentRotation.z < -90)
        {
            GameController.EndMiniGame(false);
        }
    }

    public void AddWabble(float rotationAddded)
    {
        this.transform.Rotate(Vector3.forward * rotationAddded);
    }
}