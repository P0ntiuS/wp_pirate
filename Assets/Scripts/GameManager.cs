using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int enemiesLeft;
    private readonly string enemiesLeftTextLable = "Врагов осталось: ";
    private Text enemiesLeftText;
    private GameObject healthBar;

    void Start()
    {
        healthBar = GameObject.Find("UISystem/PlayerUI/HealthBar");

        enemiesLeftText = GameObject.Find("UISystem/PlayerUI/EnemiesLeft").GetComponent<Text>();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;
        UpdateEnemiesLeftText(enemiesLeft);
    }

    public void OnDeathEnemyUpdateUi()
    {
        enemiesLeft -= 1;
        if (enemiesLeft <= 0)
		{
            Debug.Log("YOU WIN!!!");
        }
        UpdateEnemiesLeftText(enemiesLeft);
    }

    public void CloseApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void LoadScene(string name)
	{
        SceneManager.LoadScene(name);
	}

    public void RestartScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void UpdateHealthBar()
	{
        if (healthBar != null)
		{
            var hearts = healthBar.GetComponentsInChildren<Image>();
            var heart = hearts.Last(h => h.enabled == true);
            heart.enabled = false;
        }
    }

    private void UpdateEnemiesLeftText(int count)
	{
        if (enemiesLeftText != null)
		{
            enemiesLeftText.text = enemiesLeftTextLable + count;
        }
    }
}
