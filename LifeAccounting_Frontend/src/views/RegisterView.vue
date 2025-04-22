<template>
    <div class="flex justify-center items-center bg-gray-50">
        <div class="w-full max-w-md bg-white border border-gray-300 rounded-2xl shadow-md p-6">
            <h2 class="text-3xl font-semibold text-center text-gray-800 mb-6">
                {{ step === 1 ? "📧 Send Verification Code" : "📝 Register Account" }}
            </h2>
    
            <!-- 發送驗證碼表單 -->
            <VerificationForm 
                v-if="step === 1" :email="email" :sendCodeApi="sendRegisterVerificationCode"
                @update:email="email = $event" @success="step = 2"
            />
    
            <!-- 註冊表單 -->
            <RegisterForm v-else :email="email" @success="handleSuccess"/>
        </div>
    </div>
</template>

<script>
import VerificationForm from '@/components/VerificationForm.vue';
import RegisterForm from '@/components/RegisterForm.vue';
import { sendRegisterVerificationCode } from '@/api/verification';

export default {
    components: {
        VerificationForm,
        RegisterForm
    },
    data() {
        return {
            step: 1,
            email: '',
        };
    },
    methods: {
        sendRegisterVerificationCode,
        // 註冊成功後前往登入頁面
        handleSuccess() {
            this.$router.push('/')
        },
    },
};
</script>