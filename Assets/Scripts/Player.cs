using UnityEngine;

public class Player : MonoBehaviour
{
   
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
  
   
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);// rotate left
            }
            else
            {
                transform.Rotate(Vector3.forward * -turnSpeed * Time.deltaTime);//rotate right
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }
    
}
