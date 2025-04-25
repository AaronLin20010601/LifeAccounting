<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Add New Category</h1>
        
        <!-- 新增收支類型表單 -->
        <AddCategoryForm @success="handleSuccess"/>
    </div>
</template>
  
<script>
import AddCategoryForm from '@/components/category/AddCategoryForm.vue'

export default {
    components: { AddCategoryForm },
    methods: {
        // 新增後返回收支類型列表
        handleSuccess() {
            this.$router.push('/category')
        }
    },
};
</script>