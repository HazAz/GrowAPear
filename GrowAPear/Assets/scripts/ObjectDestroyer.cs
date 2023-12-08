using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Rigidbody2D otherRigidbody = other.GetComponent<Rigidbody2D>();

        if (otherRigidbody != null)
        {
            
            Destroy(other.gameObject);
        }
    }
}
