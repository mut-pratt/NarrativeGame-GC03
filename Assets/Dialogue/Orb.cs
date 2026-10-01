using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class Orb : MonoBehaviour
{
    [YarnCommand("grow")]
    public IEnumerator Grow(float target, float time) {
        Vector3 targetScale = target * Vector3.one;
        
        float elapsed = 0f;
        while (elapsed < time)
        {
            float elapsedPct = elapsed / time;

            // animate whatever you want using elapsedPct
            Vector3 currScale = Mathf.Lerp(1f, target, elapsedPct) * Vector3.one;
            transform.localScale = elapsedPct * targetScale;
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = targetScale;
    }

    [YarnCommand("change_light")]
    public void ChangeLight(GameObject target)
    {
        target.transform.position = transform.position + Random.onUnitSphere * 2f;
    }
}
