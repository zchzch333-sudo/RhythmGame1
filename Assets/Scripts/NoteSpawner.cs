
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
    17.172f, 18.078f, 20.136f, 21.084f, 21.968f, 23.080f, 24.126f, 25.056f,
    26.054f, 27.420f, 35.476f, 36.220f, 36.546f, 37.524f, 38.176f, 38.502f,
    38.828f, 39.154f, 40.133f, 41.111f, 41.437f, 42.741f, 43.393f, 43.720f,
    46.002f, 47.307f, 47.959f, 48.611f, 49.263f, 49.915f, 50.567f, 51.220f,
    51.872f, 59.046f, 59.372f, 59.698f, 60.350f, 60.676f, 61.002f, 61.654f,
    62.307f, 68.828f, 69.480f, 70.133f, 70.785f, 71.437f, 72.089f, 72.741f,
    73.393f, 74.698f, 75.350f, 76.002f, 76.654f, 77.307f, 77.959f, 80.241f,
    80.567f, 81.220f, 81.872f, 82.524f, 89.046f, 89.698f, 91.002f, 91.654f,
    92.307f, 92.959f, 93.611f, 94.263f, 95.567f, 95.893f, 96.220f, 98.176f,
    98.828f, 99.807f, 100.785f, 101.763f, 102.089f, 102.415f, 102.741f, 103.393f,
    104.046f, 104.698f, 105.024f, 105.676f, 106.002f, 113.176f, 113.828f, 114.480f,
    115.133f, 115.459f, 115.785f, 116.437f, 116.763f, 117.089f, 118.067f, 118.393f,
    119.046f, 119.698f, 120.676f, 121.002f, 121.654f, 121.980f, 122.307f, 122.959f,
    123.285f, 123.611f, 124.589f, 124.915f, 125.893f, 126.220f, 128.176f, 128.502f,
    128.828f, 129.480f, 129.807f, 130.133f, 131.111f, 131.437f, 132.089f, 132.741f,
    133.067f, 134.046f, 134.698f, 135.024f, 135.350f, 135.676f, 136.328f, 136.654f,
    137.633f, 137.959f, 139.263f, 139.915f, 140.567f, 141.220f, 141.872f, 142.524f,
    142.850f, 143.176f, 143.828f, 144.154f, 144.480f, 145.133f, 145.459f, 145.785f,
    146.437f, 146.763f, 147.089f, 147.741f, 154.263f, 154.589f, 154.915f, 155.567f,
    155.893f, 156.220f, 156.872f, 157.198f, 157.524f, 157.850f, 158.502f, 158.828f,
    159.154f, 159.480f, 160.133f, 160.785f, 161.437f, 162.089f, 162.741f, 163.393f,
    163.720f, 164.046f, 164.698f, 165.024f, 166.002f, 167.307f, 167.959f, 168.285f,
    168.611f, 169.263f, 169.915f, 170.567f, 170.893f, 171.220f, 171.872f, 172.524f,
    173.176f, 173.502f, 173.828f, 174.480f, 174.807f, 175.133f, 175.785f, 176.437f,
    177.089f, 178.067f, 179.046f, 180.024f, 187.198f, 187.524f, 188.176f, 188.502f,
    188.828f, 190.133f, 191.111f, 191.763f, 192.089f, 192.741f, 193.720f, 194.046f,
    196.654f, 197.307f, 197.633f, 197.959f, 198.937f, 205.459f, 205.785f, 206.437f,
    207.089f, 207.741f, 208.393f, 209.046f, 209.698f, 212.959f, 214.263f
};

    public int[] noteLanes =
    {
    8, 5, 3, 6, 2, 1, 4, 7, 3, 6, 8, 5,
    2, 4, 7, 5, 1, 3, 6, 8, 7, 4, 2, 3,
    1, 2, 5, 8, 6, 3, 2, 1, 4, 7, 8, 6,
    3, 2, 4, 7, 5, 8, 7, 6, 2, 1, 3, 5,
    7, 8, 6, 3, 1, 4, 2, 6, 8, 4, 7, 5,
    3, 1, 2, 5, 7, 8, 6, 3, 1, 4, 6, 8,
    7, 4, 2, 5, 3, 1, 2, 4, 7, 8, 6, 3,
    1, 2, 4, 6, 8, 5, 3, 1, 2, 7, 4, 5,
    8, 7, 6, 3, 1, 2, 6, 8, 5, 3, 1, 2,
    4, 6, 8, 7, 5, 4, 1, 3, 7, 8, 6, 5,
    3, 1, 2, 4, 8, 6, 3, 1, 2, 5, 7, 8,
    6, 4, 2, 1, 5, 7, 3, 1, 4, 6, 8, 7,
    5, 3, 2, 1, 3, 6, 4, 2, 5, 7, 8, 4,
    6, 8, 7, 5, 3, 1, 2, 4, 6, 8, 5, 3,
    1, 2, 4, 7, 8, 6, 3, 7, 5, 4, 1, 2,
    6, 8, 5, 3, 1, 4, 7, 8, 6, 5, 2, 3,
    7, 6, 4, 2, 1, 3, 5, 8, 6, 1, 4, 7,
    8, 6, 3, 2, 1, 5, 7, 4, 2, 5, 8, 6,
    3, 1, 2, 4, 8, 7, 6, 3, 1, 5, 7, 4,
    1, 5, 8
};

    public float[] slideBeats =
    {
    29.390f, 53.828f, 63.611f, 83.828f, 107.946f, 149.034f, 181.968f, 200.241f
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
