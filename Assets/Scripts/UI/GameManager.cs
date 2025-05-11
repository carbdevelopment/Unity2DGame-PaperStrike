using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _gameCanvas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        _gameCanvas.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private string saveFilePath = "savegame.dat";
    public static object Instance { get; internal set; }

    [System.Serializable]
    public class SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public int playerScore;
        public SerializableVector3 playerPosition;
    }

    private void SaveGame()
    {
        PlayerData data = new PlayerData();
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = File.Create(saveFilePath))
        {
            formatter.Serialize(fileStream, data);
        }
    }
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(saveFilePath, FileMode.Open))
            {
                PlayerData data = (PlayerData)formatter.Deserialize(fileStream);
            }
        }
        else
        {
            Debug.LogWarning("No saved game found.");
        }
    }
    public void PlayerDied()
    {
        SaveGame();
    }
}
