using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSelector : MonoBehaviour
{
    public Text scoreText;

    private List<GameObject> blueBlocks = new List<GameObject>();

    private int greenScore = 0;
    private int redScore = 0;
    public Sprite greenSprite;
    public Sprite redSprite;
    public Sprite blueSprite;

    void Start()
    {
        GameObject[] allBlueBlocks = GameObject.FindGameObjectsWithTag("Blue_Blocks");
        blueBlocks.AddRange(allBlueBlocks);

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (greenScore < 41 && redScore < 41)
        {
            yield return StartCoroutine(SelectBlocks());
        }

        ResetGame();
        StartCoroutine(GameLoop());
    }

    private IEnumerator SelectBlocks()
    {
        while (blueBlocks.Count > 0)
        {
            int randomIndex = Random.Range(0, blueBlocks.Count);
            GameObject selectedBlock = blueBlocks[randomIndex];

            bool isGreen = Random.Range(0, 2) == 0;

            bool isClicked = false;

            while (!isClicked)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // Change the sprite color
                    if (isGreen)
                    {
                        selectedBlock.GetComponent<SpriteRenderer>().sprite = greenSprite;
                        selectedBlock.tag = "Green_Blocks";
                    }
                    else
                    {
                        selectedBlock.GetComponent<SpriteRenderer>().sprite = redSprite;
                        selectedBlock.tag = "Red_Blocks";
                    }

                    // Disable further clicking on this block if it has a BoxCollider2D
                    BoxCollider2D collider = selectedBlock.GetComponent<BoxCollider2D>();
                    if (collider != null)
                    {
                        collider.enabled = false;
                    }

                    // Remove the block from the list
                    blueBlocks.Remove(selectedBlock);

                    isClicked = true;
                }

                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void ResetGame()
    {
        greenScore = 0;
        redScore = 0;
        scoreText.text = "0";

        ResetBlocks("Green_Blocks");
        ResetBlocks("Red_Blocks");
    }

    void ResetBlocks(string tag)
    {
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag(tag);
        foreach (var block in allBlocks)
        {
            // Reset block properties
            BoxCollider2D collider = block.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            block.tag = "Blue_Blocks";
            block.GetComponent<SpriteRenderer>().sprite = blueSprite; // Set to default sprite
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject clickedBlock = hit.collider.gameObject;

                if (clickedBlock.tag == "Green_Blocks")
                {
                    greenScore++;
                    // Green block clicked: update the score
                    scoreText.text = greenScore.ToString("0");
                }
                else if (clickedBlock.tag == "Red_Blocks")
                {
                    redScore++;
                    // Red block clicked: update the score
                    scoreText.text = redScore.ToString("0");
                }

                // Disable further clicking on this block if it has a BoxCollider2D
                BoxCollider2D collider = clickedBlock.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }
    }
}





/*
    * 1. TTT1_Blue tag'li objeleri se�
    * 2. TTT1_Blue tag'li objelerin say�s�n� bul
    * 3. Mavi objelerden rastgele birini se�
    * 4. Se�ilen mavi objeyi k�rm�z� yap
    * 5. Bu se�ilen objeyi bir daha se�ilememesi i�in tag'ini de�i�tir
    * 6. TTT1_BLUE tag'li objeleri se�
    * 7. TTT1_Blue tag'li objelerin say�s�n� bul
    * 8. Mavi objelerden rastgele birini se�
    * 9. Se�ilen mavi objeyi sprite renderer'� kullanarak sprite'�n� green yap
    * 10. Bu se�ilen objeyi bir daha se�ilememesi i�in tag'ini de�i�tir


    9 mavi blokta ye�il yanan bloklara oyuncu t�klayacak, ye�il bloklar�n tag'ini TTT1_Green yapacak
    ayn� zamanda yanan k�rm�z� blo�a basarsa o blok rakibe ge�ecek, tag'ini TTT1_Red yapacak
    9 turuncu blo�un i�lerinde 9'ar mavi blok var, bu turuncu bloklardan rastgele birka�� se�ilecek
    se�ilen her turuncu blo�un i�indeki 1 mavi blok ye�il yanacak, 1 mavi blok k�rm�z� yanacak oyuncu bunlara t�klayacak
    k�rm�z�ya t�klarsa rakibe ge�ecek, ye�ile t�klarsa kendisine ge�ecek. Oyun alan�nda kalan yani toplamda 81 bloktan 41'ini
    kendi rengi yapan kazanacak. K�rm�z� ve ye�ili oyunda bir counter ile tutaca��z ve oyunculara g�sterece�iz.
*/