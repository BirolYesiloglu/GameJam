using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Try_Again : MonoBehaviour
{
    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Start butonuna bas�ld���nda yeni bir sahneye gecis yap.
        SceneManager.LoadScene("Level1");
    }
}
