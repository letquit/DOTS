using Unity.Entities;
using UnityEngine;

/// <summary>
/// 作者化组件，用于在编辑器中定义眩晕状态的实体
/// </summary>
public class StunnedAuthoring : MonoBehaviour
{
    /// <summary>
    /// 将StunnedAuthoring转换为ECS实体的烘焙器
    /// </summary>
    public class Baker : Baker<StunnedAuthoring>
    {
        /// <summary>
        /// 将作者化组件烘焙为ECS实体和组件
        /// </summary>
        /// <param name="authoring">StunnedAuthoring作者化组件实例</param>
        public override void Bake(StunnedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Stunned());
            SetComponentEnabled<Stunned>(entity, false);
        }
    }
}

/// <summary>
/// 表示实体处于眩晕状态的组件数据
/// </summary>
public struct Stunned : IComponentData, IEnableableComponent
{
    
}