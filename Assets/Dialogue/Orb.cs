using System.Collections;
using UnityEngine;
using Yarn;
using Yarn.Unity;
using Yarn.Unity.Attributes;

public class Orb : MonoBehaviour {
    public YarnProject YarnProject;
    
    [YarnNode("YarnProject", true)]
    public string Node;

    [YarnCommand("grow")]
    public IEnumerator Grow(float target, float time) {
        // assuming this object is always scaled the same
        float initial = transform.localScale.x;
        
        float elapsed = 0f;
        while (elapsed < time)
        {
            // find how much percentage of the time has elapsed (0 -> 1)
            float elapsedPct = elapsed / time;

            // animate whatever you want using elapsedPct
            // in this case move from initalScale to targetScale
            float currScale = Mathf.Lerp(initial, target, elapsedPct);
            
            // set the property we want to change
            transform.localScale = currScale * Vector3.one;
            
            // update the elapsed time
            elapsed += Time.deltaTime;

            // wait til next frame
            yield return null;
        }
        
        transform.localScale = target * Vector3.one;
    }

    [YarnCommand("change_light")]
    public void ChangeLight(GameObject target)
    {
        target.transform.position = transform.position + Random.onUnitSphere * 2f;
    }
}
