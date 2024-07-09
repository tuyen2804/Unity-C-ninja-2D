using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartGame()
    {
        // Khôi phục lại thời gian game
        Time.timeScale = 1;

        // Tải lại scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
