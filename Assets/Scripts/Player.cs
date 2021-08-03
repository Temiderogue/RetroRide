using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{
    public float Speed;
    [SerializeField] private float  _slideSpeed;
    [SerializeField] private Rigidbody _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameCycle _gameCycle;

    [SerializeField] private float _distance = 1;
    [SerializeField] private int _targetRow = 0;
    [SerializeField] private float _firstLanePos;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private Camera _camera;
    [SerializeField] private PostProcessVolume _postProcess;
    [SerializeField] private float waitTime1;
    [SerializeField] private float waitTime2;
    [SerializeField] private AudioSource _boost;
    private LensDistortion _distortion;
    public int[] _rows = new int[] { -1, 0, 1 };

    private float _lensValue = 0;
    private float elapsedTime = 0,endValue = -50;
    private float _startScale = 1, _endScale;
    

    private void Start()
    {
        _distortion = _postProcess.profile.GetSetting<LensDistortion>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Data.isAlive)
        {
            if (Input.mousePosition.x > Screen.width*2/3 && _targetRow < 2)
            {
                _targetRow++;
            }
            else if(Input.mousePosition.x < Screen.width / 3 && _targetRow > 0)
            {
                _targetRow--;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Speed);
        transform.position = Vector3.Lerp(transform.position, new Vector3(_firstLanePos + (_targetRow * _distance), 0,transform.position.z), _slideSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Cone":
                StartCoroutine(_deathLerp());
                _gameCycle.Death();
                
                break;
            case "Boost":
                _explosion.transform.position = transform.position;
                _explosion.Play();
                _boost.Play();
                Speed += 0.01f;
                StartCoroutine(_cameraLerp());
                break;
            case "TriggerZone":
                _spawner.ChangeBlockPosition();
                break;
        }
    }

    private IEnumerator _cameraLerp()
    {
        float timeElapsed = 0;

        while (timeElapsed < waitTime1)
        {
            _distortion.intensity.value = Mathf.Lerp(0, endValue, timeElapsed / waitTime1);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        _distortion.intensity.value = endValue;

        timeElapsed = 0;

        while (timeElapsed < waitTime2)
        {
            _distortion.intensity.value = Mathf.Lerp(endValue, 0, timeElapsed / waitTime2);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        _distortion.intensity.value = 0;
    }

    private IEnumerator _deathLerp()
    {
        float timeElapsed = 0;

        while (timeElapsed < 1)
        {
            _distortion.intensity.value = Mathf.Lerp(_distortion.intensity.value, -40, timeElapsed / 3);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        _distortion.intensity.value = -40;
    }
}
