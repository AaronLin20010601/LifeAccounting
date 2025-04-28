<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Add New Record</h1>
        
        <!-- 新增紀錄表單 -->
        <AddRecordForm @success="handleSuccess"/>
    </div>
</template>

<script>
import AddRecordForm from '@/components/record/AddRecordForm.vue'

export default {
    components: { AddRecordForm },
    methods: {
        // 新增後返回紀錄
        handleSuccess() {
            this.$router.push('/record')
        }
    },
};
</script>