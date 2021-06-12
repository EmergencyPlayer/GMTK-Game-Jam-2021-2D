using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueLine : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private int resolution, waveCount, wobbleCount;
    [SerializeField] private float waveSize, animSpeed;

    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeLine(gameObject go)
    {
        StartCoroutine(AnimateRope(go.transform.position));
    }

    private IEnumerator AnimateRope(Vector3 targetPos)
    {
        line.positionCount = resolution;

        float percent = 0;
        while (percent <= 1f)
        {
            percent += Time.deltaTime * animSpeed;
            SetPoints(targetPos, percent);
            yield return null;
        }
        SetPoints(targetPos, 1);
    }

    private void SetPoints(Vector3 targetPos, float percent)
    {
        Vector3 ropeEnd = Vector3.Lerp(transform.position, targetPos, percent);
        float length = Vector2.Distance(transform.position, ropeEnd );

        for(int i = 0; i < resolution; i++)
        {
            float xPos = (float)i / resolution * length;
            float reversePercent = (1 - percent);

            float amplitude = Mathf.Sin(reversePercent * wobbleCount * Mathf.PI);

            float yPos = Mathf.Sin((float) waveCount * i / resolution * 2 * Mathf.PI * reversePercent) * amplitude;

            Vector2 pos = new Vector2(xPos, yPos);
            line.SetPosition(i, pos);
        }
    }
}
