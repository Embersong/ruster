using UnityEngine;
using UnityEngine.UI; // Для работы с UI
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное количество жизней
    public int currentHealth;   // Текущее количество жизней

    public Slider healthSlider; // Ссылка на слайдер (полоску здоровья)
    public TMP_Text healthText;     // Ссылка на текст (если нужно отображать цифры)

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        StartCoroutine(HealRoutine());

    }

    IEnumerator HealRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Heal(1); // Передаём параметр
        }
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    // Метод для лечения
    public void Heal(int healAmount = 5)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    // Обновление UI
    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    // Метод для смерти персонажа.
    void Die()
    {
        Debug.Log("Персонаж умер!");
        // Здесь можно добавить перезагрузку уровня или другие действия
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Перезагружаем сцену
        SceneManager.LoadScene(currentSceneName);
    }



}