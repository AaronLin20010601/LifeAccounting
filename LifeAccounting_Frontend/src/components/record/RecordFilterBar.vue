<template>
    <div class="flex flex-wrap gap-4 items-center mb-6">
        <!-- 帳戶選擇 -->
        <div class="flex flex-wrap gap-4 items-center">
            <label class="mr-2">Account:</label>
            <select v-model="selectedAccountId" @change="onFilterChange" class="p-2 border rounded-md">
                <option :value="null">All</option>
                <option v-for="account in accounts" :key="account.id" :value="account.id">{{ account.name }}</option>
            </select>
    
        <!-- 類別選擇 -->
            <label class="mr-2">Category:</label>
            <select v-model="selectedCategoryId" @change="onFilterChange" class="p-2 border rounded-md">
                <option :value="null">All</option>
                <option v-for="category in categories" :key="category.id" :value="category.id">{{ category.name }}</option>
            </select>

        <!-- 收支類型 -->
            <label class="mr-2">Type:</label>
            <select v-model="selectedType" :disabled="isTypeLocked" @change="onFilterChange" class="p-2 border rounded-md">
                <option value="">All</option>
                <option value="Income">Income</option>
                <option value="Expense">Expense</option>
            </select>
        </div>
    
        <div class="flex flex-wrap gap-4 items-center">
            <!-- 時間篩選 -->
            <label class="mr-2">Start Date:</label>
            <input type="datetime-local" v-model="startDate" @change="onFilterChange" class="p-2 border rounded-md" />
    
            <label class="mr-2">End Date:</label>
            <input type="datetime-local" v-model="endDate" @change="onFilterChange" class="p-2 border rounded-md" />

            <!-- 前往新增收支紀錄 -->
            <button @click="goToAddRecord" class="bg-blue-500 text-white p-2 rounded-md hover:bg-blue-600">
                Add Record
            </button>
        </div>
    </div>
</template>
  
<script>
import { fetchRecordMeta } from '@/api/record';
export default {
    emits: ['filter-change'],
    data() {
        return {
            accounts: [],
            categories: [],
            selectedAccountId: null,
            selectedCategoryId: null,
            selectedType: '',
            startDate: null,
            endDate: null,
        };
    },
    mounted() {
        this.fetchMetaData();
    },
    computed: {
        // 如果有選擇 category，就鎖定 type
        isTypeLocked() {
            return this.selectedCategoryId !== null;
        }
    },
    watch: {
        // 監聽 categoryId 變化
        selectedCategoryId(newCategoryId) {
            if (newCategoryId) {
                const selectedCategory = this.categories.find(c => c.id === newCategoryId);
                if (selectedCategory) {
                    this.selectedType = selectedCategory.type;
                }
            }
        }
    },
    methods: {
        async fetchMetaData() {
            const meta = await fetchRecordMeta();
            this.accounts = meta.accounts;
            this.categories = meta.categories;
        },
        onFilterChange() {
            this.$emit('filter-change', {
                accountId: this.selectedAccountId,
                categoryId: this.selectedCategoryId,
                type: this.selectedType,
                startDate: this.startDate,
                endDate: this.endDate,
            });
        },
        // 前往新增紀錄
        goToAddRecord() {
            this.$router.push('/addrecord');
        },
    },
};
</script>