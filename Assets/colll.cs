using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class colll : MonoBehaviour
{
    AudioSource source;
    public AudioClip AC;
    public AudioClip carAcc;
    private int score = 0;
    public Text scoreText;
    private int health = 3;
    public Image[] healthImages; // Array of health images

    public Image del;
    private int garbageCount = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
        scoreText.text = "Score: " + score + " / 10";
        UpdateHealthDisplay();

       
    }

    IEnumerator FadeInImage()
    {
        del.gameObject.SetActive(true); // Activate the script image
        Color originalColor = del.color;
        del.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // Set the image's alpha to 0 (fully transparent)

        for (float t = 0.0f; t < 0.7f; t += Time.deltaTime)
        {
            del.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, t / 0.5f)); // Fade-in the image
            yield return null;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        // Debug log to ensure collision detection
        Debug.Log("Collision with: " + col.gameObject.name);

        if (col.gameObject.CompareTag("carar"))
        {
            // Check if the time delay has passed before allowing another collision
            if (Time.time > lastCollisionTime + timeDelay)
            {
                // Update the last collision time
                lastCollisionTime = Time.time;
                

                // Directly decrease health and check if the player has lost the game
                health -= 1;
                Debug.Log("Health decreased to: " + health);
                source.PlayOneShot(carAcc);

                if (health <= 0)
                {
                    StartCoroutine(LoadLoseScene());
                     StartCoroutine(FadeInImage());
                }

                UpdateHealthDisplay();
            }
        }

        if (Time.time > lastCollisionTime + timeDelay)
        {
            if (col.gameObject.CompareTag("Garb"))
            {
                lastCollisionTime = Time.time;
                Destroy(col.gameObject);
                source.PlayOneShot(AC);
                score += 1;
                garbageCount++;

                if (garbageCount == 10)
                {
                    StartCoroutine(LoadWinScene());
                     StartCoroutine(FadeInImage());
                }
            }
        }

        scoreText.text = "Score: " + score + " / 10";
    }

    private void UpdateHealthDisplay()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < health)
            {
                healthImages[i].gameObject.SetActive(true);
            }
            else
            {
                healthImages[i].gameObject.SetActive(false);
            }
        }
    }

    IEnumerator LoadWinScene()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before activating the image
        del.gameObject.SetActive(true); // Activate the script image
        yield return new WaitForSeconds(0.5f); // Wait for another 0.5 seconds
        SceneManager.LoadScene("Win"); // Replace "Win" with the name of your desired scene
        del.gameObject.SetActive(false); // Deactivate the script image
    }

    IEnumerator LoadLoseScene()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before activating the image
        del.gameObject.SetActive(true); // Activate the script image
        yield return new WaitForSeconds(0.5f); // Wait for another 0.5 seconds
        SceneManager.LoadScene("Lose"); // Replace "Lose" with the name of your desired scene
        del.gameObject.SetActive(false); // Deactivate the script image
    }

    private float lastCollisionTime = 0f;
    private float timeDelay = 0.3f; // Time delay in seconds
}

