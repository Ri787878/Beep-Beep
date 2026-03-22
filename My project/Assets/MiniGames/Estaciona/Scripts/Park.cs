using UnityEngine;

public class ParkingZone : MonoBehaviour
{
void Update()
{
    var player = GameObject.FindGameObjectWithTag("Player");
    if (IsCompletelyInside(player, gameObject))
    {
        GameController.EndMiniGame(true);
    }
}

public static bool IsCompletelyInside(GameObject inner, GameObject outer)
{
    Collider innerCol = inner.GetComponent<Collider>();
    Collider outerCol = outer.GetComponent<Collider>();

    if (innerCol == null || outerCol == null)
    {
        Debug.LogWarning("One or both objects are missing a Collider.");
        return false;
    }

    Bounds innerBounds = innerCol.bounds;
    Bounds outerBounds = outerCol.bounds;

    // Check all 8 corners of the inner bounds
    Vector3[] corners = new Vector3[8];

    corners[0] = new Vector3(innerBounds.min.x, 0, innerBounds.min.z);
    corners[1] = new Vector3(innerBounds.min.x, 0, innerBounds.max.z);
    corners[2] = new Vector3(innerBounds.min.x, 0, innerBounds.min.z);
    corners[3] = new Vector3(innerBounds.min.x, 0, innerBounds.max.z);
    corners[4] = new Vector3(innerBounds.max.x, 0, innerBounds.min.z);
    corners[5] = new Vector3(innerBounds.max.x, 0, innerBounds.max.z);
    corners[6] = new Vector3(innerBounds.max.x, 0, innerBounds.min.z);
    corners[7] = new Vector3(innerBounds.max.x, 0, innerBounds.max.z);

    foreach (var corner in corners)
    {
        if (!outerBounds.Contains(corner))
            return false;
    }

    return true;
}


void OnTriggerEnter(Collider other)
{
    //if (other.CompareTag("Player"))
        //GameController.EndMiniGame(true);

}
}