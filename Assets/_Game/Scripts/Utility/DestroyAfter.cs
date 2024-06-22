using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float seconds;

    // Update is called once per frame
    void Update()
    {
        seconds -= Time.deltaTime;
        if (seconds < 0)
            Destroy(gameObject);
    }
}
