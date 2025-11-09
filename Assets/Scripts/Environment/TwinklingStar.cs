using System.Collections;
using UnityEngine;

public class TwinklingStar : MonoBehaviour
{
    [Header("Twinkle")]
    [SerializeField]
    private float _minTwinkleSpeed = 1.0f;
    [SerializeField]
    private float _maxTwinkleSpeed = 25.0f;


    [Header("Size")]
    [SerializeField]
    private float _minSize = 0.2f;
    [SerializeField]
    private float _maxSize = 3.0f;

    private float _twinkleDuration;
    private Material starMaterial;
    private Color _startColor;
    private Color _endColor;
    private Color _tempColor;
    private float _startTime;


    void Start()
    {
        // Create random twinkle speed
        _twinkleDuration = Random.Range (_minTwinkleSpeed, _maxTwinkleSpeed);

        // Create random size for star
        float scale = Random.Range(_minSize, _maxSize);
        transform.localScale = new Vector3(scale, scale, scale);

        // Create random color for _startColor
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float a = 1f;
        _startColor = new Color(r, g, b, a);

        /*
        // Create random color for _endColor
        r = Random.Range(0f, 1f);
        g = Random.Range(0f, 1f);
        b = Random.Range(0f, 1f);
        a = 1f;
        _endColor = new Color(r, g, b, a);
        */
        _endColor = Color.white;


        // Set star color
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            starMaterial = renderer.material;
            starMaterial.SetColor("_Color", _startColor);
            StartCoroutine(TransitionColor());
            //starMaterial.SetColor("_EmissionColor", _startColor);
        }
        else
        {
            Debug.LogError("TwinklingStar script requires a Renderer component on the GameObject.");
            enabled = false;
        }
    }

    IEnumerator TransitionColor()
    {
        _startTime = Time.time;
        float elapsed = 0f;
        while (true)
        {
            while (elapsed < _twinkleDuration)
            {
                elapsed = Time.time - _startTime;
                float t = Mathf.Clamp01(elapsed / _twinkleDuration);
                starMaterial.SetColor("_Color", Color.Lerp(_startColor, _endColor, t));
                yield return null;
            }

            _startTime = Time.time;
            elapsed = 0f;

            _tempColor = _startColor;
            _startColor = _endColor;
            _endColor = _tempColor;
        }
    }
}