using Unity.Entities;
using UnityEngine;

/// <summary>
/// 负责配置立方体生成参数的Authoring组件，用于在编辑器中设置生成配置并在烘焙时转换为ECS组件
/// </summary>
public class SpawnCubesConfigAuthoring : MonoBehaviour
{
    /// <summary>
    /// 用于生成的立方体预制体对象
    /// </summary>
    public GameObject cubePrefab;
    
    /// <summary>
    /// 要生成的立方体数量
    /// </summary>
    public int amountToSpawn;

    /// <summary>
    /// 将SpawnCubesConfigAuthoring转换为ECS实体和组件的烘焙器
    /// </summary>
    public class Baker : Baker<SpawnCubesConfigAuthoring>
    {
        /// <summary>
        /// 将Authoring组件的数据烘焙为ECS实体和组件
        /// </summary>
        /// <param name="authoring">SpawnCubesConfigAuthoring组件实例，包含预制体和生成数量配置</param>
        public override void Bake(SpawnCubesConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            
            // 将Authoring组件的数据转换为SpawnCubesConfig组件并添加到实体
            AddComponent(entity, new SpawnCubesConfig
            {
                cubePrefabEntity = GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                amountToSpawn = authoring.amountToSpawn
            });
        }
    }
}

/// <summary>
/// 存储立方体生成配置的ECS组件数据结构
/// </summary>
public struct SpawnCubesConfig : IComponentData
{
    /// <summary>
    /// 立方体预制体对应的实体引用
    /// </summary>
    public Entity cubePrefabEntity;
    
    /// <summary>
    /// 需要生成的立方体数量
    /// </summary>
    public int amountToSpawn;
}