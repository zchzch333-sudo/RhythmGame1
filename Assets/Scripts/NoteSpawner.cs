
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject slidePrefab;
    public AudioSource music;

    public float noteSpeed = 6f;
    public float spawnZ = 12f;
    public float bpm = 120f;

    public float[] beatNumbers =
    {
    1f, 8.75f, 9.75f, 10.75f, 11.75f,
    12.75f, 13.75f, 14.75f, 15.75f,
    16.75f, 17.75f, 18.75f, 19.75f,
    20.75f, 21.75f, 22.75f, 23.75f
};

    public int[] noteLanes =
    {
    2, 4, 5, 6, 7,
    8, 7, 6, 4,
    3, 5, 7, 8,
    6, 5, 3, 2
};

    public float[] slideBeats = { 30.75f };

    private int nextNote = 0;
    private int nextSlide = 0;
    public GameManager gameManager;

    private bool gameEnded = false;

    void Start()
    {
        music.Play();
    }

    void Update()
    {
        if (gameEnded == true)
        {
            return;
        }
        if (music.isPlaying == false && music.time > 0f)
        {
            gameEnded = true;
            gameManager.EndGame();
            return;
        }

        float beatLength = 60f / bpm;
        float travelTime = spawnZ / noteSpeed;

        // Normal Notes
        if (nextNote < beatNumbers.Length &&
            nextNote < noteLanes.Length)
        {
            float hitTime = beatNumbers[nextNote] * beatLength;

            if (music.time >= hitTime - travelTime)
            {
                float x = -2.75f + noteLanes[nextNote] * 0.5f;

                Vector3 spawnPosition = new Vector3(x, 0.14f, spawnZ);

                Instantiate(notePrefab, spawnPosition, Quaternion.identity);

                nextNote++;
            }
        }

        // Slide Notes
        if (nextSlide < slideBeats.Length)
        {
            float slideHitTime = slideBeats[nextSlide] * beatLength;

            if (music.time >= slideHitTime - travelTime)
            {
                Vector3 spawnPosition = new Vector3(-2f, 0.14f, spawnZ);

                Instantiate(slidePrefab, spawnPosition, Quaternion.identity);

                nextSlide++;
            }
        }
    }
}
