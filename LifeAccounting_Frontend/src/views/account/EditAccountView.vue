<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Account</h1>
        
        <!-- 編輯帳戶表單 -->
        <EditAccountForm :accountId="accountId" @success="handleSuccess"/>
    </div>
</template>
  
<script>
import EditAccountForm from '@/components/account/EditAccountForm.vue'

export default {
    components: { EditAccountForm },
    computed: {
        accountId() {
            return this.$route.params.id
        }
    },
    methods: {
        // 編輯後返回帳戶列表
        handleSuccess() {
            this.$router.push('/account')
        }
    },
};
</script>