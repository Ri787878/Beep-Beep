using System.Collections;
using UnityEngine;

public class DiceRollButtonController : MonoBehaviour
{
    [Header("Dice")]
    [SerializeField] private Rigidbody diceRb;

    [Header("Impulse / Roll Feel")]
    [SerializeField] private float upwardImpulse = 2.5f;
    [SerializeField] private float randomTorque = 0.5f;
    [SerializeField] private float sidewaysImpulse = 1.0f;
    [SerializeField] private float randomSpinTorque = 1.0f;

    [Header("Teleport (only used if out of bounds)")]
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private float teleportY = 1.0f;

    [Header("Board Limits (world space AABB)")]
    [SerializeField] private Vector3 boardMin = new Vector3(-5f, 0f, -5f);
    [SerializeField] private Vector3 boardMax = new Vector3(5f, 0f, 5f);
    [SerializeField] private float edgePadding = 0.3f;

    [Header("Result UI (optional)")]
    [SerializeField] private DiceResultUI resultUI;

    [Header("Settle Detection")]
    [SerializeField] private float minRollTime = 0.25f;
    [SerializeField] private float maxWaitForSettle = 4.0f;
    [SerializeField] private float settleVelocity = 0.05f;
    [SerializeField] private float settleAngularVelocity = 0.05f;

    private Coroutine _rollRoutine;

    public void OnRollButtonClicked()
    {
        if (diceRb == null) return;

        // Stop any previous pending "wait then read" routine.
        if (_rollRoutine != null)
        {
            StopCoroutine(_rollRoutine);
            _rollRoutine = null;
        }

        // If the dice is out of bounds, bring it back first.
        if (!IsWithinBoard(diceRb.position))
        {
            Vector3 destination = teleportTarget != null ? teleportTarget.position : diceRb.position;
            TeleportDiceClamped(destination);
        }

        RollImpulse();

        // Now wait until it settles, then update UI.
        _rollRoutine = StartCoroutine(WaitForSettleThenShow());
    }

    private void RollImpulse()
    {
        diceRb.WakeUp();

        diceRb.AddForce(Vector3.up * upwardImpulse, ForceMode.Impulse);

        Vector3 sideways =
            (Vector3.right * Random.Range(-1f, 1f) +
             Vector3.forward * Random.Range(-1f, 1f)).normalized;

        diceRb.AddForce(sideways * sidewaysImpulse, ForceMode.Impulse);

        float torqueStrength = Mathf.Max(randomTorque, randomSpinTorque);
        if (torqueStrength > 0f)
        {
            Vector3 torque = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-0.3f, 0.3f),
                Random.Range(-1f, 1f)
            ).normalized * torqueStrength;

            diceRb.AddTorque(torque, ForceMode.Impulse);
        }
    }

    private IEnumerator WaitForSettleThenShow()
    {
        // Minimum time so we don't read immediately.
        float t = 0f;
        while (t < minRollTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // Wait until dice is settled (or timeout).
        while (t < maxWaitForSettle)
        {
            t += Time.deltaTime;

            bool settled =
                diceRb.linearVelocity.sqrMagnitude <= settleVelocity * settleVelocity &&
                diceRb.angularVelocity.sqrMagnitude <= settleAngularVelocity * settleAngularVelocity;

            if (settled) break;
            yield return null;
        }

        // Update UI after settle.
        if (resultUI != null)
            resultUI.ReadDiceAndShow();

        _rollRoutine = null;
    }

    private bool IsWithinBoard(Vector3 position)
    {
        float minX = boardMin.x + edgePadding;
        float maxX = boardMax.x - edgePadding;
        float minZ = boardMin.z + edgePadding;
        float maxZ = boardMax.z - edgePadding;

        return position.x >= minX && position.x <= maxX &&
               position.z >= minZ && position.z <= maxZ;
    }

    private void TeleportDiceClamped(Vector3 desiredPosition)
    {
        Vector3 clamped = ClampToBoard(desiredPosition);
        clamped.y = teleportY;

        diceRb.linearVelocity = Vector3.zero;
        diceRb.angularVelocity = Vector3.zero;

        diceRb.position = clamped;
        diceRb.Sleep();
    }

    private Vector3 ClampToBoard(Vector3 position)
    {
        float minX = boardMin.x + edgePadding;
        float maxX = boardMax.x - edgePadding;
        float minZ = boardMin.z + edgePadding;
        float maxZ = boardMax.z - edgePadding;

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        return position;
    }
}