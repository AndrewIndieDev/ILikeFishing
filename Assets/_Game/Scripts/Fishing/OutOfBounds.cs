using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Hook>() != null)
        {
            collision.gameObject.GetComponent<Hook>().KillHook();
        }
    }
}
