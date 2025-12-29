using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

/// <summary>
/// 玩家射击管理器，负责处理玩家射击相关的UI弹窗显示
/// </summary>
public class PlayerShootManager : MonoBehaviour
{
    public static PlayerShootManager Instance { get; private set; }
    
    [SerializeField] private GameObject shootPopupPrefab;

    private void Awake()
    {
        Instance = this;
    }

    // private void Start()
    // {
    //     PlayerShootingSystem playerShootingSystem =
    //         World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PlayerShootingSystem>();
    //     
    //     playerShootingSystem.OnShoot += PlayerShootingSystem_OnShoot;
    // }
    //
    // private void PlayerShootingSystem_OnShoot(object sender, EventArgs e)
    // {
    //     Entity playerEntity = (Entity)sender;
    //     LocalTransform localTransform =
    //         World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalTransform>(playerEntity);
    //     Instantiate(shootPopupPrefab, localTransform.Position, Quaternion.identity);
    // }

    /// <summary>
    /// 玩家射击时在指定位置实例化射击弹窗
    /// </summary>
    /// <param name="playerPosition">玩家当前位置，用于确定弹窗显示位置</param>
    public void PlayerShoot(Vector3 playerPosition)
    {
        Instantiate(shootPopupPrefab, playerPosition, Quaternion.identity);
    }
}