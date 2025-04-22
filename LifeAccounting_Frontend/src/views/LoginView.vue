<template>
    <div class="flex justify-center items-center bg-gray-50">
        <div class="w-full max-w-md bg-white border border-gray-300 rounded-2xl shadow-md p-6">
            <h2 class="text-3xl font-semibold text-center text-gray-800 mb-6">
            🔐 Login to Life Accounting
            </h2>
    
            <!-- 登入表單 -->
            <LoginForm @success="handleSuccess" />
        </div>
    </div>
  </template>

<script>
import LoginForm from '@/components/LoginForm.vue'

export default {
    components: { LoginForm },
    methods: {
        // 登入成功後進入 Todo 首頁
        handleSuccess({ token, user }) {
            this.$store.dispatch('login', { token, user })
            this.$router.push('/todo')
        }
    },
};
</script>