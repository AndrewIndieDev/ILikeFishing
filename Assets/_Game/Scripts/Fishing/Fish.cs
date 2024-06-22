using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 1f;
    public bool flipped = false;
    public Transform visualParent;

    private bool caught = false;
    private Vector2 startingPos;
    private SpriteRenderer meshRenderer;

    private void Start()
    {
        startingPos = transform.position;
        Init(flipped);
    }

    public void Init(bool flip = false)
    {
        flipped = flip;
        transform.localScale = new Vector3(flip ? -1f : 1f, 1f, 1f);
        RandomVisual();
        speed = Random.Range(speed - 0.2f, speed + 0.2f);
    }

    private void Update()
    {
        if (!caught)
            transform.position += new Vector3(transform.right.x * Time.deltaTime * speed * (flipped ? -1f : 1f), 0f, 0f);
    }

    private void RandomVisual()
    {
        for (int i = 0; i < visualParent.childCount; i++)
        {
            visualParent.GetChild(i).gameObject.SetActive(false);
        }
        GameObject visual = visualParent.GetChild(Random.Range(0, visualParent.childCount)).gameObject;
        visual.SetActive(true);
        meshRenderer = visual.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hook hook = collision.gameObject.GetComponent<Hook>();
        if (hook != null)
        {
            caught = true;
            transform.SetParent(hook.fishParent);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.Rotate(Vector3.forward, Random.Range(45f, 135f));
            hook.rod.ReelIn();
            meshRenderer.color = Color.white;
        }

        if (collision.gameObject.tag == "Fish Bounds")
        {
            transform.position = startingPos;
        }
    }
}
