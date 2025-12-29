using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using UnityEngine;

// [UpdateInGroup()]
/// <summary>
/// 旋转立方体系统，负责处理带有旋转功能的游戏对象的更新逻辑
/// </summary>
public partial struct RotatingCubeSystem : ISystem
{
    /// <summary>
    /// 系统创建时的初始化方法
    /// </summary>
    /// <param name="ref SystemState state">系统状态引用</param>
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
        
        // state.EntityManager.SetComponentData(state.SystemHandle
        // NativeArray<int> nativeArray = new NativeArray<int>(5, Allocator.Temp);
    }
    
    /// <summary>
    /// 系统更新方法，处理旋转逻辑的执行
    /// </summary>
    /// <param name="ref SystemState state">系统状态引用</param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        return;
        
        // NativeArray<int> nativeArray = new NativeArray<int>(5, Allocator.Persistent);
        // nativeArray.Dispose();
        
        // 被注释的查询方式：通过SystemAPI查询带有LocalTransform和RotateSpeed组件的对象
        // foreach ((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed)
        // // foreach (var (localTransform, rotateSpeed)
        //          // in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithNone<Player>())
        //          in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithAll<RotatingCube>())
        // {
        //     float power = 1f;
        //     for (int i = 0; i < 100000; i++)
        //     {
        //         power *= 2f;
        //         power /= 2f;
        //     }
        //     
        //     localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.Value * SystemAPI.Time.DeltaTime * power);
        // }
        
        // 创建旋转立方体作业实例
        RotatingCubeJob rotatingCubeJob = new RotatingCubeJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        
        // 被注释的作业执行方式说明：
        // // 立即在主线程运行作业
        // rotatingCubeJob.Run();
        // // 计划在工作线程上运行作业
        // rotatingCubeJob.Schedule();
        // // 并行调度 将作业分配给多个线程以提高性能
        // rotatingCubeJob.ScheduleParallel();
        
        // // 强制完成作业 在继续之前等待作业完成
        // rotatingCubeJob.Schedule(state.Dependency).Complete();
        
        // // 当安排一个作业时，将具有正确的依赖关系 在执行ijob实体以外的任何操作，确保显示定义依赖项为该作业的依赖项
        // state.Dependency = rotatingCubeJob.Schedule(state.Dependency);
        
        rotatingCubeJob.ScheduleParallel();
    }

    /// <summary>
    /// 旋转立方体作业，用于在多线程环境中执行旋转逻辑
    /// </summary>
    [BurstCompile]
    // [WithNone(typeof(Player))]
    [WithAll(typeof(RotatingCube))]
    public partial struct RotatingCubeJob : IJobEntity
    {
        /// <summary>
        /// 只读的整数原生数组
        /// </summary>
        [ReadOnly] public NativeArray<int> nativeArray;
        
        /// <summary>
        /// 时间增量，用于计算旋转角度
        /// </summary>
        public float deltaTime;
        
        /// <summary>
        /// 执行旋转逻辑的方法
        /// </summary>
        /// <param name="ref LocalTransform localTransform">本地变换组件的引用</param>
        /// <param name="in RotateSpeed rotateSpeed">旋转速度组件的只读引用</param>
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            float power = 1f;
            for (int i = 0; i < 100000; i++)
            {
                power *= 2f;
                power /= 2f;
            }
            
            localTransform = localTransform.RotateY(rotateSpeed.Value * deltaTime * power);
        }
    }
}