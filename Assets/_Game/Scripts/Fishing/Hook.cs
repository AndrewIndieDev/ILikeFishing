using UnityEngine;

public class Hook : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject prefabSplash;
    public Transform fishParent;
    public FishingRod rod;

    private bool isMoving;
    private Vector2 movementDirection;
    private bool submerged = false;
    private float maxYVelocity = -5f;
    private float submergedVelocity = -1f;
    private bool reelIn = false;
    private float xDampening = 3f;

    public void Init(float force, int dir, FishingRod rod)
    {
        this.rod = rod;
        submerged = false;
        transform.localPosition = Vector2.zero;
        if (dir == 0) //Left
        {
            rb.velocity = (Vector3.up + Vector3.left).normalized * force;
        }
        else if (dir == 1) //Right
        {
            rb.velocity = (Vector3.up + Vector3.right).normalized * force;
        }
    }

    void Update()
    {
        if (rb.velocity.y < maxYVelocity && !submerged)
            rb.velocity = new Vector2(rb.velocity.x, maxYVelocity);
        else if (rb.velocity.y < submergedVelocity && submerged)
            rb.velocity = new Vector2(rb.velocity.x, submergedVelocity);
        else if (rb.velocity.y > -submergedVelocity && submerged)
            rb.velocity = new Vector2(rb.velocity.x, -submergedVelocity);

        if (!submerged)
            return;

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (reelIn)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxYVelocity * (movementDirection.y == 0 ? 1f : movementDirection.y > 0 ? 1.5f : 0.5f));
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, submergedVelocity * (movementDirection.y == 0 ? 1f : movementDirection.y > 0 ? 0.5f : 1.5f));
        }
    }

    private void FixedUpdate()
    {
        isMoving = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        if (isMoving)
        {
            rb.velocity = new Vector2(movementDirection.x, rb.velocity.y);
        }
        else
        {
            rb.AddForce(new Vector2(-rb.velocity.x * (reelIn ? xDampening : 0.5f), 0f));
        }

        if (reelIn)
        {
            rb.AddForce(new Vector2((fishParent.position.x - transform.position.x) * xDampening, 0f));
        }
    }

    public void Submerge()
    {
        if (!submerged)
            Instantiate(prefabSplash, transform.position, Quaternion.identity);

        submerged = !reelIn;
        if (reelIn)
        {
            Instantiate(prefabSplash, transform.position, Quaternion.identity);
            KillHook();
        }
        rb.velocity = new Vector2(rb.velocity.x * 0.3f, submergedVelocity);
    }

    public void Surface()
    {
        reelIn = true;
    }

    public void KillHook()
    {
        rod.ReeledIn();
        Destroy(gameObject);
    }
}
