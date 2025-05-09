<template>
    <div class="max-w-6xl mx-auto mt-10 p-4">
        <h1 class="text-3xl font-bold mb-4">Income and Expense Record</h1>
        <p class="mb-4">Here is your record.</p>
        
        <!-- 篩選和新增按鈕區塊 -->
        <RecordFilterBar @filter-change="onFilterChange" />

        <!-- 錯誤消息顯示區域 -->
        <div v-if="errorMessage" class="text-red-500 text-sm mb-4">{{ errorMessage }}</div>

        <!-- 紀錄列表 -->
        <RecordTable :records="records" @reload="getRecords" />
        
        <!-- 分頁按鈕 -->
        <Pagination :currentPage="page" :totalPages="totalPages" @page-change="changePage" />
    </div>
</template>

<script>
import RecordFilterBar from '@/components/record/RecordFilterBar.vue';
import RecordTable from '@/components/record/RecordTable.vue';
import Pagination from '@/components/record/Pagination.vue';
import { fetchRecords } from '@/api/record';
import errorService from '@/service/errorService';

export default {
    components: { RecordFilterBar, RecordTable, Pagination },
    data() {
        return {
            records: [],
            accounts: [],
            categories: [],
            accountId: null,
            categoryId: null,
            type: '',
            startDate: null,
            endDate: null,
            page: 1,
            pageSize: 10,
            totalPages: 1,
            errorMessage: ''
        };
    },
    methods: {
        // 獲取紀錄列表
        async getRecords() {
            try {
                const data = await fetchRecords({
                    accountId : this.accountId, categoryId : this.categoryId, type : this.type,
                    startDate : this.startDate, endDate : this.endDate,
                    page : this.page, pageSize : this.pageSize
                });
                this.records = data.items;

                this.totalPages = data.totalPages;
                this.page = data.currentPage;
                this.errorMessage = ''
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Error fetching records'
            }
        },

        // 換頁
        changePage(pageNum) {
            if (pageNum !== this.page) {
                this.page = pageNum;
                this.getRecords(); // 換頁時重新取得資料
            }
        },

        // 重置頁面
        onFilterChange(newFilter) {
            this.accountId = newFilter.accountId;
            this.categoryId = newFilter.categoryId;
            this.type = newFilter.type;
            this.startDate = newFilter.startDate;
            this.endDate = newFilter.endDate;
            this.page = 1;
            this.getRecords();
        },
    },
    mounted() {
        this.getRecords(); // 頁面加載時自動獲取紀錄
    },
};
</script>