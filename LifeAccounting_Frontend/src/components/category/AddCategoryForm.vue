<template>
    <!-- 新增收支類型表單 -->
    <form @submit.prevent="onSubmit" class="space-y-6">
        <div class="flex space-x-4">
            <!-- 收支類型名稱 -->
            <div class="flex-1">
                <label for="name" class="block text-sm font-medium text-gray-700">Category name</label>
                <input 
                    v-model="category.name" type="text" id="name" required
                    class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
                />
            </div>

            <!-- 收入或支出選單 -->
            <div class="w-40">
                <label for="type" class="block text-sm font-medium text-gray-700">Type</label>
                <select
                    v-model="category.type" id="type" required
                    class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
                >
                    <option value="income">income</option>
                    <option value="expense">expense</option>
                </select>
            </div>
        </div>

        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">{{ errorMessage }}</div>
    
        <div class="flex justify-between">
            <!-- 新增收支類型 -->
            <button type="submit" class="bg-blue-500 text-white px-4 py-2 rounded-md hover:bg-blue-600">
            Add Category
            </button>
            
            <!-- 返回收支類型列表 -->
            <button @click="$router.push('/category')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Category List
            </button>
        </div>
    </form>
</template>

<script>
import { createCategory } from '@/api/category'
import errorService from '@/service/errorService';
  
export default {
    data() {
        return {
            category: {
                name: '',
                type: 'income'
            },
            errorMessage: ''
        }
    },
    methods: {
        // 新增收支類型
        async onSubmit() {
            try {
                // 新增成功
                await createCategory(this.category)
                alert('Category added successfully!')
                this.$emit('success')
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Add category failed.'
            }
        }
    }
}
</script>