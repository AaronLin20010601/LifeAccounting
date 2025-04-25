<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Account List</h1>
        <p class="mb-4">Your account list.</p>

        <!-- 錯誤訊息 -->
        <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>

        <!-- 帳戶列表表格 -->
        <AccountTable :accounts="accounts" @reload="getAccounts" />
    </div>
</template>

<script>
import AccountTable from '@/components/account/AccountTable.vue';
import { fetchAccounts } from '@/api/account';
import errorService from '@/service/errorService';

export default {
    components: { AccountTable },
    data() {
        return {
            accounts: [],
            errorMessage: ''
        };
    },
    methods: {
        // 取得帳戶列表
        async getAccounts() {
            try {
                const data = await fetchAccounts();
                this.accounts = data;
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Error fetching accounts';
            }
        }
    },
    mounted() {
        this.getAccounts();
    }
};
</script>