using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureMove : MonoBehaviour
{
    [SerializeField] GameObject head, body;

    [SerializeField] float period = 0.5f;
    [SerializeField] float verticalAmplitude = 0.2f;

    [SerializeField] float headLag = Mathf.PI / 8;

    [Range(0, 1)]
    public float blend = 1;

    float headHeight, bodyHeight;

    void Awake()
    {
        headHeight = head.transform.position.y;
        bodyHeight = body.transform.position.y;
    }

    void LateUpdate()
    {
        float frequency = 2 * Mathf.PI / period;

        var _bodyHeight = Mathf.Lerp(bodyHeight, bodyHeight + verticalAmplitude / 2 * (Mathf.Sin(frequency * Time.time) + 1), blend);
        var position = body.transform.position;
        position.y = _bodyHeight;
        body.transform.position = position;

        var _headHeight = Mathf.Lerp(headHeight, headHeight + verticalAmplitude / 2 * (Mathf.Sin(frequency * Time.time + headLag) + 1), blend);
        position = head.transform.position;
        position.y = _headHeight;
        head.transform.position = position;
    }
}
