using Unity.Entities;
using UnityEngine;

/// <summary>
/// 用于在编辑器中定义旋转速度值的Authoring组件，将GameObject上的旋转速度数据转换为ECS实体组件
/// </summary>
public class RotateSpeedAuthoring : MonoBehaviour
{
    /// <summary>
    /// 旋转速度值
    /// </summary>
    public float value;

    /// <summary>
    /// 将Authoring组件转换为ECS实体组件的Baker类
    /// </summary>
    private class Baker : Baker<RotateSpeedAuthoring>
    {
        /// <summary>
        /// 将Authoring组件数据烘焙为ECS实体组件
        /// </summary>
        /// <param name="authoring">RotateSpeedAuthoring组件实例</param>
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            // 创建实体并添加旋转速度组件
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotateSpeed
            {
                Value = authoring.value
            });
        }
    }
}

/// <summary>
/// 表示实体旋转速度的ECS组件数据
/// </summary>
public struct RotateSpeed : IComponentData
{
    /// <summary>
    /// 旋转速度值
    /// </summary>
    public float Value;
}