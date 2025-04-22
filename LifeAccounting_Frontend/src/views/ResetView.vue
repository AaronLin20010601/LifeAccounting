<template>
    <div class="flex justify-center items-center bg-gray-50">
        <div class="w-full max-w-md bg-white border border-gray-300 rounded-2xl shadow-md p-6">
            <h2 class="text-3xl font-semibold text-center text-gray-800 mb-6">
                {{ step === 1 ? "📧 Send Verification Code" : "🔒 Reset Password" }}
            </h2>
    
            <!-- 發送驗證碼表單 -->
            <VerificationForm
                v-if="step === 1" :email="email" :sendCodeApi="sendResetVerificationCode"
                @update:email="email = $event" @success="step = 2"
            />
    
            <!-- 重設密碼表單 -->
            <ResetForm v-else :email="email" @success="handleSuccess"/>
        </div>
    </div>
</template>

<script>
import VerificationForm from '@/components/VerificationForm.vue';
import ResetForm from '@/components/ResetForm.vue';
import { sendResetVerificationCode } from '@/api/verification';

export default {
    components: {
        VerificationForm,
        ResetForm
    },
    data() {
        return {
            step: 1,
            email: '',
        };
    },
    methods: {
        sendResetVerificationCode,
        // 重設成功後前往登入頁面
        handleSuccess() {
            this.$router.push('/')
        },
    },
};
</script>