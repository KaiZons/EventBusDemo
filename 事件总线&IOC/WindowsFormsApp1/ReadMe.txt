View依赖于发布方；

发布方依赖于EventSourceObject、EventBus 和 IOC

监听方依赖于EventSourceObject、EventBus 和 IOC

EventSourceObject依赖于EventBus

程序入口需要引用监听方，方便反射并初始化EventBus

====>  发布方于监听方解耦 IOC？EventBus？