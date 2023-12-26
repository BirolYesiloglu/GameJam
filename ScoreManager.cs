using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject[] blocks; // Assign the blocks in the inspector
    public Sprite greenSprite; // Assign the green sprite in the inspector
    private int score;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        score = 0;
    }

    void Update()
    {
        // Reset the score
        score = 0;

        // Count the blocks with the "Green_Blocks" tag and greenSprite
        foreach (GameObject block in blocks)
        {
            SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();
            if (block.tag == "Green_Blocks" && spriteRenderer.sprite == greenSprite)
            {
                score++;
            }
        }

        // Update the displayed text
        text.text = " " + score;
    }

    public void IncreaseScore(GameObject block)
    {
        if (block.tag == "Green_Blocks")
        {
            score++;
        }
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
    }
}

