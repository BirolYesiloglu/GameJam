using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Start butonuna basıldığında yeni bir sahneye gecis yap.
        SceneManager.LoadScene("Level1");
    }
}