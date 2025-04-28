<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Category List</h1>
        <p class="mb-4">The customize income and expense category.</p>

        <!-- 錯誤訊息 -->
        <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>

        <!-- 收支類型列表表格 -->
        <CategoryTable :categories="categories" @reload="getCategories" />
    </div>
</template>

<script>
import CategoryTable from '@/components/category/CategoryTable.vue';
import { fetchCategories } from '@/api/category';
import errorService from '@/service/errorService';

export default {
    components: { CategoryTable },
    data() {
        return {
            categories: [],
            errorMessage: ''
        };
    },
    methods: {
        // 取得帳戶列表
        async getCategories() {
            try {
                const data = await fetchCategories();
                this.categories = data.items;
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Error fetching categories';
            }
        }
    },
    mounted() {
        this.getCategories();
    }
};
</script>