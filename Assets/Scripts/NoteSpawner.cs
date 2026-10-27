
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
    4.156f, 5.202f, 6.106f, 8.336f, 9.252f, 12.404f, 13.318f, 16.244f,
    17.172f, 18.078f, 20.136f, 21.084f, 21.968f, 23.08f, 24.126f, 25.056f,
    26.054f, 27.42f, 29.39f, 30.31f, 31.156f, 32.922f, 35.476f, 36.22f,
    37.524f, 38.828f, 40.133f, 41.437f, 42.741f, 43.393f, 46.002f, 47.307f,
    47.959f, 49.263f, 49.915f, 50.567f, 51.22f, 51.872f, 52.85f, 53.828f,
    56.437f, 57.089f, 58.393f, 60.35f, 61.002f, 61.654f, 62.307f, 63.611f,
    65.567f, 66.22f, 66.872f, 67.524f, 68.176f, 68.828f, 72.089f, 73.393f,
    75.35f, 76.002f, 76.654f, 77.307f, 77.959f, 80.567f, 81.22f, 82.524f,
    83.176f, 83.828f, 85.133f, 87.741f, 88.393f, 89.046f, 89.698f, 91.654f,
    92.959f, 93.611f, 94.263f, 98.176f, 98.828f, 99.807f, 100.785f, 102.741f,
    103.393f, 104.046f, 104.698f, 106.002f, 113.176f, 114.48f, 115.133f, 115.785f,
    116.437f, 117.089f, 119.046f, 119.698f, 121.002f, 122.307f, 123.611f, 124.915f,
    126.22f, 128.828f, 129.48f, 131.437f, 132.089f, 134.046f, 135.35f, 136.654f,
    137.959f, 139.263f, 139.915f, 140.567f, 141.22f, 141.872f, 143.176f, 144.48f,
    145.785f, 147.089f, 154.915f, 156.22f, 158.828f, 159.48f, 160.133f, 160.785f,
    161.437f, 162.089f, 162.741f, 163.393f, 164.046f, 164.698f, 166.002f, 167.307f,
    167.959f, 169.263f, 171.22f, 172.524f, 173.176f, 174.48f, 175.133f, 175.785f,
    176.437f, 177.089f, 179.046f, 187.524f, 188.176f, 188.828f, 190.133f, 192.089f,
    192.741f, 194.046f, 196.654f, 197.307f, 197.959f, 200.241f, 201.872f, 202.524f,
    203.176f, 204.48f, 205.785f, 206.437f, 207.089f, 207.741f, 208.393f, 209.046f
};

    public int[] noteLanes =
    {
    2, 4, 3, 4, 5, 6, 7, 6,
    6, 4, 6, 5, 7, 4, 3, 4,
    4, 3, 2, 7, 3, 7, 4, 5,
    6, 2, 5, 6, 7, 6, 2, 5,
    5, 2, 7, 4, 6, 3, 2, 3,
    3, 5, 3, 4, 5, 5, 6, 5,
    6, 6, 7, 3, 7, 6, 2, 5,
    4, 3, 5, 4, 3, 3, 5, 2,
    3, 3, 4, 5, 7, 5, 6, 7,
    6, 6, 6, 5, 2, 4, 3, 7,
    3, 3, 3, 2, 7, 6, 6, 7,
    7, 6, 2, 6, 2, 6, 3, 4,
    4, 3, 4, 2, 3, 4, 6, 4,
    5, 4, 6, 2, 6, 5, 6, 7,
    7, 4, 7, 4, 3, 7, 2, 3,
    4, 5, 5, 6, 7, 6, 7, 6,
    6, 7, 6, 5, 2, 3, 5, 4,
    4, 3, 2, 4, 7, 5, 7, 6,
    7, 6, 5, 6, 7, 5, 3, 3,
    3, 2, 3, 4, 3, 7, 6, 5
};

    public float[] slideBeats =
    {
    107.946f,
    149.034f,
    181.968f
};


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
