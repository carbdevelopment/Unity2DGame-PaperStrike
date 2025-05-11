using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 10f;
    public Transform player;
    public Vector2 spawnOffset = Vector2.zero;
    private Camera mainCamera;
    private float cameraHeight;
    private float cameraWidth;
    private Transform[] spawnPoints;
    private int lastSpawnIndex = -1;
    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Camera not found");
            return;
        }
        InitializeSpawnPoints();
        InvokeRepeating("SpawnObstacle", 0f, spawnInterval);
    }

    private void UpdateCameraParameters()
    {
        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
    }

    private void InitializeSpawnPoints()
    {
        spawnPoints = new Transform[4];
    //TL= top left;
    //TR= top right;
    //BL= bottom left;  
    //BR= bottom right;
        spawnPoints[0] = new GameObject("SpawnPointTL").transform;
        spawnPoints[1] = new GameObject("SpawnPointTR").transform;
        spawnPoints[2] = new GameObject("SpawnPointBL").transform;
        spawnPoints[3] = new GameObject("SpawnPointBR").transform;
        UpdateSpawnPoints();
    }

    private void UpdateSpawnPoints()
    {
        if (spawnPoints == null || spawnPoints.Length < 4)
        {
            Debug.LogError("Spawn poinns not initialized");
            return;
        }
    // update the spawn points, based on the camera position
        spawnPoints[0].position = new Vector3(mainCamera.transform.position.x - cameraWidth, mainCamera.transform.position.y + cameraHeight, 0f);
        spawnPoints[1].position = new Vector3(mainCamera.transform.position.x + cameraWidth, mainCamera.transform.position.y + cameraHeight, 0f);
        spawnPoints[2].position = new Vector3(mainCamera.transform.position.x - cameraWidth, mainCamera.transform.position.y - cameraHeight, 0f);
        spawnPoints[3].position = new Vector3(mainCamera.transform.position.x + cameraWidth, mainCamera.transform.position.y - cameraHeight, 0f);
    }

    private void SpawnObstacle()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }
        UpdateCameraParameters();
        UpdateSpawnPoints();
        int randomSpawnIndex;
        do
        {
            randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        } while (randomSpawnIndex == lastSpawnIndex);
        lastSpawnIndex = randomSpawnIndex;

        Vector3 spawnPosition = spawnPoints[randomSpawnIndex].position;
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }

}
