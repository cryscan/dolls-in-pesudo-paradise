using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigureAnimation : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Transform[] attaches;

    [SerializeField] float period = 0.5f;
    [SerializeField] float verticalAmplitude = 0.2f;

    [SerializeField] float attachLag = Mathf.PI / 8;

    [Range(0, 1)]
    [SerializeField] float targetBlend = 0;
    [SerializeField] float blendFallout = 5;

    float bodyHeight;
    float[] attachHeights;
    float blend;

    void Awake()
    {
        bodyHeight = body.transform.position.y;
        attachHeights = attaches.Select(_transform => _transform.position.y).ToArray();
    }

    void Update()
    {
        blend = Mathf.Lerp(blend, targetBlend, 1 - Mathf.Exp(-blendFallout * Time.deltaTime));
    }

    void LateUpdate()
    {
        float frequency = 2 * Mathf.PI / period;

        var _bodyHeight = Mathf.Lerp(bodyHeight, bodyHeight + verticalAmplitude / 2 * (Mathf.Sin(frequency * Time.time) + 1), blend);
        var position = body.position;
        position.y = _bodyHeight;
        body.position = position;

        for (int i = 0; i < attaches.Length; ++i)
        {
            var _transform = attaches[i];
            var height = attachHeights[i];

            var _height = Mathf.Lerp(height, height + verticalAmplitude / 2 * (Mathf.Sin(frequency * Time.time + attachLag) + 1), blend);
            position = _transform.transform.position;
            position.y = _height;
            _transform.position = position;
        }
    }

    public void SetTargetBlend(float blend) => targetBlend = blend;
}
