<template>
    <div class="flex flex-wrap gap-4 items-center mb-6">
        <div class="flex flex-wrap gap-4 items-center">
            <!-- 帳戶選擇 -->
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

            <label class="mr-2">Sync with accounts:</label>
            <input
                type="checkbox" v-model="user.isSync" @change="toggleUserSync"
                class="w-5 h-5 text-green-600 bg-gray-100 border-gray-300 rounded focus:ring-green-500"
            />
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

            <button @click="downloadExcel" class="bg-cyan-500 text-white p-2 rounded-md hover:bg-cyan-600">
                Download Record
            </button>
        </div>
    </div>
</template>
  
<script>
import { fetchMeta } from '@/api/meta';
import { exportRecordsToExcel } from '@/api/excel';
import { fetchUser, toggleSync } from '@/api/user';
import { downloadExcelFile } from '@/service/excelService';

export default {
    emits: ['filter-change'],
    data() {
        return {
            user: {
                isSync: false
            },
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
        this.fetchUserData();
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
            const meta = await fetchMeta();
            this.accounts = meta.accounts;
            this.categories = meta.categories;
        },
        // 取得使用者資料
        async fetchUserData() {
            const data = await fetchUser();
            this.user = data.item;
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
        // 下載 excel
        async downloadExcel() {
            await downloadExcelFile(exportRecordsToExcel, {
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
        // 更新帳戶和紀錄同步
        async toggleUserSync() {
            await toggleSync(this.user.isSync);
        },
    },
};
</script>