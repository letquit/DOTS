using Unity.Entities;
using UnityEngine;

/// <summary>
/// 旋转立方体的作者化组件，用于在Unity编辑器中配置旋转立方体实体的初始数据
/// </summary>
public class RotatingCubeAuthoring : MonoBehaviour
{
    /// <summary>
    /// 旋转立方体的烘焙器，负责将Authoring组件转换为运行时的ECS实体
    /// </summary>
    public class Baker : Baker<RotatingCubeAuthoring>
    {
        /// <summary>
        /// 将Authoring组件烘焙为ECS实体
        /// </summary>
        /// <param name="authoring">旋转立方体的Authoring组件实例</param>
        public override void Bake(RotatingCubeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new RotatingCube());
        }
    }
}

/// <summary>
/// 旋转立方体的组件数据，用于标识实体为旋转立方体
/// </summary>
public struct RotatingCube : IComponentData
{
}