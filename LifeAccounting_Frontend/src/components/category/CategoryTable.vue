<template>
    <div>
        <!-- 新增收支類型 -->
        <div class="flex justify-end mb-4">
            <button @click="goToAddCategory" class="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600">
                Add Category
            </button>
        </div>
        <!-- 收支類型列表 當沒有任何收支類型時顯示 "No categories found" -->
        <div v-if="categories.length === 0" class="text-center text-gray-500 p-8 border border-gray-200 rounded-lg">
            No categories found.
        </div>

        <!-- 收支類型列表表格 僅在有收支類型時顯示 -->
        <table v-else class="w-full table-auto">
            <thead>
                <tr class="border-b">
                    <th class="px-4 py-2 text-left w-[300px]">Name</th>
                    <th class="px-4 py-2 text-left w-[150px]">Type</th>
                    <th class="px-4 py-2 text-left">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="category in categories" :key="category.id" class="border-b">
                    <!-- 收支類型名稱 -->
                    <td class="px-4 py-2">{{ category.name }}</td>
                    <!-- 收入或支出 -->
                    <td class="px-4 py-2">{{ category.type }}</td>

                    <!-- 操作欄 -->
                    <td class="px-4 py-2 flex space-x-2">
                        <!-- 前往編輯收支類型 -->
                        <button @click="editCategory(category)" class="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600">
                            Edit
                        </button>
                        <!-- 刪除收支類型 -->
                        <button @click="removeCategory(category)" class="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-700">
                            Delete
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import { deleteCategory } from '@/api/category';

export default {
    props: {
        categories: Array,
    },
    emits: ['reload'],
    methods: {
        // 前往新增收支類型
        goToAddCategory() {
            this.$router.push('/addcategory');
        },
        // 編輯收支類型
        editCategory(category) {
            this.$router.push(`/editcategory/${category.id}`);
        },
        // 刪除收支類型
        async removeCategory(category) {
            if (!confirm('Are you sure you want to delete this category?')) return;
            await deleteCategory(category.id);
            this.$emit('reload');
        }
    }
};
</script>