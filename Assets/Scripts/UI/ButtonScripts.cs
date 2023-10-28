using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public void LoadFreeScene()
    {
        SceneManager.LoadScene("Free");
    }

    public void LoadDenseScene()
    {
        SceneManager.LoadScene("Dense");
    }

    public void LoadSparseScene()
    {
        SceneManager.LoadScene("Sparse");
    }

    public void LoadSharkScene()
    {
        SceneManager.LoadScene("Shark");
    }

    public void LoadObstacleScene()
    {
        SceneManager.LoadScene("Obstacle");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
