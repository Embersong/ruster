using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    public int damagePerSecond = 5; // Урон в секунду
    public float damageInterval = 1f; // Как часто наносить урон (раз в секунду)
    
    private float timer;
    private PlayerHealth playerHealth; // Ссылка на скрипт здоровья игрока

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, вошёл ли игрок в воду
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Если игрок в воде, наносим периодический урон
        if (playerHealth != null && other.CompareTag("Player"))
        {
            timer += Time.deltaTime;
            
            if (timer >= damageInterval)
            {
                playerHealth.TakeDamage(damagePerSecond);
                timer = 0f;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Игрок вышел из воды — обнуляем ссылку
        if (other.CompareTag("Player"))
        {
            playerHealth = null;
            timer = 0f;
        }
    }
}