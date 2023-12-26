/*using System.Collections;
using UnityEngine;

public class changeColour : MonoBehaviour
{
    public GameObject[] colorObjects; // Array to hold the color objects
    public Sprite greenSprite;
    public Sprite redSprite;
    public Sprite blueSprite;
    private bool isBlueSet = false; // Flag to track if the blue color has been set

    void Start()
    {
        StartCoroutine(ChangeColorRandomly());
    }

    IEnumerator ChangeColorRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f); // Wait for 1 second before changing color

            // Randomly select an object from the array
            int randomIndex = Random.Range(0, colorObjects.Length);
            GameObject selectedObject = colorObjects[randomIndex];

            // Change the sprite of the selected object based on the color
            SpriteRenderer objectSpriteRenderer = selectedObject.GetComponent<SpriteRenderer>();
            if (objectSpriteRenderer != null)
            {
                objectSpriteRenderer.sprite = greenSprite;
            }

            // Wait for 0.5 seconds before reverting the sprite to red
            yield return new WaitForSeconds(2f);

            // If the block was not clicked in 0.5 seconds, change the sprite to red
            if (objectSpriteRenderer != null)
            {
                objectSpriteRenderer.sprite = redSprite;
            }

            // Reset the blue set flag
            isBlueSet = false;

            // If blue sprite is not set, set it and exit the loop
            if (!isBlueSet)
            {
                if (objectSpriteRenderer != null)
                {
                    objectSpriteRenderer.sprite = blueSprite;
                }
                isBlueSet = true;
                break; // Exit the loop after setting blue sprite
            }
        }
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public List<GameObject> blueBlocks; // Assign the blue blocks in the inspector
    public Sprite greenSprite; // Assign the green sprite in the inspector
    public Sprite blueSprite; // Assign the blue sprite in the inspector
    public Sprite redSprite;

    void Start()
    {
        StartCoroutine(ChangeRandomBlocks());
    }

    IEnumerator ChangeRandomBlocks()
    {
        while (true)
        {
            // Randomly select two blue blocks
            for (int i = 0; i < 2; i++)
            {
                // Check if there are any blocks left in the list
                if (blueBlocks.Count == 0)
                {
                    Debug.Log("All blocks have been removed.");
                    yield break; // Exit the coroutine
                }

                int index = Random.Range(0, blueBlocks.Count);
                GameObject block = blueBlocks[index];
                blueBlocks.RemoveAt(index); // Remove the block from the list

                // Change the block's color
                SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = greenSprite;

                // Wait for 2 seconds
                yield return new WaitForSeconds(2);

                // If the block is still green, keep it green
                // Otherwise, change it back to blue and add it back to the list
                if (block.tag != "Green_Blocks")
                {
                    spriteRenderer.sprite = blueSprite;
                    gameObject.tag = "Blue_Blocks";
                    blueBlocks.Add(block);
                    Debug.Log(block.name + " changed back to blue.");
                }

            }

            // Wait for a while before changing the next blocks
            yield return new WaitForSeconds(3);
        }
    }
}





