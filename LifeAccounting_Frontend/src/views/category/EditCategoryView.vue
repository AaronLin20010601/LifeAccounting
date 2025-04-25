<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Category</h1>
        
        <!-- 編輯收支類型表單 -->
        <EditCategoryForm :categoryId="categoryId" @success="handleSuccess"/>
    </div>
</template>
  
<script>
import EditCategoryForm from '@/components/category/EditCategoryForm.vue'

export default {
    components: { EditCategoryForm },
    computed: {
        categoryId() {
            return this.$route.params.id
        }
    },
    methods: {
        // 編輯後返回收支類型列表
        handleSuccess() {
            this.$router.push('/category')
        }
    },
};
</script>