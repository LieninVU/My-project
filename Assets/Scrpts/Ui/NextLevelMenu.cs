using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelMenu : MonoBehaviour
{
    public static NextLevelMenu Instance { get; private set; }
    [SerializeField] private GameObject _nextLevelMenu;
    [SerializeField] private NextLevelArea _nextLevelArea;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _nextLevelButton;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _nextLevelMenu.SetActive(false);
        _restartButton.onClick.AddListener(RestartScene);
        _nextLevelButton.onClick.AddListener(NextLevel);
        _nextLevelArea.OnShowNextLevelMenu += ShowNextLevelMenu;
    }

    private void ShowNextLevelMenu()
    {
        _nextLevelMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            Debug.Log("All Levels was gone");
    }

    private void OnDestroy()
    {
        _nextLevelArea.OnShowNextLevelMenu -= ShowNextLevelMenu;
    }
}
