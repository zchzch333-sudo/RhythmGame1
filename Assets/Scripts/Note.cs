
using UnityEngine;

public class Note : MonoBehaviour
{
    public float moveSpeed = 6f;

    private GameObject paddle;
    private GameManager gameManager;

    void Start()
    {
        paddle = GameObject.Find("Paddle");
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        Vector3 position = transform.position;

        position.z -= moveSpeed * Time.deltaTime;

        transform.position = position;

        if (transform.position.z <= 0f)
        {
            float distance = Mathf.Abs(transform.position.x - paddle.transform.position.x);

            gameManager.Judge(distance);

            Destroy(gameObject);
        }
    }
}
