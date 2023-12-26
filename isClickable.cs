using UnityEngine;

public class BlockClick : MonoBehaviour
{
    public Sprite greenSprite; // Assign the green sprite in the inspector
    public Sprite redSprite; // Assign the red sprite in the inspector
    private SpriteRenderer spriteRenderer;
    private bool isClicked = false;
    private float clickTime;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isClicked && Time.time - clickTime < 2)
        {
            gameObject.tag = "Green_Blocks";
            Debug.Log(gameObject.name + " changed to Green_Blocks.");
        }
        else if (isClicked && Time.time - clickTime >= 2)
        {
            isClicked = false;
            Debug.Log(gameObject.name + " did not stay green.");
        }
    }

    void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            clickTime = Time.time;
            Debug.Log(gameObject.name + " was clicked.");

            // Check the sprite and the tag before increasing the score
            if (spriteRenderer.sprite == greenSprite && gameObject.tag == "Green_Blocks")
            {
                // Find the ScoreManager and increase the score
                ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
                if (scoreManager != null)
                {
                    scoreManager.IncreaseScore(gameObject);
                }
            }
        }
    }
}
