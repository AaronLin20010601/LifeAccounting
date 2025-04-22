<!-- 網頁導覽列 -->
<template>
    <nav class="bg-white border-b border-gray-200 px-6 py-4 shadow-sm">
        <div class="container mx-auto flex justify-between items-center">
            <h1 class="text-2xl font-semibold text-gray-800 flex items-center space-x-2">
                🔖Life Accounting
            </h1>
            <ul class="flex space-x-4 text-gray-700 font-medium">
                <!-- 登入 -->
                <li v-if="!isLoggedIn">
                    <RouterLink to="/" class="hover:text-indigo-600 transition">Login</RouterLink>
                </li>
                <!-- 註冊 -->
                <li v-if="!isLoggedIn">
                    <RouterLink to="/register" class="hover:text-indigo-600 transition">Register</RouterLink>
                </li>
                <!-- 重設密碼 -->
                <li v-if="!isLoggedIn">
                    <RouterLink to="/reset" class="hover:text-indigo-600 transition">Reset Password</RouterLink>
                </li>
                <!-- 登出 -->
                <li v-if="isLoggedIn">
                    <button @click="logout" class="hover:text-red-500 transition">Logout</button>
                </li>
            </ul>
        </div>
    </nav>
</template>

<script>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'
export default {
    setup() {
        const router = useRouter()
        const store = useStore()
        // 讀取 token，判斷是否已登入
        const isLoggedIn = computed(() => store.getters.isLoggedIn)

        // 登出功能
        const logout = () => {
            // 清除 token
            store.dispatch('logout')
            // 清除 localStorage
            localStorage.removeItem('token');
            localStorage.removeItem('user');
            // 重定向到登入頁
            router.push('/')
        }

        return {
            isLoggedIn,
            logout,
        }
    },
}
</script>