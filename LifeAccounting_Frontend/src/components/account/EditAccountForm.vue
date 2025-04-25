<template>
    <!-- 編輯帳戶表單 -->
    <form @submit.prevent="onSubmit" class="space-y-6">
        <div class="flex space-x-4">
            <!-- 帳戶名稱 -->
            <div class="flex-1">
                <label for="name" class="block text-sm font-medium text-gray-700">Account name</label>
                <input 
                    v-model="account.name" type="text" id="name" required
                    class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
                />
            </div>

            <!-- 幣別選單 -->
            <div class="w-40">
                <label for="currency" class="block text-sm font-medium text-gray-700">Currency</label>
                <select
                    v-model="account.currency" id="currency" required
                    class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
                >
                    <option value="TWD">TWD</option>
                    <option value="USD">USD</option>
                    <option value="JPY">JPY</option>
                    <option value="EUR">EUR</option>
                </select>
            </div>
        </div>

        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">{{ errorMessage }}</div>
    
        <div class="flex justify-between">
            <!-- 儲存變更 -->
            <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">
            Save Changes
            </button>
            
            <!-- 返回帳戶列表 -->
            <button @click="$router.push('/account')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Account List
            </button>
        </div>
    </form>
</template>

<script>
import { getEditAccount, updateAccount } from '@/api/account'
import errorService from '@/service/errorService';
  
export default {
    props: ['accountId'],
    data() {
        return {
            account: {
                name: '',
                currency: 'TWD'
            },
            errorMessage: ''
        }
    },
    async created() {
        // 取得要編輯的帳戶資料
        try {
            const result = await getEditAccount(this.accountId)
            this.account = {
                name: result.name,
                currency: result.currency
            }
        } catch (error) {
            this.errorMessage = errorService.handleError(error) || 'Failed to fetch account data.'
        }
    },
    methods: {
        // 提交編輯後的帳戶
        async onSubmit() {
            try {
                // 更新成功
                await updateAccountAccount(this.accountId, this.account)
                alert('Account updated successfully!')
                this.$emit('success')
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'An error occurred while updating account.'
            }
        }
    }
}
</script>