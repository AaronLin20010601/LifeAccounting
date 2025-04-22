import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue' // 引入登入頁面
import RegisterView from '../views/RegisterView.vue';  // 引入註冊頁面
import ResetView from '../views/ResetView.vue'; // 引入重設密碼頁面

// 檢查是否存在 JWT Token
const requireAuth = (to, from, next) => {
    const token = localStorage.getItem('token');
    token ? next() : next('/');
};

// 網頁路徑
const routes = [
    { path: '/', name: 'login', component: LoginView },
    { path: '/register', name: 'register', component: RegisterView },
    { path: '/reset', name: 'reset', component: ResetView },
];

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
})

export default router