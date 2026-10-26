
using UnityEngine;

public class SlideNote : MonoBehaviour
{
    public float moveSpeed = 6f;

    public float slideDuration = 2f;
    public float checkEvery = 0.25f;

    public float startX = -2f;
    public float endX = 2f;

    private GameObject paddle;
    private GameManager gameManager;
    public GameObject slideBody;

    private bool slideStarted = false;
    private float timer = 0f;
    private float checkTimer = 0f;

    void Start()
    {
        paddle = GameObject.Find("Paddle");
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (slideStarted == false)
        {
            Vector3 position = transform.position;
            position.z -= moveSpeed * Time.deltaTime;
            transform.position = position;

            if (transform.position.z <= 0f)
            {
                slideStarted = true;
                timer = 0f;
                checkTimer = 0f;


            }
        }
        else
        {
            timer += Time.deltaTime;
            checkTimer += Time.deltaTime;

            float slideProgress = timer / slideDuration;


            if (slideProgress > 1f)
            {
                slideProgress = 1f;
            }
            Vector3 bodyScale = slideBody.transform.localScale;
            bodyScale.z = 5f * (1f - slideProgress);
            slideBody.transform.localScale = bodyScale;

            float x = startX + (endX - startX) * slideProgress;

            transform.position = new Vector3(x, 0.14f, 0f);

            if (checkTimer >= checkEvery)
            {
                checkTimer -= checkEvery;

                float distance = Mathf.Abs(
                    transform.position.x - paddle.transform.position.x
                );

                gameManager.Judge(distance);
            }

            if (timer >= slideDuration)
            {
                Destroy(gameObject);
            }
        }
    }
}
