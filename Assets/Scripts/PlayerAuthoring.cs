using Unity.Entities;
using UnityEngine;

/// <summary>
/// PlayerAuthoring类用于在Unity编辑器中定义玩家组件的作者化数据，作为ECS系统中Player组件的数据源
/// </summary>
public class PlayerAuthoring : MonoBehaviour
{
    /// <summary>
    /// Baker类负责将PlayerAuthoring组件转换为ECS实体和组件
    /// </summary>
    public class Baker : Baker<PlayerAuthoring>
    {
        /// <summary>
        /// 将PlayerAuthoring组件烘焙为ECS实体和Player组件
        /// </summary>
        /// <param name="authoring">PlayerAuthoring类型的作者化组件实例</param>
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new Player());
        } 
    }
}

/// <summary>
/// Player结构体定义了玩家实体的组件数据，实现IComponentData接口以作为ECS组件使用
/// </summary>
public struct Player : IComponentData
{
    
}