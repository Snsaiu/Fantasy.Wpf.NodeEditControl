# Fantasy.Wpf.NodeEditControl
## 概述

Fantasy.Wpf.NodeEditControl 是一款基于**WPF框架**的node节点编辑器控件，基于.net7开发。
该控件具备**高扩展性**和**高定制化**的要求进行开发！

## 控件组成

该控件由**Node组件**，**Port组件**，**Line组件**和**Canvas**组成。

### Node(节点)
Node组件的默认样式如下：

![节点](./readmeAssets/imgs/nodesolo.png)

节点具备基本的输入和输出以及节点本身的属性调节。默认情况下，左边的端口是输入，右边的端口是输出，点击输出端口并拖拽会出现一根连接线，如图所示：

![拖拽创建线连接](./readmeAssets/imgs/willConnect.gif)

节点具备一些属性，例如冷冻属性，查看结果，设置属性，查看信息，如图所示：

![节点属性](./readmeAssets/imgs/nodeattribute.png)

1.冷冻属性

该属性是一个bool值，默认是false（即默认不冻结）,冷冻属性是指当前节点不进行计算（如果一次计算都没有过，那么会进行一次计算）。

![未选择冻结属性](./readmeAssets/imgs/noFv.gif)

当未勾选FV时候，当上游节点发生更改时候，会自动计算下游节点。

![勾选冻结属性](./readmeAssets/imgs/fv.gif)

当勾选FV时候，被勾选的节点在上游节点更改的时候不会进行计算，包括被冻结的下游节点。

2.重新计算

默认情况下当上游的节点发生更改的时候，会自动计算下游节点。若节点FV（冻结属性）的值由true变成false，需要手动点击**重新计算**节点进行更新节点

![重新计算](./readmeAssets/imgs/recalculate.gif)

3.查看结果

查看结果会打开一个窗口，该窗口用于展现节点最终的计算结果。如图所示:

![查看结果](./readmeAssets/imgs/showResult.gif)

查看结果会根据上游的更改自动更新结果窗口。

4.设置属性

设置属性会打开该节点的属性面板，属性面板用于调节该节点的属性，当节点属性发生变化时候，会自动更新下游节点。

![设置属性](./readmeAssets/imgs/settpanel.gif)

5.查看节点说明

查看节点说明会打开节点说明面板，该面板会详细解释该节点的用法。

![查看节点说明](./readmeAssets/imgs/helpnode.gif)

节点删除

默认删除节点的操作是鼠标右击节点，弹出菜单栏中选择‘删除’按钮。

![节点删除]()
