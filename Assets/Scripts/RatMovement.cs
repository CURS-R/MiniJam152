using UnityEngine;

public class RatMovement : MonoBehaviour
{
    public Transform cheese;
    public Transform spawnPoint;
    private bool hasCheese = false;
    private float speed = 5f; // Movement speed
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = cheese.position; // Initial target is the cheese
    }

    private void Update()
    {
        MoveTowardsAndFaceTarget();

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget < 0.5f) // Check proximity to the target
        {
            if (!hasCheese)
            {
                PickUpCheese();
            }
            else
            {
                // Action upon returning to spawn point with cheese
                Debug.Log("Cheese returned!");
                // Optionally disable the rat or reset for another round
            }
        }
    }

    private void MoveTowardsAndFaceTarget()
    {
        // Move towards the current target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Calculate the direction vector without the y-component
        var direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero) // Ensure there is a direction to look towards
        {
            // Create a rotation looking in the direction of the target
            var lookRotation = Quaternion.LookRotation(direction);
            // Smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    void PickUpCheese()
    {
        hasCheese = true;
        cheese.gameObject.SetActive(false); // Simulate picking up the cheese
        targetPosition = spawnPoint.position; // Change target to spawn point
    }
}