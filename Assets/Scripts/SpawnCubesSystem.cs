using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 负责生成立方体实体的系统
/// </summary>
public partial class SpawnCubesSystem : SystemBase
{
    /// <summary>
    /// 系统创建时的初始化方法
    /// </summary>
    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubesConfig>();
    }
    
    /// <summary>
    /// 系统更新方法，负责实例化立方体并设置其位置
    /// </summary>
    protected override void OnUpdate()
    {
        this.Enabled = false;

        SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();

        for (int i = 0; i < spawnCubesConfig.amountToSpawn; i++)
        {
            Entity spawnedEntity = EntityManager.Instantiate(spawnCubesConfig.cubePrefabEntity);
            // SystemAPI.SetComponent(spawnedEntity, new LocalTransform
            EntityManager.SetComponentData(spawnedEntity, new LocalTransform
            {
                Position = new float3(Random.Range(-10f, 5f), 0.6f, Random.Range(-4f, 7f)),
                Rotation = quaternion.identity,
                Scale = 1f
            });
            // LocalTransform.FromPosition(new float3(Random.Range(-10f, 5f), 0.6f, Random.Range(-4f, 7f)));
        }
    }
}