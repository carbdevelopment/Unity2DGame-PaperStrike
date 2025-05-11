using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private AudioSource ded;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float initialFollowSpeed = 2f; 
    public float maxFollowSpeed = 6f;
    public float acceleration = 0.6f;
    public float rotationSpeed = 2f;
    private float currentFollowSpeed;
    void Start()
    {
        currentFollowSpeed = initialFollowSpeed;
    }
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            playerPosition.z = 0f;

            Vector3 directionToPlayer = playerPosition - transform.position;
            Quaternion rotationToPlayer = Quaternion.LookRotation(Vector3.forward, directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToPlayer, Time.deltaTime * currentFollowSpeed);

            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
            currentFollowSpeed = Mathf.Min(currentFollowSpeed + acceleration * Time.deltaTime, maxFollowSpeed);

            transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * currentFollowSpeed);
        }
        else
        {
            Debug.LogWarning("Player not found.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
            ded.Play();
        }
    }
}