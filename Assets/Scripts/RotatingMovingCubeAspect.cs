using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

/// <summary>
/// 旋转移动立方体的切面组件，封装了具有旋转和移动功能的实体的相关组件访问
/// </summary>
public readonly partial struct RotatingMovingCubeAspect : IAspect
{
    public readonly Entity entity;
    public readonly RefRO<RotatingCube> rotatingCube;
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<RotateSpeed> rotateSpeed;
    public readonly RefRO<Movement> movement;

    /// <summary>
    /// 执行立方体的移动和旋转操作
    /// </summary>
    /// <param name="deltaTime">自上一帧以来的时间间隔</param>
    public void MoveAndRotate(float deltaTime)
    {
        // 绕Y轴旋转立方体
        localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.Value * deltaTime);
        // 沿指定方向移动立方体
        localTransform.ValueRW =
            localTransform.ValueRO.Translate(movement.ValueRO.movementVector * deltaTime);
    }
}