<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Transfer Balance</h1>
    
        <!-- 餘額轉移表單 -->
        <TransferBalanceForm @success="handleSuccess" />
    </div>
</template>
  
<script>
import TransferBalanceForm from '@/components/account/TransferBalanceForm.vue'
  
export default {
    components: { TransferBalanceForm },
    methods: {
        // 返回帳戶列表
        handleSuccess() {
            this.$router.push('/account');
        }
    }
};
</script>