using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public Hook prefabHook;
    public Transform castPoint;
    public float maxCastPower = 5f;
    public float castPullBackSpeed = 1f;
    public LineRenderer lr;
    public Movement movement;
    public Transform fishingRodParent;
    public Transform boatTransform;

    private float pullBackAngle = 130f;
    private float currentPower;
    private Hook currentHook;
    private bool reelingIn = false;
    private float reelInCooldown;
    private float maxReelInCooldown = 2f;

    private void Update()
    {
        if (reelInCooldown > 0f)
            reelInCooldown -= Time.deltaTime;
        if (currentHook != null)
            lr.SetPosition(1, currentHook.transform.position);
        if (Input.GetKeyDown(KeyCode.Space))
            ReelIn();

        HandleRodCast();
    }

    private void HandleRodCast()
    {
        if (!reelingIn && currentHook == null)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                currentPower = Mathf.Clamp(currentPower + Time.deltaTime * castPullBackSpeed, 0f, maxCastPower);
                float normalisedPower = currentPower / maxCastPower;
                fishingRodParent.localRotation = Quaternion.Euler(new Vector3(0f, 0f, pullBackAngle * normalisedPower));
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                fishingRodParent.localRotation = Quaternion.Euler(Vector3.zero);
                currentHook = Instantiate(prefabHook, castPoint.position, Quaternion.identity);
                currentHook.transform.SetParent(lr.transform, true);
                currentHook.Init(currentPower, transform.localScale.x < 0 ? 0 : 1, this);
                CameraSwitcher.Instance.SwitchFollow(currentHook.transform);

                lr.SetPosition(0, castPoint.position);
                lr.enabled = true;

                currentPower = 0f;
                castPoint.gameObject.SetActive(false);
                movement.CastLine();

                reelInCooldown = maxReelInCooldown;
            }
        }
    }

    public void ReelIn()
    {
        if (currentHook != null && !reelingIn && reelInCooldown <= 0f)
        {
            reelingIn = true;
            currentHook.Surface();
        }
    }

    public void ReeledIn()
    {
        reelingIn = false;
        lr.enabled = false;
        currentHook = null;
        castPoint.gameObject.SetActive(true);
        movement.FinishedFishing();
        CameraSwitcher.Instance.SwitchFollow(boatTransform);
    }
}
