using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

/// <summary>
/// 玩家射击系统，负责处理玩家射击逻辑和生成射击物体
/// </summary>
public partial class PlayerShootingSystem : SystemBase
{
    /// <summary>
    /// 射击事件，当玩家射击时触发
    /// </summary>
    public event EventHandler OnShoot;
    
    /// <summary>
    /// 系统创建时的初始化方法
    /// </summary>
    protected override void OnCreate()
    {
        RequireForUpdate<Player>();
    }
    
    /// <summary>
    /// 系统更新方法，处理玩家输入和射击逻辑
    /// </summary>
    protected override void OnUpdate()
    {
        // 检测T键按下，使玩家进入眩晕状态
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, true);
        }
        
        // 检测Y键按下，使玩家退出眩晕状态
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, false);
        }
        
        // 如果空格键未按下则直接返回
        if (!Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            return;
        }
        
        // 获取生成方块的配置信息
        SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();

        // 创建实体命令缓冲区用于批量操作实体
        EntityCommandBuffer entityCommandBuffer = new  EntityCommandBuffer(WorldUpdateAllocator);

        // 遍历所有具有Player组件且未被眩晕的实体
        foreach ((RefRO<LocalTransform> localTransform, Entity entity) in 
                 SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>().WithDisabled<Stunned>().WithEntityAccess())
        {
            // 实例化方块预制体
            Entity spawnedEntity = entityCommandBuffer.Instantiate(spawnCubesConfig.cubePrefabEntity);
            entityCommandBuffer.SetComponent(spawnedEntity, new LocalTransform
            {
                Position = localTransform.ValueRO.Position,
                Rotation = quaternion.identity,
                Scale = 1f
            });
            
            // 触发射击事件
            OnShoot?.Invoke(entity, EventArgs.Empty);
            
            // 调用玩家射击管理器处理射击逻辑
            PlayerShootManager.Instance.PlayerShoot(localTransform.ValueRO.Position);
        }
        
        // 执行实体命令缓冲区中的操作
        entityCommandBuffer.Playback(EntityManager);
    }
}