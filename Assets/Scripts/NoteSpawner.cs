
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject slidePrefab;
    public AudioSource music;

    public float noteSpeed = 6f;
    public float spawnZ = 12f;
    public float bpm = 120f;
    public TextAsset chartFile;


    private float[] beatNumbers;
    private int[] noteLanes;
    private float[] slideBeats;


    private int nextNote = 0;
    private int nextSlide = 0;
    public GameManager gameManager;

    private bool gameEnded = false;

    void Start()
    {
        music.Play();
        NoteChart chart = JsonUtility.FromJson<NoteChart>(chartFile.text);

        beatNumbers = chart.beatNumbers;
        noteLanes = chart.noteLanes;
        slideBeats = chart.slideBeats;
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
[System.Serializable]
public class NoteChart
{
    public float[] beatNumbers;
    public int[] noteLanes;
    public float[] slideBeats;
}