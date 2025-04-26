using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WaterDamage : MonoBehaviour
{
    public PostProcessVolume volume;
    private DepthOfField DepthOfField;

    public int damagePerSecond = 5; // Урон в секунду
    public float damageInterval = 1f; // Как часто наносить урон (раз в секунду)
    
    private float timer;
    private PlayerHealth playerHealth; // Ссылка на скрипт здоровья игрока

    private void Start()
    {
        volume.profile.TryGetSettings(out DepthOfField);
    }



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
        DepthOfField.active = true;
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
            DepthOfField.active = false;
        }
    }
}