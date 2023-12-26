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
    * 1. TTT1_Blue tag'li objeleri seç
    * 2. TTT1_Blue tag'li objelerin sayýsýný bul
    * 3. Mavi objelerden rastgele birini seç
    * 4. Seçilen mavi objeyi kýrmýzý yap
    * 5. Bu seçilen objeyi bir daha seçilememesi için tag'ini deðiþtir
    * 6. TTT1_BLUE tag'li objeleri seç
    * 7. TTT1_Blue tag'li objelerin sayýsýný bul
    * 8. Mavi objelerden rastgele birini seç
    * 9. Seçilen mavi objeyi sprite renderer'ý kullanarak sprite'ýný green yap
    * 10. Bu seçilen objeyi bir daha seçilememesi için tag'ini deðiþtir


    9 mavi blokta yeþil yanan bloklara oyuncu týklayacak, yeþil bloklarýn tag'ini TTT1_Green yapacak
    ayný zamanda yanan kýrmýzý bloða basarsa o blok rakibe geçecek, tag'ini TTT1_Red yapacak
    9 turuncu bloðun içlerinde 9'ar mavi blok var, bu turuncu bloklardan rastgele birkaçý seçilecek
    seçilen her turuncu bloðun içindeki 1 mavi blok yeþil yanacak, 1 mavi blok kýrmýzý yanacak oyuncu bunlara týklayacak
    kýrmýzýya týklarsa rakibe geçecek, yeþile týklarsa kendisine geçecek. Oyun alanýnda kalan yani toplamda 81 bloktan 41'ini
    kendi rengi yapan kazanacak. Kýrmýzý ve yeþili oyunda bir counter ile tutacaðýz ve oyunculara göstereceðiz.
*/