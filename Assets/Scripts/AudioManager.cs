using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //ui cues
    public AudioClip highlightAudio;
    public AudioClip unhighlightAudio;
    public AudioClip clickAudio;
    public AudioClip padActivateAudio;
    public AudioClip timeTravelAudio;
    public AudioClip feedAudio;
    //footsteps
    public Transform character;
    public AudioClip[] grassClips; //soft surface
    public AudioClip[] metalClips; //hard surface

    private AudioSource _source;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _source = gameObject.AddComponent<AudioSource>();
        _source.spatialBlend = 0f;
    }

    public void PlayHighlight() => _source.PlayOneShot(highlightAudio);
    public void PlayUnhighlight() => _source.PlayOneShot(unhighlightAudio);
    public void PlayClick() => _source.PlayOneShot(clickAudio);
    public void PlayPadActivate() => _source.PlayOneShot(padActivateAudio);
    public void PlayTimeTravel() => _source.PlayOneShot(timeTravelAudio);
    public void PlayFeed() => _source.PlayOneShot(feedAudio);

    public void PlayFootstep()
    {
        if (Physics.Raycast(character.position, Vector3.down, out RaycastHit hit, 2f))
        {
            if (hit.collider.tag == "hardSurface")
            {
                Debug.Log("HARD");
            }
            AudioClip[] clips = hit.collider.tag switch
            {
                "hardSurface" => metalClips,
                _ => grassClips
            };

            _source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        character = GameObject.Find("Character").transform;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
