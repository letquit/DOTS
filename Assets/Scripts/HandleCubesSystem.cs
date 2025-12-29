using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

/// <summary>
/// 处理立方体系统的系统实现，负责更新旋转和移动立方体的位置和旋转
/// </summary>
public partial struct HandleCubesSystem : ISystem
{
    /// <summary>
    /// 系统更新方法，在每一帧调用以处理立方体的移动和旋转逻辑
    /// </summary>
    /// <param name="state">系统状态引用，提供对ECS系统的访问</param>
    public void OnUpdate(ref SystemState state)
    {
        // 遍历所有具有RotatingMovingCubeAspect的实体，执行移动和旋转操作
        foreach (RotatingMovingCubeAspect rotatingMovingCubeAspect in
                 SystemAPI.Query<RotatingMovingCubeAspect>())
                 // SystemAPI.Query<RotatingMovingCubeAspect>().WithAll<RotatingCube>())
        {
            // 根据时间增量更新立方体的位置和旋转
            rotatingMovingCubeAspect.MoveAndRotate(SystemAPI.Time.DeltaTime);
        }
    }
}