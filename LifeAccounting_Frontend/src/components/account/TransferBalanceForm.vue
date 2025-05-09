<template>
    <form @submit.prevent="onSubmit" class="space-y-6 max-w-xl mx-auto">
        <!-- 來源帳戶 -->
        <div>
            <label for="fromAccountId" class="block text-sm font-medium text-gray-700">From Account</label>
            <select v-model="form.fromAccountId" id="fromAccountId" required class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md">
                <option disabled value="">Select source account</option>
                <option v-for="account in accounts" :key="account.id" :value="account.id">
                    {{ account.name }}
                </option>
            </select>
        </div>
    
        <!-- 目標帳戶 -->
        <div>
            <label for="toAccountId" class="block text-sm font-medium text-gray-700">To Account</label>
            <select v-model="form.toAccountId" id="toAccountId" required class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md">
                <option disabled value="">Select destination account</option>
                <option v-for="account in accounts" :key="account.id" :value="account.id">
                    {{ account.name }}
                </option>
            </select>
        </div>
    
        <!-- 金額 -->
        <div>
            <label for="amount" class="block text-sm font-medium text-gray-700">Amount</label>
            <input
                v-model.number="form.amount" type="number" step="0.01" min="0.01" id="amount" required
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <!-- 錯誤訊息 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">{{ errorMessage }}</div>

        <div class="flex justify-between">
            <button type="submit" class="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700">
            Transfer
            </button>
            <button @click="$router.push('/account')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Accounts
            </button>
        </div>
    </form>
</template>
  
<script>
import { fetchMeta } from '@/api/meta';
import { transferBalance } from '@/api/account';
import errorService from '@/service/errorService';
  
export default {
    data() {
      return {
            form: {
                fromAccountId: '',
                toAccountId: '',
                amount: null,
            },
            accounts: [],
            errorMessage: '',
        };
    },
    async created() {
        try {
            const meta = await fetchMeta();
            this.accounts = meta.accounts;
        } catch (error) {
            this.errorMessage = errorService.handleError(error) || 'Failed to load meta data.';
        }
    },
    methods: {
        async onSubmit() {
            try {
                await transferBalance(this.form);
                alert('Balance transfer successfully!');
                this.errorMessage = ''
                this.$emit('success');
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Failed to transfer balance.';
            }
        }
    }
};
</script>