<template>
    <form @submit.prevent="onSubmit" class="space-y-6">
        <!-- 帳戶選擇 -->
        <div>
            <label for="account" class="block text-sm font-medium text-gray-700">Account</label>
            <select v-model="record.accountId" id="account" required class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md">
                <option disabled value="">Select an account</option>
                <option v-for="account in accounts" :key="account.id" :value="account.id">
                    {{ account.name }}
                </option>
            </select>
        </div>
    
        <!-- 類別選擇 -->
        <div>
            <label for="category" class="block text-sm font-medium text-gray-700">Category</label>
            <select v-model="record.categoryId" id="category" class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md">
                <option value="">Select a category</option>
                <option v-for="category in categories" :key="category.id" :value="category.id">
                    {{ category.name }}
                </option>
            </select>
        </div>
    
        <!-- 收支類型 -->
        <div>
            <label for="type" class="block text-sm font-medium text-gray-700">Type</label>
            <select v-model="record.type" :disabled="isTypeLocked" id="type" required class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md bg-white disabled:bg-gray-100">
                <option value="">Select a type</option>
                <option value="Income">Income</option>
                <option value="Expense">Expense</option>
            </select>
        </div>
    
        <!-- 金額輸入 -->
        <div>
            <label for="amount" class="block text-sm font-medium text-gray-700">Amount</label>
            <input 
                v-model.number="record.amount" type="number" step="0.01" id="amount" required
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <!-- 日期選擇 -->
        <div>
            <label for="date" class="block text-sm font-medium text-gray-700">Date</label>
            <input 
                v-model="record.date" type="datetime-local" id="date" required
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
            />
        </div>
    
        <!-- 備註 -->
        <div>
            <label for="note" class="block text-sm font-medium text-gray-700">Note</label>
            <textarea 
                v-model="record.note" id="note" rows="3"
                class="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md"
            ></textarea>
        </div>
    
        <!-- 錯誤訊息 -->
        <div v-if="errorMessage" class="mt-4 text-red-500">{{ errorMessage }}</div>
    
        <div class="flex justify-between">
            <button type="submit" class="bg-green-500 text-white px-4 py-2 rounded-md hover:bg-green-600">
            Add Record
            </button>
    
            <button @click="$router.push('/record')" type="button" class="bg-gray-500 text-white px-4 py-2 rounded-md hover:bg-gray-600">
            Back to Record List
            </button>
        </div>
    </form>
</template>

<script>
import { createRecord } from '@/api/record';
import { fetchMeta } from '@/api/meta';
import errorService from '@/service/errorService';

export default {
    data() {
        return {
            record: {
                accountId: '',
                categoryId: '',
                type: '',
                amount: null,
                date: '',
                note: ''
            },
            accounts: [],
            categories: [],
            errorMessage: ''
        };
    },
    computed: {
        // 如果有選擇 category，就鎖定 type
        isTypeLocked() {
            return this.record.categoryId !== '';
        }
    },
    watch: {
        // 監聽 categoryId 變化
        'record.categoryId'(newCategoryId) {
            if (newCategoryId) {
                const selectedCategory = this.categories.find(c => c.id === newCategoryId);
                if (selectedCategory) {
                    this.record.type = selectedCategory.type;
                }
            }
        }
    },
    async created() {
        try {
            const meta = await fetchMeta();
            this.accounts = meta.accounts;
            this.categories = meta.categories;
        } catch (error) {
            this.errorMessage = errorService.handleError(error) || 'Failed to load meta data.';
        }
    },
    methods: {
        async onSubmit() {
            let dateTime
            dateTime = new Date(this.record.date);
            this.record.date = dateTime.toISOString();

            if (this.record.categoryId === ''){
                this.record.categoryId = null;
            }

            try {
                await createRecord(this.record);
                alert('Record added successfully!');
                this.errorMessage = ''
                this.$emit('success');
            } catch (error) {
                this.errorMessage = errorService.handleError(error) || 'Failed to add record.';
            }
        }
    }
};
</script>