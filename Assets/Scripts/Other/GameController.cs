using Cinemachine;
using System;
using Unity.Multiplayer.PlayMode;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private GameObject player;
    public static Action<GameObject> OnPlayerSpawned;
    private void Awake()
    {
        player = Instantiate(playerPrefab);
        virtualCamera.Follow = player.transform;
    }

    private void Start()
    {
        OnPlayerSpawned?.Invoke(player);
    }
}
