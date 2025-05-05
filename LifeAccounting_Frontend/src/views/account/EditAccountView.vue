<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Account</h1>

        <!-- 匯率轉換提示語 -->
        <div class="mt-6 p-4 bg-yellow-100 border-l-4 border-yellow-500 text-yellow-700 rounded">
            <p>Notice</p>
            <p>
                If you change both the currency and the balance at the same time, the balance will not be automatically converted. 
                Automatic conversion only occurs when you change the currency without modifying the balance. 
                Note that exchange rate conversions may not be perfectly accurate and are based on the rates of the current day. 
                All related income and expense records will also be converted to the new currency.
            </p>
        </div>
        
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