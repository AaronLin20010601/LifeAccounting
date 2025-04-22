<template>
    <!-- 登入表單 -->
    <form @submit.prevent="loginUser" class="space-y-5">
        <!-- Email 欄位 -->
        <div>
            <label for="email" class="block text-sm font-medium text-gray-700 mb-1">Email</label>
            <input
                type="email" id="email" v-model="email" required placeholder="you@example.com"
                class="w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
        </div>
    
        <!-- 密碼欄位 -->
        <div>
            <label for="password" class="block text-sm font-medium text-gray-700 mb-1">Password</label>
            <input
                type="password" id="password" v-model="password" required placeholder="Enter your password"
                class="w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
        </div>
    
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="text-red-500 text-sm text-center">{{ errorMessage }}</div>
    
        <!-- 登入並進入首頁 -->
        <button
            type="submit"
            class="w-full bg-blue-500 hover:bg-blue-600 text-white font-medium py-2 rounded-lg transition duration-200"
        >
        Login
        </button>
    </form>
</template>

<script>
import { login } from '@/api/login'
import errorService from '@/service/errorService';
  
export default {
    data() {
        return {
            email: '',
            password: '',
            errorMessage: ''
        }
    },
    methods: {
        async loginUser() {   
            try {
                // 發送登入請求到後端，並獲取 JWT
                const { token, user, message } = await login(this.email, this.password)
        
                if (token && user) {
                    // 登入成功
                    this.$emit('success', { token, user })
                }
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Login failed.';
            }
        }
    }
}
</script>