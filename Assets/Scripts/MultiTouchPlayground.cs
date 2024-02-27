using UnityEngine;

public class MultiTouchPlayground : MonoBehaviour
{
    public GameObject spherePrefab;

    void Update()
    {
        // Check if there are any touches
        if (Input.touchCount > 0)
        {
            // Iterate through each touch
            foreach (Touch touch in Input.touches)
            {
                // Check if the touch phase is began (touch just started)
                if (touch.phase == TouchPhase.Began)
                {
                    // Convert touch position to world space
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                    // Set z position to ensure the sphere appears in front of other objects
                    touchPosition.z = 0f; // Adjust this value if needed

                    // Create a new sphere at the touch position
                    GameObject newSphere = Instantiate(spherePrefab, touchPosition, Quaternion.identity);
                    float newSize = 2.0f; // You can adjust this value to change the size of the sphere

                    // Adjust the scale of the new sphere
                    newSphere.transform.localScale = new Vector3(newSize, newSize, newSize);
                    
                    // Attach a unique identifier to the sphere
                    newSphere.name = "Sphere" + touch.fingerId;
                }
                // Check if the touch phase is ended (finger removed)
                else if (touch.phase == TouchPhase.Ended)
                {
                    // Find and destroy the corresponding sphere
                    GameObject sphereToRemove = GameObject.Find("Sphere" + touch.fingerId);
                    if (sphereToRemove != null)
                    {
                        Destroy(sphereToRemove);
                    }
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    // Find the corresponding sphere
                    GameObject sphereToMove = GameObject.Find("Sphere" + touch.fingerId);
                    if (sphereToMove != null)
                    {
                        // Convert touch position to world space
                        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                        // Update the position of the sphere
                        sphereToMove.transform.position = touchPosition;
                    }
                }
            }
        }
    }
}
