using Unity.VisualScripting;
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
        // WONT WORK: THE SCREEN DOES NOT HAVE A COLLIDER

        Collider[] cols = Physics.OverlapSphere(gameObject.transform.position, DetectionRadius);
        float lowestDistance = Mathf.Infinity;
        foreach (Collider c in cols)
        {
            float thisDistance = Vector3.Distance(c.transform.position, gameObject.transform.position);
            RawImage rawImg = c.GetComponentInChildren<RawImage>();
            Debug.Log($"{c.gameObject.name}: {thisDistance}: {thisDistance < lowestDistance}: {rawImg}");
            if (thisDistance < lowestDistance && rawImg)
            {
                RawImg = rawImg;
            }
        }
    }
}
