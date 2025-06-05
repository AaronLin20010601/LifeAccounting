import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue' // 引入首頁
import LoginView from '@/views/LoginView.vue' // 引入登入頁面
import RegisterView from '../views/RegisterView.vue';  // 引入註冊頁面
import ResetView from '../views/ResetView.vue'; // 引入重設密碼頁面
import AccountView from '@/views/account/AccountView.vue'; // 引入帳戶頁面
import TransferBalanceView from '@/views/account/TransferBalanceView.vue'; // 引入餘額轉移頁面
import AddAccountView from '@/views/account/AddAccountView.vue'; // 引入新增帳戶頁面
import EditAccountView from '@/views/account/EditAccountView.vue'; // 引入編輯帳戶頁面
import CategoryView from '@/views/category/CategoryView.vue'; // 引入帳戶頁面
import AddCategoryView from '@/views/category/AddCategoryView.vue'; // 引入新增帳戶頁面
import EditCategoryView from '@/views/category/EditCategoryView.vue'; // 引入編輯帳戶頁面
import RecordView from '@/views/record/RecordView.vue'; // 引入帳戶頁面
import AddRecordView from '@/views/record/AddRecordView.vue'; // 引入新增帳戶頁面
import EditRecordView from '@/views/record/EditRecordView.vue'; // 引入編輯帳戶頁面
import ChartView from '@/views/ChartView.vue'; // 引入統計圖表頁面

// 檢查是否存在 JWT Token
const requireAuth = (to, from, next) => {
    const token = localStorage.getItem('token');
    token ? next() : next('/');
};

// 網頁路徑
const routes = [
    { path: '/', name: 'home', component: HomeView },
    { path: '/login', name: 'login', component: LoginView },
    { path: '/register', name: 'register', component: RegisterView },
    { path: '/reset', name: 'reset', component: ResetView },

    { path: '/account', name: 'account', component: AccountView, beforeEnter: requireAuth },
    { path: '/transfer', name: 'transfer', component: TransferBalanceView, beforeEnter: requireAuth },
    { path: '/addaccount', name: 'addaccount', component: AddAccountView, beforeEnter: requireAuth },
    { path: '/editaccount/:id', name: 'editaccount', component: EditAccountView, beforeEnter: requireAuth },

    { path: '/category', name: 'category', component: CategoryView, beforeEnter: requireAuth },
    { path: '/addcategory', name: 'addcategory', component: AddCategoryView, beforeEnter: requireAuth },
    { path: '/editcategory/:id', name: 'editcategory', component: EditCategoryView, beforeEnter: requireAuth },

    { path: '/record', name: 'record', component: RecordView, beforeEnter: requireAuth },
    { path: '/addrecord', name: 'addrecord', component: AddRecordView, beforeEnter: requireAuth },
    { path: '/editrecord/:id', name: 'editrecord', component: EditRecordView, beforeEnter: requireAuth },

    { path: '/chart', name: 'chart', component: ChartView, beforeEnter: requireAuth },
];

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
})

export default router