using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueLine : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private int resolution, waveCount, wobbleCount;
    [SerializeField] private float waveSize, animSpeed;

    private LineRenderer line;
    private bool isMakingLine = false;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMakingLine)
            StartCoroutine(AnimateRope(transform.InverseTransformPoint(target.position)));
            
    }

    public void MakeLine(GameObject go)
    {
        target = go.transform;
        isMakingLine = true;
    }

    public void StopLine()
    {
        isMakingLine = false;
        target = gameObject.transform;
        StartCoroutine(AnimateRope(transform.InverseTransformPoint(target.position)));

    }

    private IEnumerator AnimateRope(Vector3 targetPos)
    {
        //Debug.Log(targetPos);
       // Debug.Log(target.name);
        line.positionCount = resolution;
        float angle = LookAtAngle(targetPos - transform.InverseTransformPoint(transform.position));
        float percent = 0;
        while (percent <= 1f)
        {
            percent += Time.deltaTime * animSpeed;
            SetPoints(targetPos, percent, angle);
            yield return null;
        }
        SetPoints(targetPos, 1, angle);
    }

    private void SetPoints(Vector3 targetPos, float percent, float angle)
    {
        Vector3 ropeEnd = Vector3.Lerp(transform.position, targetPos, percent);
        //Debug.Log(Vector3.Lerp(transform.TransformPoint(transform.position), targetPos, 100));
        float length = Vector2.Distance(transform.position, ropeEnd);
        //Debug.Log(transform.TransformPoint(targetPos));
        for (int i = 0; i < resolution; i++)
        {
            float xPos = (float)i / resolution * length;
            float reversePercent = (1 - percent);

            float amplitude = Mathf.Sin(reversePercent * wobbleCount * Mathf.PI);

            float yPos = Mathf.Sin((float)waveCount * i / resolution * 2 * Mathf.PI * reversePercent) * amplitude;

            Vector2 pos = RotatePoint( new Vector2(xPos  , yPos ), new Vector2(0,0), angle);
            //xPos + gameObject.transform.position.x, yPos + gameObject.transform.position.y
            //Debug.Log(pos);
            line.SetPosition(i, pos);
        }
    }

    Vector2 RotatePoint(Vector2 point, Vector2 pivot, float angle)
    {
        Vector2 dir = point - pivot;
        dir = Quaternion.Euler(0, 0, angle ) * dir;
        point = dir + pivot;
        return point;
    }

    private float LookAtAngle(Vector2 target)
    {
        return Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
    }
}
