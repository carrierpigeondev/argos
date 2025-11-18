using UnityEngine;
using UnityEngine.UI;

public class ToggleNearestScreen : MonoBehaviour
{
    public float DetectionRadius;
    public RawImage RawImg;

    void Start()
    {
        
    }

    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(gameObject.transform.position, DetectionRadius);
        float lowestDistance = Mathf.Infinity;
        foreach (Collider c in cols)
        {
            float thisDistance = Vector3.Distance(c.transform.position, gameObject.transform.position);
            if (thisDistance < lowestDistance)
            {
                lowestDistance = thisDistance;
                if (c.TryGetComponent<RawImage>(out var rawimg))
                {
                    RawImg = rawimg;
                }
            }
        }

        Debug.Log(RawImg.gameObject.name);
    }
}
