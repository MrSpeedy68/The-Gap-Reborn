using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject CameroCar;
    [SerializeField] private GameObject F1Car;
     
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneChange;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneChange;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("level") + "-" + PlayerPrefs.GetString("time"), LoadSceneMode.Single);
    }

    private void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            switch (PlayerPrefs.GetString("car"))
            {
                case "Camero":
                    Instantiate(CameroCar); break;
                case "F1":
                    Instantiate(F1Car); break;
            }
        }
    }
}
