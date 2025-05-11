using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject obstaclePrefab; 
    public GameObject particleEffectPrefab; 
    private ObstacleSpawner obstacleSpawner;
    public void SetObstacleSpawner(ObstacleSpawner spawner)
    {
        obstacleSpawner = spawner;
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") && obstaclePrefab.CompareTag("Obstacle"))
        {
            Vector2 collisionPoint = other.ClosestPoint(transform.position);
            Debug.Log("Collision Point: " + collisionPoint);
            PlayParticleEffect(collisionPoint);
        }
    }

    private void PlayParticleEffect(Vector3 position)
    {
        GameObject particleEffect = Instantiate(particleEffectPrefab, position, Quaternion.identity);
        Destroy(particleEffect, 2.0f); 
    }
}

