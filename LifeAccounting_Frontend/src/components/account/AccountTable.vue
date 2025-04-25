<template>
    <div>
        <!-- 新增帳戶-->
        <div class="flex justify-end mb-4">
            <button @click="goToAddAccount" class="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600">
                Add Account
            </button>
        </div>
        <!-- 帳戶列表 當沒有任何帳戶時顯示 "No accounts found" -->
        <div v-if="accounts.length === 0" class="text-center text-gray-500 p-8 border border-gray-200 rounded-lg">
            No accounts found.
        </div>

        <!-- 帳戶列表表格 僅在有帳戶時顯示 -->
        <table v-else class="w-full table-auto">
            <thead>
                <tr class="border-b">
                    <th class="px-4 py-2 text-left w-[300px]">Name</th>
                    <th class="px-4 py-2 text-left w-[150px]">Currency</th>
                    <th class="px-4 py-2 text-left w-[200px]">Created At</th>
                    <th class="px-4 py-2 text-left">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="account in accounts" :key="account.id" class="border-b">
                    <!-- 帳戶名稱 -->
                    <td class="px-4 py-2">{{ account.name }}</td>
                    <!-- 帳戶幣別 -->
                    <td class="px-4 py-2">{{ account.currency }}</td>
                    <!-- 建立時間 -->
                    <td class="px-4 py-2">{{ formatDate(account.createdAt) }}</td>

                    <!-- 操作欄 -->
                    <td class="px-4 py-2 flex space-x-2">
                        <!-- 前往編輯帳戶 -->
                        <button @click="editAccount(account)" class="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600">
                            Edit
                        </button>
                        <!-- 刪除帳戶 -->
                        <button @click="removeAccount(account)" class="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-700">
                            Delete
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import { deleteAccount } from '@/api/account';

export default {
    props: {
        accounts: Array,
    },
    emits: ['reload'],
    methods: {
        // 格式化日期
        formatDate(date) {
            return new Date(date).toLocaleString();
        },
        // 前往新增帳戶
        goToAddAccount() {
            this.$router.push('/addaccount');
        },
        // 編輯帳戶
        editAccount(account) {
            this.$router.push(`/editaccount/${account.id}`);
        },
        // 刪除帳戶
        async removeAccount(account) {
            if (!confirm('Are you sure you want to delete this account?')) return;
            await deleteAccount(account.id);
            this.$emit('reload');
        }
    }
};
</script>