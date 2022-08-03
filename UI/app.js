const routes=[
    {path:'/',component:home},
    {path:'/usuario',component:usuario},
    {path:'/login',component:login},
    {path:'/categoria',component:categoria},
    {path:'/lanche',component:lanche},
    {path:'/pedido',component:pedido}
]

const router = new VueRouter.createRouter({
    history: VueRouter.createWebHashHistory(),
    routes,
})

const app = Vue.createApp({})

app.use(router)

app.mount('#app')