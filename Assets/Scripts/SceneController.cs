using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("level") + "-" + PlayerPrefs.GetString("time"));
    }
}
