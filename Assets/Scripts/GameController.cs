using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject ballPrefab; // Префаб шарика
    public Transform shootPoint; // Точка, откуда будет выпущен шарик
    public TextMeshProUGUI[] timerText; // Текст для отображения таймера
    public GameObject gameMessage;
    public float levelTime = 60f; // Время на прохождение уровня
    public GameObject winMessage; // Объект, который отображает сообщение о выигрыше
    public GameObject loseMessage; // Объект, который отображает сообщение о проигрыше
    [SerializeField] private AudioSource audioComponenDestroy;
    private float currentTime;
    private bool levelCompleted = false;
    private BlockSpawner blockSpawner; // Ссылка на BlockSpawner
    private int totalBlocks; // Общее количество блоков на уровне
    private int destroyedBlocks = 0; // Количество уничтоженных блоков
    private Ball _ball;

    void Start()
    {
        Time.timeScale = 1f;
        currentTime = levelTime;
        blockSpawner = FindObjectOfType<BlockSpawner>();
        if (blockSpawner != null)
        {
            totalBlocks = blockSpawner.numberOfBlocks; // Получаем количество блоков из BlockSpawner
        }
        else
        {
            Debug.LogError("BlockSpawner не найден на сцене!");
        }

        SpawnBall();
        winMessage.SetActive(false);
        loseMessage.SetActive(false);
    }

    void Update()
    {
        if (!levelCompleted)
        {
            // Обновляем таймер, если чит-код не включен
            if (!GameSettingsManager.Instance.cheatEnabled)
            {
                currentTime -= Time.deltaTime;
                foreach (var textTimerIndex in timerText)
                {
                    textTimerIndex.text = $"Time: {Mathf.CeilToInt(currentTime).ToString()} seconds";
                }

                if (currentTime <= 0)
                {
                    GameOver();
                }
            }
            else
            {
                // Если чит-код включен, показываем, что таймер отключен
                foreach (var textTimerIndex in timerText)
                {
                    textTimerIndex.text = "Time: OFF"; // Пример текста, когда таймер отключен
                }
            }

            // Проверяем, сбиты ли все блоки
            if (destroyedBlocks >= totalBlocks)
            {
                LevelCompleted();
            }
        }
    }

    public void OnTimeScaleRelease()
    {
        _ball = FindObjectOfType<Ball>();
        _ball.RespawnBall();
        Time.timeScale = 1f;
    }
    
    public void OffTimeScaleRelease()
    {
        
        Time.timeScale = 0f;
    }

    void SpawnBall()
    {
        // Спавним шарик как дочерний объект у shootPoint
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity, shootPoint);
    }

    public void BlockDestroyed()
    {
        audioComponenDestroy.Play();
        destroyedBlocks++; // Увеличиваем количество уничтоженных блоков
    }

    void GameOver()
    {
        gameMessage.SetActive(false);
        levelCompleted = true;
        loseMessage.SetActive(true);
        Debug.Log("Game Over! Time's up.");
        Time.timeScale = 0; // Останавливаем игру
    }

    void LevelCompleted()
    {
        gameMessage.SetActive(false);
        levelCompleted = true;
        winMessage.SetActive(true);
        Debug.Log("Level Completed!");
        Time.timeScale = 0; // Останавливаем игру
    }
}
