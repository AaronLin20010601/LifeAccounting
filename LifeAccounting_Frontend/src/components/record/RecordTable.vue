<template>
    <div>
        <!-- 紀錄列表 當沒有任何紀錄時顯示 "No Records" -->
        <div v-if="records.length === 0" class="text-center text-gray-500 p-8 border border-gray-200 rounded-lg">
            No Records available.
        </div>
        
        <!-- 紀錄列表表格 僅在有紀錄時顯示 -->
        <table v-else class="w-full table-auto">
            <thead>
                <tr class="border-b">
                    <th class="px-4 py-2 text-left w-[160px]">Account</th>
                    <th class="px-4 py-2 text-left w-[100px]">Category</th>
                    <th class="px-4 py-2 text-left w-[100px]">Amount</th>
                    <th class="px-4 py-2 text-left w-[300px]">Note</th>
                    <th class="px-4 py-2 text-left w-[160px]">Date</th>
                    <th class="px-4 py-2 text-left w-[100px]">Type</th>
                    <th class="px-4 py-2 text-left">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="record in records" :key="record.id" class="border-b">
                    <!-- 帳戶 -->
                    <td class="px-4 py-2">{{ record.accountName }}</td>
                    <!-- 收支類型 -->
                    <td class="px-4 py-2">{{ record.categoryName }}</td>
                    <!-- 金額 -->
                    <td class="px-4 py-2">{{ record.amount }}</td>
                    <!-- 備註 -->
                    <td class="px-4 py-2">{{ record.note }}</td>
                    <!-- 收支時間 -->
                    <td class="px-4 py-2">{{ formatDate(record.date) }}</td>
                    <!-- 收入或支出 -->
                    <td class="px-4 py-2">{{ record.type }}</td>
                    
                    <!-- 操作欄 -->
                    <td class="px-4 py-2 flex space-x-2">
                        <!-- 前往編輯紀錄 -->
                        <button @click="editRecord(record)" class="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600">
                            Edit
                        </button>
                        
                        <!-- 刪除紀錄 -->
                        <button @click="removeRecord(record)" class="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-700">
                            Delete
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
  
<script>
import { deleteRecord } from '@/api/record';
  
export default {
    props: {
        records: Array,
    },
    emits: ['reload'],
    methods: {
        // 格式化日期
        formatDate(date) {
            return new Date(date).toLocaleString();
        },
        // 編輯紀錄
        editRecord(record) {
            this.$router.push(`/editrecord/${record.id}`);
        },
        // 刪除紀錄
        async removeRecord(record) {
            if (!confirm('Are you sure you want to delete this record?')) return;
            await deleteRecord(record.id);
            this.$emit('reload');
        },
    },
};
</script>