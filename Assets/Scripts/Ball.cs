using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public float shootSpeed = 10f; // Скорость выстрела для аркадного движения
    public Image arrow;            // UI Image стрелки для направления
    public float respawnTime = 6f; // Время, через которое шарик должен исчезать и появляться заново

    private Rigidbody2D rb;
    private bool isDragging = true; // Флаг, что игрок держит шарик
    private Vector2 shootDirection; // Направление выстрела
    private float shootTimer;       // Таймер для исчезновения шарика
    private Transform spawnPoint;   // Точка спауна шарика

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Находим GameController и получаем точку спауна
        GameController gameController = FindObjectOfType<GameController>();
        if (gameController != null)
        {
            spawnPoint = gameController.shootPoint; // Берём точку спауна из GameController
        }
        else
        {
            Debug.LogError("GameController не найден на сцене!");
        }

        // Отключаем гравитацию, чтобы шарик не падал
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero; // Убедимся, что нет начальной скорости
        rb.isKinematic = true;      // Включаем кинематику, чтобы шарик не двигался при спавне

        // Изначально стрелка скрыта
        arrow.gameObject.SetActive(true);

        // Устанавливаем начальную позицию шарика на точку спауна
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            AimBall(); // Логика для направления шарика
        }

        // Отслеживаем момент отпускания
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseBall(); // Когда отпускаем шарик
        }

        // Проверяем, вышел ли шарик за границы экрана
        if (!isDragging && IsOutOfScreen())
        {
            RespawnBall(); // Перезапуск шарика, если он вышел за границы
        }

        // Таймер для исчезновения шарика
        if (!isDragging)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= respawnTime)
            {
                RespawnBall(); // Перезапуск шарика через 6 секунд
            }
        }
    }

    // Проверка выхода шарика за границы экрана
    bool IsOutOfScreen()
    {
        // Получаем камеру
        Camera camera = Camera.main;
        Vector3 screenPos = camera.WorldToViewportPoint(transform.position);

        // Проверяем, находится ли шарик за пределами видимости
        return screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;
    }

    // Логика для направления шарика (перетягивания)
    void AimBall()
    {
        // Показываем стрелку
        arrow.enabled = true;

        // Получаем позицию курсора или пальца на экране
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Рассчитываем направление от точки шарика до позиции курсора
        shootDirection = (mousePos - (Vector2)transform.position).normalized;

        // Обновляем направление стрелки
        UpdateArrowDirection(shootDirection); // Передаём направление, чтобы стрелка указывала правильно
    }

    // Логика для выстрела
    void ReleaseBall()
    {
        isDragging = false;
        rb.isKinematic = false; // Отключаем кинематику, чтобы шарик мог двигаться
        rb.velocity = shootDirection * shootSpeed; // Устанавливаем мгновенную скорость для аркадного движения

        // Скрываем стрелку, когда шарик летит
        arrow.gameObject.SetActive(false);

        // Запускаем таймер для исчезновения шарика
        shootTimer = 0f;
    }

    // Логика для перезапуска шарика
    public void RespawnBall()
    {
        arrow.gameObject.SetActive(true);
        // Сбрасываем параметры шарика
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        // Возвращаем шарик на точку спауна из GameController
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        // Снова разрешаем игроку перетаскивать шарик
        isDragging = true;
    }

    // Обновление направления стрелки
    void UpdateArrowDirection(Vector2 direction)
    {
        // Вычисляем угол для поворота стрелки
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Угол в радианах
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); // Поворачиваем на -90 градусов

        // Обновляем позицию стрелки (если необходимо)
        if (spawnPoint != null)
        {
            arrow.transform.position = spawnPoint.position; // Стрелка следует за точкой спауна
        }
    }
}
