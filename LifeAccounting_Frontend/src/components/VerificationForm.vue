<template>
    <!-- 發送驗證碼表單 -->
    <form @submit.prevent="handleSendCode" class="space-y-5">
        <!-- Email 欄位 -->
        <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">Email</label>
            <input
                v-model="localEmail" type="email" required placeholder="Enter your email"
                class="w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-400"
            />
        </div>
    
        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="text-red-500 text-sm text-center">{{ errorMessage }}</div>
    
        <!-- 發送驗證碼 -->
        <button
            type="submit"
            class="w-full bg-blue-500 hover:bg-blue-600 text-white font-medium py-2 rounded-lg transition duration-200"
        >
        Send Verification Code
        </button>
    </form>
</template>

<script>
import errorService from '@/service/errorService';

export default {
    props: {
        email: String,
        sendCodeApi: {
            type: Function,
            required: true
        }
    },
    emits: ['success', 'update:email'],
    data() {
        return {
            localEmail: this.email || '',
            errorMessage: ''
        };
    },
    methods: {
        async handleSendCode() {
            try {
                // 發送驗證碼
                const response = await this.sendCodeApi(this.localEmail);
                alert(response);
                this.$emit('update:email', this.localEmail);
                this.$emit('success');
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Failed to send verification code.';
            }
        }
    }
};
</script>