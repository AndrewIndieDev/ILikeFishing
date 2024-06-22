using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void SwitchFollow(Transform follow)
    {
        virtualCamera.Follow = follow;
    }
}
