<template>
    <div class="max-w-4xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Edit Record</h1>
        
        <!-- 編輯紀錄表單 -->
        <EditRecordForm :recordId="recordId" @success="handleSuccess"/>
    </div>
</template>

<script>
import EditRecordForm from '@/components/record/EditRecordForm.vue'

export default {
    components: { EditRecordForm },
    computed: {
        recordId() {
            return this.$route.params.id
        }
    },
    methods: {
        // 編輯後返回紀錄
        handleSuccess() {
            this.$router.push('/record')
        }
    },
};
</script>