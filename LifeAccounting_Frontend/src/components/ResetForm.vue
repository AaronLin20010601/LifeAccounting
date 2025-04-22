<template>
    <!-- 重設密碼表單 -->
    <form @submit.prevent="handleReset" class="space-y-5">
        <!-- 新密碼欄位 -->
        <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">New Password</label>
            <input
                v-model="password" type="password" required placeholder="Enter your new password"
                class="w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
            />
        </div>
    
        <!-- 確認密碼欄位 -->
        <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Confirm Password</label>
            <input
                v-model="confirmPassword" type="password" required placeholder="Re-enter your new password"
                class="w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
            />
        </div>
    
        <!-- 驗證碼欄位 -->
        <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Verification Code</label>
            <input
                v-model="verificationCode" type="text" required placeholder="Enter the code sent to your email"
                class="w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-400"
            />
        </div>
    
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="text-red-500 text-sm text-center">{{ errorMessage }}</div>
    
        <!-- 重設密碼 -->
        <button
            type="submit"
            class="w-full bg-green-500 hover:bg-green-600 text-white font-medium py-2 rounded-lg transition duration-200"
        >
        Reset Password
        </button>
    </form>
</template>

<script>
import { reset } from '@/api/reset';
import errorService from '@/service/errorService';

export default {
    props: {
        email: {
            type: String,
            required: true
        }
    },
    emits: ['success'],
    data() {
        return {
            password: '',
            confirmPassword: '',
            verificationCode: '',
            errorMessage: ''
        };
    },
    methods: {
        async handleReset() {    
            try {
                // 送出重設資料
                const response = await reset({
                    email: this.email,
                    password: this.password,
                    confirmPassword: this.confirmPassword,
                    verificationCode: this.verificationCode
                });
    
                alert(response);
                this.$emit('success');
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Failed to reset.';
            }
        }
    }
};
</script>