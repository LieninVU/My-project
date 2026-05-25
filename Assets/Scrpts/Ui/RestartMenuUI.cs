using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartMenuUI : MonoBehaviour
{
    public static RestartMenuUI Instance { get; private set; }
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        restartMenu.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        Player.Instance.OnShowRestartMenu += PlayerOnShowRestartMenu;
    }

    private void PlayerOnShowRestartMenu()
    {
        restartMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        Player.Instance.OnShowRestartMenu -= PlayerOnShowRestartMenu;
    }
}
