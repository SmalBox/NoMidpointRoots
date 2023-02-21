# SmalBoxCamEdgeFollow V1.0.0

## 功能简介(Introduction)

### Instructions in English

   - The target moves the camera when it touches the set edge, keeping the target always in the set screen area.

### 中文说明

   - 目标在接触设定边缘时会让相机跟随移动，保持目标始终在设定的屏幕区域内。

## 优势(Advantage)

### English

   - Exposed parameter control and detailed annotations are easy to use.
   - The implemented resource scripts are concise and easy to embed.
   - The sample implements a complete set of common functions for easy reference.

### 中文

   - 暴露丰富的参数控制、详细的注释便于使用。
   - 实现的资源脚本等简洁方便嵌入。
   - 样例实现了完整的常用功能，方便参考使用。

## 运行环境(Environment)

   - UnityVersion >= Unity 2020.3.25
   - Demo Run resolution : 1080*1920

## 使用说明(Instructions for use)

### English

   - Implement the function of target edge following movement by using "Res/SmalBoxCamEdgeFollow.prefab".
   - Adjust the parameters in the Inspector of the CamEdgeFollowMagr component to adjust the range of free motion, see the demo in Example.
   - Parameter description in CamEdgeFollowMgr:
      - DebugLog : Whether debugging information is turned on
      - MoveCam : Set the camera you want to control, if you don't set it, you will automatically get MainCamera
      - TargetTransform : Set the object to follow
      - Range setting method (default method 1, method 2 will be used when the setting method 2 changes):
         - Way 1 - Set the axis range to adjust the free activity area: Set TargetRangeOffsetX and TargetRangeOffsetY, which are the numbers of the positive and negative axes of the two axes of XY based on the center point of the screen, see Use Case Reference 1 for details
         - Way 2 - Set the screen edge inward offset to adjust the free active area: Set TargetScreenEdgeOffsetLeftRight and TargetScreenEdgeOffsetUpDown, with the four sides of the screen as the benchmark, the inward offset, see use case reference 2 for details
         - OpenReferenceArea: Whether to turn on the range reference map

### 中文

   - 通过使用"Res/SmalBoxCamEdgeFollow.prefab"实现目标边缘跟随移动的功能。
   - 调整CamEdgeFollowMgr组件的Inspector中的参数调整自由活动范围，具体参看Example中的演示。
   - CamEdgeFollowMgr中的参数说明：
      - DebugLog : 是否开启调试信息
      - MoveCam : 设置要控制的相机，不设置则会自动获取MainCamera
      - TargetTransform : 设置要跟随的对象
      - 范围设置方式(默认方式1,当设置的方式2的变时会使用方式2)：
         - 方式1-设置轴范围调整自由活动区域：设置TargetRangeOffsetX和TargetRangeOffsetY,为以屏幕中心点为基准，XY两个轴的正负轴的数字，具体参看用例参考1
         - 方式2-设置屏幕边缘向内偏移量调整自由活动区域：设置TargetScreenEdgeOffsetLeftRight和TargetScreenEdgeOffsetUpDown,以屏幕四个边为基准，向内偏移量，具体参看用例参考2
      - OpenReferenceArea : 是否开启范围参考图

## 样例(Demo)

### English

   - Example1-SetAxisRangeCameraMove Display axis range settings
   - Example2-SetEdgeRangeCameraMove Showcase edge range settings

### 中文

   - Example1-SetAxisRangeCameraMove 展示轴范围设置
   - Example2-SetEdgeRangeCameraMove 展示边缘范围设置

## 联系(Contact)

   - GitHub:[SmalBox][GitHubSmalBox]
   - Website:[SmalBox.top][WebsiteSmalBox]

[GitHubSmalBox]: https://github.com/smalbox
[WebsiteSmalBox]: https://smalbox.top/UnityPlugins/SmalBoxCamEdgeFollow.php

## 许可说明(License)

   - Apache License Version 2.0
   - For details, see the license file in the project.