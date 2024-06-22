using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class Movement : MonoBehaviour
{
    [Header("References")]
    public Transform boatLeftEdge;
    public Transform boatRightEdge;
    public SplineContainer splineContainer;

    [Header("Veriables")]
    public float speed = 1f;

    private float horizontalInput;
    private bool canMove = true;
    private Spline currentSpline;

    private void Start()
    {
        currentSpline = splineContainer.Splines[0];
    }

    private void Update()
    {
        if (!canMove)
            return;

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(horizontalInput * Time.deltaTime * speed, 0f, 0f);
            transform.localScale = new Vector3(horizontalInput < 0 ? -0.5f : 0.5f, 0.5f, 1);
        }

        var native = new NativeSpline(currentSpline);
        float distance = SplineUtility.GetNearestPoint(native, transform.position, out float3 nearest, out float t);

        Vector3 pos = nearest;
        transform.position = pos;

        //if (Input.GetKey(KeyCode.A) && transform.position.x > boatLeftEdge.position.x)
        //{
        //    transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        //    transform.position = new Vector3(transform.position.x + horizontalInput * Time.deltaTime, transform.position.y, transform.position.z);
        //}
        //if (Input.GetKey(KeyCode.D) && transform.position.x < boatRightEdge.position.x)
        //{
        //    transform.localScale = new Vector3(0.5f, 0.5f, 1);
        //    transform.position = new Vector3(transform.position.x + horizontalInput * Time.deltaTime, transform.position.y, transform.position.z);
        //}
    }

    public void CastLine()
    {
        canMove = false;
    }

    public void FinishedFishing()
    {
        canMove = true;
    }
}
