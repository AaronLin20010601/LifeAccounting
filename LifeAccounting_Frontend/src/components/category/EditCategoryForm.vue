<template>
    <!-- 編輯收支類型表單 -->
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
                    <option value="Income">Income</option>
                    <option value="Expense">Expense</option>
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
            
            <!-- 返回收支類型列表 -->
            <button @click="$router.push('/category')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Category List
            </button>
        </div>
    </form>
</template>

<script>
import { getEditCategory, updateCategory } from '@/api/category'
import errorService from '@/service/errorService';
  
export default {
    props: ['categoryId'],
    data() {
        return {
            category: {
                name: '',
                type: 'income'
            },
            errorMessage: ''
        }
    },
    async created(){
        // 取得要編輯的收支類型資料
        try {
            const result = await getEditCategory(this.categoryId)
            this.category = {
                name: result.name,
                type: result.type
            }
        } catch (error) {
            this.errorMessage = errorService.handleError(error) || 'Failed to fetch category data.'
        }
    },
    methods: {
        // 新增收支類型
        async onSubmit() {
            try {
                // 新增成功
                await updateCategory(this.categoryId, this.category)
                alert('Category updated successfully!')
                this.$emit('success')
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'An error occurred while updating category.'
            }
        }
    }
}
</script>