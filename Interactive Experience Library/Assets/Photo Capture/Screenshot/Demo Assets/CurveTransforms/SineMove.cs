using System;
using UnityEngine;

public class SineMove : MonoBehaviour
{
    [SerializeField] bool useLocalAxis;
    [SerializeField] Vector3 direction = new Vector3(1, 0, 0);
    [SerializeField] float speed = 1f;
    [SerializeField] float maxDistance = 1f;

    Vector3 startPosition;
    Vector3 nextPosition;
    // Start is called before the first frame update
    private void Start()
    {
        startPosition = new Vector3();
        startPosition = useLocalAxis ? transform.localPosition : transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        nextPosition = startPosition + direction.normalized * Mathf.Sin(Time.time * speed) * maxDistance;

        if (useLocalAxis)
        {
            transform.localPosition = Vector3.Lerp(nextPosition, transform.localPosition, Time.deltaTime * speed);
        }
        else {
            transform.localPosition = Vector3.Lerp(nextPosition, transform.position, Time.deltaTime * speed);
        }
    }
    
}
