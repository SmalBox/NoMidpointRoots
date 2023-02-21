# SmalBoxHollow V1.0.0

## 功能简介(Introduction)

### Instructions in English

   - Shader achieves a cutout effect.
   - Support changing basemaps, hollow styles, color overlays.
   - The cutout style supports zooming (mouse wheel zooming out has been implemented in the sample).
   - Support mouse drag following, target object following, blend mode (implemented in the sample for reference).
   - Shader realizes the preservation of hollow marks (can be used as an eraser or basic fog dispersion function).
   - Retaining the hollow traces supports resolving modifications, increasing or decreasing the accuracy to achieve different effects.

### 中文说明

   - Shader实现镂空效果
   - 支持更换底图、镂空样式、颜色覆盖叠加
   - 镂空样式支持缩放(样例中已实现鼠标滚轮放大缩小)
   - 支持鼠标拖动跟随、目标物体跟随、混合模式(样例中已实现可做参考)
   - Shader实现保留镂空痕迹(可做橡皮擦或基础的迷雾散开功能)
   - 保留镂空痕迹支持分辨修改，提高或降低精度实现不同效果

## 优势(Advantage)

### English

   - Use Shader to achieve the drawing part, the performance is efficient.
   - Exposed parameter control and detailed annotations are easy to use.
   - The implemented resource scripts are concise and easy to embed.
   - The sample implements a complete set of common functions for easy reference.

### 中文

   - 使用Shader实现绘制部分，性能高效。
   - 暴露丰富的参数控制、详细的注释便于使用。
   - 实现的资源脚本等简洁方便嵌入。
   - 样例实现了完整的常用功能，方便参考使用。

## 运行环境(Environment)

   - UnityVersion >= Unity 2020.3.25
   - Demo Run resolution : 1080*1920

## 使用说明(Instructions for use)

### English

   - Implement the function of openwork by using "Res/SmalBoxHollowPlane.prefab".
   - Adjust the parameters in the Inspector of the component MotionMgr to achieve effects such as cutout erase, see the demo in Example.
   - Change the effect by adjusting the material parameters in "Res/SmalBoxHollowPlane.prefab".
      - Overlaycolor Adjust the overlay color.
      - MainTexture adjusts the main display diagram.
      - SubTexture shows the hollow effect according to this map (the transparency of the hollow is determined by the size of the R value in the figure).
      - SubTexture2 eliminates the need for manual assignment and is used to record retention traces.
      - Scale can adjust the size of the cutout effect (example control in Example).
      - Pos can adjust the hollow position (complete mouse and object control example in Example).

### 中文

   - 通过使用"Res/SmalBoxHollowPlane.prefab"实现镂空的功能。
   - 调整组件MotionMgr的Inspector中的参数实现镂空擦除等效果，具体参看Example中的演示。
   - 通过调整"Res/SmalBoxHollowPlane.prefab"中的材质参数改变效果。
      - Overlaycolor 调整覆盖颜色
      - MainTexture 调整主要显示图
      - SubTexture 根据此贴图显示镂空效果(根据图中的R值大小决定镂空的透明度)
      - SubTexture2 无需手动赋值，用来记录保留痕迹
      - Scale 可以调节镂空效果大小(Example中有例子控制)
      - Pos 可以调节镂空位置(Example中完整的鼠标、物体控制例子)

## 样例(Demo)

### English

   - Example1-MouseModel Show mouse drag to control the hollow position.
   - Example2-TargetModel Shows the hollow position to follow the target movement.
   - Example3-MixModel Demonstrate a blend of target and mouse controls.
   - Example4-PreserveTracesModel Exhibit preserved traces of hollow movement (erase effect can be achieved).

### 中文

   - Example1-MouseModel 展示鼠标拖拽控制镂空位置
   - Example2-TargetModel 展示镂空位置跟随目标移动
   - Example3-MixModel 展示目标和鼠标控制的混合模式
   - Example4-PreserveTracesModel 展示保留镂空移动痕迹(可实现擦除效果)

## 联系(Contact)

   - GitHub:[SmalBox][GitHubSmalBox]
   - Website:[SmalBox.top][WebsiteSmalBox]

[GitHubSmalBox]: https://github.com/smalbox
[WebsiteSmalBox]: https://smalbox.top/UnityPlugins/SmalBoxHollow.php

## 许可说明(License)

   - Apache License Version 2.0
   - For details, see the license file in the project.