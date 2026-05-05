using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{

    public static WinMenu Instance { get; private set; }
    [SerializeField] private GameObject winMenu;
    [SerializeField] private Button restartButton;
    [SerializeField] private WinArea winArea;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        winMenu.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        winArea.OnShowWinMenu += PlayerOnShowWinMenu;
    }

    private void PlayerOnShowWinMenu()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        winArea.OnShowWinMenu -= PlayerOnShowWinMenu;
    }
}
