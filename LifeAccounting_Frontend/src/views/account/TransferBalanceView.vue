<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Transfer Balance</h1>
        
        <!-- 不同幣值帳戶轉帳提示語 -->
        <div class="mt-6 p-4 bg-yellow-100 border-l-4 border-yellow-500 text-yellow-700 rounded">
            <p>Notice</p>
            <p>
                When transferring balances between accounts with different currencies, 
                the system will automatically convert the amount based on the exchange rate of the day.
                Note that exchange rate conversions may not be perfectly accurate and are based on the rates of the current day.
            </p>
        </div>

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