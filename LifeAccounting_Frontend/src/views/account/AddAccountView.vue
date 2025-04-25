<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Add New Account</h1>
        
        <!-- 新增帳戶表單 -->
        <AddAccountForm @success="handleSuccess"/>
    </div>
</template>
  
<script>
import AddAccountForm from '@/components/account/AddAccountForm.vue'

export default {
    components: { AddAccountForm },
    methods: {
        // 新增後返回帳戶列表
        handleSuccess() {
            this.$router.push('/account')
        }
    },
};
</script>