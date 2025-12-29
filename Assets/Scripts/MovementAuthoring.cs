using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 运动组件的授权类，用于在编辑器中配置运动相关参数，并在烘焙时转换为ECS组件
/// </summary>
public class MovementAuthoring : MonoBehaviour
{
    /// <summary>
    /// 负责将MovementAuthoring转换为ECS实体组件的烘焙器
    /// </summary>
    public class Baker : Baker<MovementAuthoring>
    {
        /// <summary>
        /// 将授权组件烘焙为ECS实体和组件
        /// </summary>
        /// <param name="authoring">MovementAuthoring组件实例，包含运动配置数据</param>
        public override void Bake(MovementAuthoring authoring)
        {
            // 创建具有动态变换使用标志的实体
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            // 为实体添加Movement组件，初始化随机运动向量
            AddComponent(entity, new Movement
            {
                movementVector = new float3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f))
            });
        }
    }
}

/// <summary>
/// 表示实体运动状态的ECS组件数据结构
/// </summary>
public struct Movement : IComponentData
{
    /// <summary>
    /// 运动向量，定义实体在三维空间中的运动方向和速度
    /// </summary>
    public float3 movementVector;
}